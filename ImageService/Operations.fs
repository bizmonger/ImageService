namespace BeachMobile.ImageService

open System.Threading.Tasks
open Language

module Operations =

    module List =

        type ByCategory = ContainerImagesRequest -> Task<Result<string seq, ErrorDescription>>
        type All        = AllImagesRequest       -> Task<Result<string seq, ErrorDescription>>

    module Download =

        type Item      = ImageRequest           -> Task<Result<Image , ErrorDescription>>
        type Container = ContainerImagesRequest -> Task<Result<Images, ErrorDescription>>
        type All       = AllImagesRequest       -> Task<Result<Images, ErrorDescription>>

    module Tenant =

        type Add = AddTenantRequest -> Task<Result<unit, ErrorDescription>>

    module Upload =

        type Image  = AddImageRequest  -> Task<Result<unit, ErrorDescription>>
        type Images = AddImagesRequest -> Task<Result<unit, ErrorDescription>>