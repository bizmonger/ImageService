namespace ImageService.TestAPI

open BeachMobile.ImageService.Language

module Mock =

    let someTenantId   = "some tenant id"
    let someContainer1 = "some-container-1"
    let someContainer2 = "some-container-2"
    let someTitle      = "some title"

    let someAddImageRequest : AddImageRequest = {
        TenantId  = someTenantId
        Container = someContainer1
        Title     = someTitle
        Content   = Array.zeroCreate 0
    }

    let someAddImagesRequest : AddImagesRequest = {
        Items = seq [someAddImageRequest]
    }

    let someContainerImagesRequest : ContainerImagesRequest = {
        TenantId  = someTenantId
        Container = someContainer1
    }

    let someAllImagesRequest : AllImagesRequest = {
        TenantId = someTenantId
    }

    let someAddTenantRequest : AddTenantRequest = {
        TenantId = someTenantId
        ImageContainers = [someContainer1; someContainer2]
    }

    let someImageRequest : ImageRequest = {
        ImageId   = someTitle
        Container = someContainer1
        TenantId  = someTenantId
    }