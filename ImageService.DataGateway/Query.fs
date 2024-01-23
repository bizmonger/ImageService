namespace ImageService.DataGateway

open BeachMobile.ImageService

module QueryImage =

    let item          : Operations.Get           = fun v -> task { return Error "" }
    let byCategory    : Operations.GetByCategory = fun v -> task { return Error "" }
    let allCategories : Operations.GetAll        = fun v -> task { return Error "" }