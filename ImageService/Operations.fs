namespace BeachMobile.ImageService

open System.Threading.Tasks
open Language

module Operations =

    type Get           = ImageRequest          -> Task<Result<Image , ErrorDescription>>
    type GetByCategory = CategoryImagesRequest -> Task<Result<Images, ErrorDescription>>
    type GetAll        = AllImagesRequest      -> Task<Result<Images, ErrorDescription>>

    type AddTenant = AddTenantRequest -> Task<Result<unit, ErrorDescription>>
    type Add       = AddImageRequest  -> Task<Result<unit, ErrorDescription>>