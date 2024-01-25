namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System
open System.IO
open Azure.Identity
open Azure.Storage.Blobs
open Azure.Storage.Blobs.Models
open BeachMobile.ImageService.Operations

module Tenant =

    let add : Tenant.Add = 
    
        fun v -> task {

            try
                let client = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())

                let addContainer name = client.CreateBlobContainerAsync(name, PublicAccessType.Blob).Wait()
                
                v.ImageContainers |> Seq.iter(fun c -> addContainer c)

                return Ok ()

            with ex -> return Error (ex.GetBaseException().Message)
        }

module Upload =

    let image : Upload.Image = 
    
        fun image -> task { 
        
            let  containerName   = $"{image.TenantId}-{image.Container}"
            let  serviceClient   = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())
            let  containerClient = serviceClient.GetBlobContainerClient(containerName)
            let  blobClient      = containerClient.GetBlobClient(image.Title)

            blobClient.UploadAsync(new MemoryStream(image.Content)) |> ignore

            return Ok()
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