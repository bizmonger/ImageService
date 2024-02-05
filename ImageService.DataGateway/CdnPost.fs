namespace ImageService.DataGateway.Cdn

open BeachMobile.ImageService.Operations

// https://learn.microsoft.com/en-us/azure/cdn/cdn-create-a-storage-account-with-cdn?toc=%2Fazure%2Ffrontdoor%2FTOC.json

module Upload =

    let image : Upload.Image = 
    
        fun image -> task { return Error "TODO" }

    let images : Upload.Images = 
    
        fun image -> task { return Error "TODO" }

module Containers =

    let add : Container.Add =

        fun v -> task { return Error "TODO" }

    let remove : Container.Remove =

        fun v -> task { return Error "TODO" }

    let removeImages : Container.RemoveImages =

        fun v -> task { return Error "TODO" }