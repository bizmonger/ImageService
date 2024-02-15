namespace BeachMobile.BlobService

open System.Threading.Tasks
open Language

module Operations =

    module List =

        type ByContainer = ContainerRequest -> Task<Result<string seq, ErrorDescription>>
        type All         = AllIBlobsRequest -> Task<Result<string seq, ErrorDescription>>

    module Download =

        type Item      = BlobRequest      -> Task<Result<Image , ErrorDescription>>
        type Container = ContainerRequest -> Task<Result<Images, ErrorDescription>>
        type All       = AllIBlobsRequest -> Task<Result<Images, ErrorDescription>>

    module Tenant =

        type Add = AddTenantRequest -> Task<Result<unit, ErrorDescription>>

    module Upload =

        type Blob  = UploadBlobRequest  -> Task<Result<unit, ErrorDescription>>
        type Blobs = UploadBlobsRequest -> Task<Result<unit, ErrorDescription>>

    module Container =
        
        type Add          = ContainersRequest -> Task<Result<unit, ErrorDescription>>
        type Remove       = ContainersRequest -> Task<Result<unit, ErrorDescription>>
        type RemoveImages = ImageRequests     -> Task<Result<unit, ErrorDescription>>