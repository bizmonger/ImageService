namespace BeachMobile.ImageService

module Language =

    type ErrorDescription = string
    type ImageId = string

    type ContainerRequest = {
        TenantId  : string
        Container : string

    } with member x.QualifiedName = $"{x.TenantId}-{x.Container}"


    type ImageRequest = {
        ImageId   : ImageId
        Container : ContainerRequest
    }

    type AllImagesRequest = {
        TenantId : string
    }

    type Image = {
        Id : ImageId
        Data : byte[]
    }

    type Images = Image seq

    type AddTenantRequest = {
        TenantId : string
        ImageContainers : string seq
    }

    type UploadImageRequest = {
        Details : ImageRequest
        Content : byte[]
    }

    type UploadImagesRequest = {
        Items : UploadImageRequest seq
    }

    type ImageRequests = ImageRequest seq

    type ContainersRequest = ContainerRequest seq