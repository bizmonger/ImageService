namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System
open Azure.Storage.Blobs
open Azure.Storage.Blobs.Models
open Azure.Identity
open BeachMobile.ImageService
open Operations

type ServiceUri() =

    static member val Instance = "" with get,set

module ListImages =

    let private getBlobNames containerName =

        task {

            try
                let serviceClient   = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())
                let containerClient = serviceClient.GetBlobContainerClient(containerName)
                let blobNames       = containerClient.GetBlobs() |> Seq.map(fun blob -> blob.Name)
                return Ok blobNames

            with ex -> return Error (ex.GetBaseException().Message)
        }

    let byCategory : List.ByCategory =

        fun v -> task {

            try return! $"{v.TenantId}-{v.Category}" |> getBlobNames
            with ex -> return Error (ex.GetBaseException().Message)
        }

    let all : List.All  =

        fun v -> task {

            try
                let serviceClient  = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())
                let containers     = serviceClient.GetBlobContainers(BlobContainerTraits.Metadata, BlobContainerStates.None, v.TenantId) |> Seq.map(id)
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
                        
                    let items = result |> Seq.map(fun r -> r |> getValue) 
                                       |> Seq.concat
                    return Ok items

            with ex -> return Error (ex.GetBaseException().Message)
        }

module DownloadImages =

    let byItem : Download.Item = 

        fun v -> task {

            return Error "" 
        }

    let byCategory : Download.Category =

        fun v -> task {

            return Error "" 
        }

    let all : Download.All  =

        fun v -> task {

            return Error "" 
        }