namespace BeachMobile.ImageService

module Language =

    type ErrorDescription = string
    type ImageId = string

    type ImageRequest = {
        ImageId   : ImageId
        Container : string
        TenantId  : string
    }

    type ContainerImagesRequest = {
        TenantId  : string
        Container : string
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

    type AddImageRequest = {
        TenantId  : string
        Container : string
        Title     : string
        Content   : byte[]
    }

    type AddImagesRequest = {
        Items : AddImageRequest seq
    }

    type AddContainerRequest = {
        TenantId  : string
        Container : string
    }

    type AddContainersRequest = AddContainerRequest seq

    type RemoveContainerRequest = {
        TenantId  : string
        Container : string
    }

    type RemoveContainersRequest = RemoveContainerRequest seq