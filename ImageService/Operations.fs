namespace BeachMobile.ImageService

open System.Threading.Tasks
open Language

module Operations =

    module List =

        type ByContainer = ContainerRequest -> Task<Result<string seq, ErrorDescription>>
        type All        = AllImagesRequest       -> Task<Result<string seq, ErrorDescription>>

    module Download =

        type Item      = ImageRequest           -> Task<Result<Image , ErrorDescription>>
        type Container = ContainerRequest -> Task<Result<Images, ErrorDescription>>
        type All       = AllImagesRequest       -> Task<Result<Images, ErrorDescription>>

    module Tenant =

        type Add = AddTenantRequest -> Task<Result<unit, ErrorDescription>>

    module Upload =

        type Image  = UploadImageRequest  -> Task<Result<unit, ErrorDescription>>
        type Images = UploadImagesRequest -> Task<Result<unit, ErrorDescription>>

    module Container =
        
        type Add          = AddContainersRequest -> Task<Result<unit, ErrorDescription>>
        type Remove       = ContainersRequest    -> Task<Result<unit, ErrorDescription>>
        type RemoveImages = ImageRequests        -> Task<Result<unit, ErrorDescription>>