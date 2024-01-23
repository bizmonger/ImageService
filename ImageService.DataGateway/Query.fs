namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net
// https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio

open System
open System.IO
open Azure.Storage.Blobs
open Azure.Storage.Blobs.Models
open BeachMobile.ImageService

module QueryImages =

    let byItem     : Operations.Get           = fun v -> task { return Error "" }
    let byCategory : Operations.GetByCategory = fun v -> task { return Error "" }
    let all        : Operations.GetAll        = fun v -> task { return Error "" }