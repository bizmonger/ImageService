module ImageService.Download.Tests

open System.Configuration
open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock
open BeachMobile.ImageService.Language

[<Test>]
let ``Download image`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        let request = { TenantId= someTenantId; Container= someContainer1 }

        match! Containers.add [request] with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            match! Upload.image someAddImageRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      ->

                // Test
                match! Download.item someImageRequest with
                | Error msg -> Assert.Fail msg
                | Ok _ ->

                // Teardown
                match! Containers.remove [request] with
                | Error msg -> Assert.Fail()
                | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Download container images`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        let request = { TenantId= someTenantId; Container= someContainer1 }

        match! Containers.add [request] with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            match! Upload.image someAddImageRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      ->

                // Test
                match! Download.container someContainerRequest with
                | Error msg -> Assert.Fail msg
                | Ok _ ->

                // Teardown
                match! Containers.remove [request] with
                | Error msg -> Assert.Fail()
                | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Download all images`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        let request = { TenantId= someTenantId; Container= someContainer1 }

        match! Containers.add [request] with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            match! Upload.image someAddImageRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      ->

                // Test
                match! Download.all someAllImagesRequest with
                | Error msg -> Assert.Fail msg
                | Ok _ ->

                // Teardown
                match! Containers.remove [request] with
                | Error msg -> Assert.Fail msg
                | Ok _      -> Assert.Pass()
    }