module ImageService.Container.Tests

open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock

[<Test>]
let ``Add container`` () =

    task {

        // Test
        match! Containers.add someContainersRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Remove container`` () =

    task {

        // Setup
        match! Containers.add someContainersRequest with
        | Error msg -> Assert.Fail msg
        | Ok _ ->

            // Test
            match! Containers.remove someContainersRequest with
            | Error msg -> Assert.Fail msg
            | Ok _      -> Assert.Pass()
    }
