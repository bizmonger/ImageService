namespace BeachMobile.BlobService

module Language =

    type ErrorDescription = string
    type ImageId = string

    type ContainerRequest = {
        TenantId  : string
        Container : string

    } with member x.QualifiedName = $"{x.TenantId}-{x.Container}"


    type BlobRequest = {
        ImageId   : ImageId
        Container : ContainerRequest
    }

    type AllIBlobsRequest = {
        TenantId : string
    }

    type Image = {
        Id : ImageId
        Data : byte[]
    }

    type Images = Image seq

    type AddTenantRequest = {
        TenantId : string
        BlobContainers : string seq
    }

    type UploadBlobRequest = {
        Details : BlobRequest
        Content : byte[]
    }

    type UploadBlobsRequest = {
        Items : UploadBlobRequest seq
    }

    type ImageRequests = BlobRequest seq

    type ContainersRequest = ContainerRequest seq