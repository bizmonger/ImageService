module ImageService.Upload.Tests

open System.Configuration
open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock
open BeachMobile.BlobService.Language

[<Test>]
let ``Upload image`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        let request = { TenantId= someTenantId; Container= someContainer1 }

        match! Containers.add [request] with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            // Test
            match! Upload.blob someAddImageRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      ->

                // Teardown
                match! Containers.remove [request] with
                | Error msg -> Assert.Fail()
                | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Upload images`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        let request = { TenantId= someTenantId; Container= someContainer1 }

        match! Containers.add [request] with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            // Test
            match! Upload.blobs someAddImagesRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      ->

                // Teardown
                match! Containers.remove [request] with
                | Error msg -> Assert.Fail()
                | Ok _      -> Assert.Pass()
    }