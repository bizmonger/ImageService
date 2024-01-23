namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System
open Azure.Identity
open Azure.Storage.Blobs
open System.IO
open BeachMobile.ImageService

type ServiceUri() =

    static member val Instance = "" with get,set

module Upload =

    let image : Operations.Add = 
    
        fun image -> task { 
        
            let  containerName   = $"{image.TenantId}-{image.Category}"
            let  serviceClient   = BlobServiceClient(Uri(ServiceUri.Instance), DefaultAzureCredential())
            let  containerClient = serviceClient.GetBlobContainerClient(containerName)
            let  blobClient      = containerClient.GetBlobClient(image.Title)
            blobClient.UploadAsync(new MemoryStream(image.Image)) |> ignore

            return Ok(); 
        }