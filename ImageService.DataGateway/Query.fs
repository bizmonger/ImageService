namespace ImageService.DataGateway

open BeachMobile.ImageService

module QueryImages =

    let byItem     : Operations.Get           = fun v -> task { return Error "" }
    let byCategory : Operations.GetByCategory = fun v -> task { return Error "" }
    let all        : Operations.GetAll        = fun v -> task { return Error "" }