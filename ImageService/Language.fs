namespace BeachMobile.ImageService

module Language =

    type ErrorDescription = string
    type ImageId = string

    type ImageRequest = {
        Id       : ImageId
        TenantId : string
    }

    type CategoryImagesRequest = {
        TenantId : string
        Category : string
    }

    type AllImagesRequest = {
        TenantId : string
    }

    type Image = {
        Id : ImageId
        Data : byte[]
    }

    type AddTenantRequest = {
        TenantId : string
        ImageCategories : string seq
    }

    type AddImageRequest = {
        TenantId : string
        Category : string
        Title    : string
        Image    : byte[]
    }