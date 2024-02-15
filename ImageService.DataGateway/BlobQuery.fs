namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System
open Azure.Storage.Blobs
open Azure.Storage.Blobs.Models
open Azure.Identity
open BeachMobile.BlobService
open Language
open Operations

type ServiceUri() = static member val Instance = "" with get,set

module ListImages =

    let private getBlobNames containerName =

        task {

            try
                let serviceClient   = BlobServiceClient(ServiceUri.Instance)
                let containerClient = serviceClient.GetBlobContainerClient(containerName)
                let blobNames       = containerClient.GetBlobs() |> Seq.map(fun blob -> blob.Name)

                return Ok blobNames

            with ex -> return Error (ex.GetBaseException().Message)
        }

    let byContainer : List.ByContainer =

        fun v -> task {

            try return! v.QualifiedName |> getBlobNames
            with ex -> return Error (ex.GetBaseException().Message)
        }

    let all : List.All  =

        fun v -> task {

            try
                let serviceClient = BlobServiceClient(ServiceUri.Instance)
                let containers    = serviceClient.GetBlobContainers(BlobContainerTraits.Metadata, BlobContainerStates.None, v.TenantId) |> Seq.map(id)
                let containerNames = containers |> Seq.map(fun c -> c.Name)

                let result = containerNames |> Seq.map(fun c -> c |> getBlobNames |> Async.AwaitTask)
                                            |> Async.Parallel 
                                            |> Async.RunSynchronously
                                            |> Array.toSeq

                match result |> Seq.forall(fun r -> match r with | Ok _ -> true | Error _ -> false) with
                | false -> return Error "Error retrieving all blob items"
                | true  ->

                    let getValue(result:Result<string seq,string>) = result |> function
                        | Error _ -> Seq.empty
                        | Ok v    -> v
                        
                    let items = result |> Seq.map(getValue) |> Seq.concat
                    return Ok items

            with ex -> return Error (ex.GetBaseException().Message)
        }

module Download =

    let private download (imageId:ImageId) (container:BlobContainerClient) =

        try
            let blobClient = container.GetBlobClient(imageId)
            let response   = blobClient.DownloadContent()

            if response.HasValue then

                let data = response.Value.Content.ToArray()
                Ok { Id= imageId; Data= data }
             
            else Error $"Blob not found: {container}:{imageId}"

        with ex -> Error <| ex.GetBaseException().Message

    let item : Download.Item = 

        fun v -> task {

            try 
                let serviceClient = BlobServiceClient(ServiceUri.Instance)
                let container = serviceClient.GetBlobContainerClient(v.Container.QualifiedName)

                return download v.ImageId container

            with ex -> return Error (ex.GetBaseException().Message)
        }

    let container : Download.Container =

        fun v -> task {

            try 
                let serviceClient = BlobServiceClient(ServiceUri.Instance)
                let container = serviceClient.GetBlobContainerClient(v.QualifiedName)

                let result = container.GetBlobs() 
                             |> Seq.map(fun blobItem -> download blobItem.Name container)

                match result |> Seq.forall(fun r -> match r with | Ok _ -> true | Error _ -> false) with
                | false -> return Error "Error downloading all blob items"
                | true  ->

                    let getValue(result:Result<Image,string>) = result |> function
                        | Error _ -> None
                        | Ok v    -> Some v
                        
                    let downloads = result |> Seq.map(getValue) |> Seq.choose id
                    return Ok downloads

            with ex -> return Error (ex.GetBaseException().Message)
        }

    let all : Download.All  =

        fun v -> task {

            let toContainerImagesRequest containerName : ContainerRequest = {
                    TenantId  = v.TenantId
                    Container = containerName
                }

            try
                let serviceClient = BlobServiceClient(ServiceUri.Instance)
                let result = serviceClient.GetBlobContainers(BlobContainerTraits.Metadata, BlobContainerStates.None, v.TenantId)
                             |> Seq.map(fun c -> c.Name.Replace($"{v.TenantId}-","") |> toContainerImagesRequest |> container |> Async.AwaitTask)
                             |> Async.Parallel 
                             |> Async.RunSynchronously
                             |> Array.toSeq
                
                match result |> Seq.forall(fun r -> match r with | Ok _ -> true | Error _ -> false) with
                | false -> return Error "Error downloading all blob items"
                | true  ->

                    let getValue(result:Result<Image seq,string>) = result |> function
                        | Error _ -> None
                        | Ok v    -> Some v
                        
                    let downloads = result |> Seq.map(getValue) |> Seq.choose id
                    return Ok (downloads |> Seq.concat)

            with ex -> return Error (ex.GetBaseException().Message) 
        }