module ImageService.Container.Tests

open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock
open System.Configuration

[<Test>]
let ``Add container`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        // Test
        match! Containers.add someContainersRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Remove container`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        match! Containers.add someContainersRequest with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            // Test
            match! Containers.remove someContainersRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Remove images`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        match! Containers.removeImages [someImageRequest] with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            // Test
            match! ListImages.byContainer someContainerRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      -> Assert.Pass()
    }