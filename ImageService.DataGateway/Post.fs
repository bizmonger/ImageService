namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System.IO
open Azure.Storage.Blobs
open BeachMobile.ImageService.Operations
open BeachMobile.ImageService.Language

module Tenant =

    let add : Tenant.Add = 
    
        fun v -> task {

            try
                return Error "TODO"

            with ex -> return Error (ex.GetBaseException().Message)
        }

module Upload =

    let image : Upload.Image = 
    
        fun image -> task { 
        
            try
                let containerName   = image.Details.QualifiedContainerName
                let serviceClient   = BlobServiceClient(ServiceUri.Instance)
                let containerClient = serviceClient.GetBlobContainerClient(containerName)
                let blobClient      = containerClient.GetBlobClient(image.Details.ImageId)

                let! response = blobClient.UploadAsync(new MemoryStream(image.Content))

                if response.HasValue
                then return Ok()
                else return Error "Failed to upload image"

            with ex -> return Error <| ex.GetBaseException().Message
        }

    let images : Upload.Images = 
    
        fun requests -> task {
        
            let result = requests.Items |> Seq.map(fun r -> r |> image |> Async.AwaitTask)
                                        |> Async.Parallel
                                        |> Async.RunSynchronously
                                        |> Seq.forall(fun r -> match r with 
                                                               | Ok _    -> true 
                                                               | Error _ -> false)
            match result with
            | false -> return Error "Error uploading all blob items"
            | true  -> return Ok ()
        }

module Containers =

    let add : Container.Add =

        fun v -> task {
        
            try
                let add(request:ContainerRequest) =

                    task {
                        let  containerName = request.QualifiedName
                        let  serviceClient = BlobServiceClient(ServiceUri.Instance)
                        let! response = serviceClient.CreateBlobContainerAsync(containerName)

                        if response.HasValue
                        then return Ok()
                        else return Error $"Failed to create container: {containerName}"
                    }

                return v |> Seq.map (fun c -> c |> add |> Async.AwaitTask)
                         |> Async.Parallel
                         |> Async.RunSynchronously
                         |> Seq.ofArray
                         |> Seq.forall(fun r -> match r with | Ok _ -> true | Error _ -> false)
                         |> function
                            | false -> Error "Failed to create all conatiners"
                            | true  -> Ok ()

            with ex -> return Error <| ex.GetBaseException().Message
        }

    let remove : Container.Remove =

        fun v -> task {
        
            try
                let delete(request:ContainerRequest) =

                    task {
                        let  containerName = request.QualifiedName
                        let  serviceClient = BlobServiceClient(ServiceUri.Instance)
                        let! response = serviceClient.DeleteBlobContainerAsync(containerName)

                        if not response.IsError
                        then return Ok()
                        else return Error $"Failed to remove container: {containerName}"
                    }

                return v |> Seq.map (fun c -> c |> delete |> Async.AwaitTask)
                         |> Async.Parallel
                         |> Async.RunSynchronously
                         |> Seq.ofArray
                         |> Seq.forall(fun r -> match r with | Ok _ -> true | Error _ -> false)
                         |> function
                            | false -> Error "Failed to remove all conatiners"
                            | true  -> Ok ()

            with ex -> return Error <| ex.GetBaseException().Message
        }

    let removeImages : Container.RemoveImages =

        fun v -> task {

            try

                let  serviceClient = BlobServiceClient(ServiceUri.Instance)

                let delete (r:ImageRequest) =

                    let containerName   = r.QualifiedContainerName
                    let containerClient = serviceClient.GetBlobContainerClient(containerName)
                    let response = containerClient.DeleteBlob(r.ImageId)

                    if response.IsError
                    then Error $"Failed to delete blob: {r.ImageId}"
                    else Ok()
    
                return

                    v |> Seq.map(delete)
                      |> Seq.forall(fun r -> match r with | Ok _ -> true | Error _ -> false)
                      |> function
                         | false -> Error "Failed to remove all conatiners"
                         | true  -> Ok ()

            with ex -> return Error <| ex.GetBaseException().Message
        }