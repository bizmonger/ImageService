namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System
open System.IO
open Azure.Identity
open Azure.Storage.Blobs
open Azure.Storage.Blobs.Models
open BeachMobile.ImageService.Operations

type ServiceUri() =

    static member val Instance = "" with get,set

module Tenant =

    let add : Tenant.Add = 
    
        fun v -> task {

            try
                let client = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())

                let addContainer name = client.CreateBlobContainerAsync(name, PublicAccessType.Blob).Wait()
                
                v.ImageCategories |> Seq.iter(fun c -> addContainer c)

                return Ok ()

            with ex -> return Error (ex.GetBaseException().Message)
        }

module Upload =

    let image : Upload.Image = 
    
        fun image -> task { 
        
            let  containerName   = $"{image.TenantId}-{image.Category}"
            let  serviceClient   = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())
            let  containerClient = serviceClient.GetBlobContainerClient(containerName)
            let  blobClient      = containerClient.GetBlobClient(image.Title)

            blobClient.UploadAsync(new MemoryStream(image.Content)) |> ignore

            return Ok()
        }