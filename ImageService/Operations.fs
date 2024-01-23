namespace BeachMobile.ImageService

open System.Threading.Tasks
open Language

module Operations =

    type Get           = ImageRequest          -> Task<Result<Image, ErrorDescription>>
    type GetByCategory = CategoryImagesRequest -> Task<Result<Image, ErrorDescription>>
    type GetAll        = AllImagesRequest      -> Task<Result<Image, ErrorDescription>>

    type AddTenant = AddTenantRequest -> Task<Result<unit, ErrorDescription>>
    type AddImage  = AddImageRequest  -> Task<Result<unit, ErrorDescription>>