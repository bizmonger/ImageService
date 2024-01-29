namespace ImageService.TestAPI

open BeachMobile.ImageService.Language

module Mock =

    let someTenantId   = "some-tenant-id"
    let someContainer1 = "some-container-1"
    let someContainer2 = "some-container-2"
    let someTitle      = "some title"

    let someImageDetails : ImageRequest = {
        TenantId  = someTenantId
        Container = someContainer1
        ImageId   = someTitle
    }

    let someAddImageRequest : UploadImageRequest = {
        Details = someImageDetails
        Content = Array.zeroCreate 0
    }

    let someAddImagesRequest : UploadImagesRequest = {
        Items = seq [someAddImageRequest]
    }

    let someContainerRequest : ContainerRequest = {
        TenantId  = someTenantId
        Container = someContainer1
    }

    let someAllImagesRequest : AllImagesRequest = {
        TenantId = someTenantId
    }

    let someTenantRequest : AddTenantRequest = {
        TenantId = someTenantId
        ImageContainers = [someContainer1; someContainer2]
    }

    let someImageRequest : ImageRequest = {
        ImageId   = someTitle
        Container = someContainer1
        TenantId  = someTenantId
    }

    let someContainersRequest = [someContainerRequest]