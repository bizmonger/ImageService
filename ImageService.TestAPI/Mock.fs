namespace ImageService.TestAPI

open BeachMobile.BlobService.Language

module Mock =

    let someTenantId   = "some-tenant-id"
    let someContainer1 = "some-container-1"
    let someContainer2 = "some-container-2"
    let someTitle      = "some title"

    let someContainerRequest : ContainerRequest = {
        TenantId  = someTenantId
        Container = someContainer1
    }

    let someImageDetails : BlobRequest = {
        Container = someContainerRequest
        ImageId   = someTitle
    }

    let someAddImageRequest : UploadBlobRequest = {
        Details = someImageDetails
        Content = Array.zeroCreate 0
    }

    let someAddImagesRequest : UploadBlobsRequest = {
        Items = seq [someAddImageRequest]
    }

    let someAllImagesRequest : AllIBlobsRequest = {
        TenantId = someTenantId
    }

    let someTenantRequest : AddTenantRequest = {
        TenantId = someTenantId
        BlobContainers = [someContainer1; someContainer2]
    }

    let someImageRequest : BlobRequest = {
        ImageId   = someTitle
        Container = someContainerRequest
    }

    let someContainersRequest = [someContainerRequest]