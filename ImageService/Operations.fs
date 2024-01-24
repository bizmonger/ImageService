namespace BeachMobile.ImageService

open System.Threading.Tasks
open Language

module Operations =

    module List =

        type ByCategory = CategoryImagesRequest -> Task<Result<string, ErrorDescription>>
        type All        = AllImagesRequest      -> Task<Result<string, ErrorDescription>>

    module Download =

        type Item     = ImageRequest          -> Task<Result<Image , ErrorDescription>>
        type Category = CategoryImagesRequest -> Task<Result<Images, ErrorDescription>>
        type All      = AllImagesRequest      -> Task<Result<Images, ErrorDescription>>

    module Tenant =

        type Add = AddTenantRequest -> Task<Result<unit, ErrorDescription>>

    module Upload =

        type Image  = AddImageRequest -> Task<Result<unit, ErrorDescription>>