namespace ImageService.DataGateway

// https://learn.microsoft.com/en-us/azure/cdn/cdn-app-dev-net

open BeachMobile.ImageService

module Post =

    let images : Operations.Add = fun v -> task { return Error "" }