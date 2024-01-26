module ImageService.Container.Tests

open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock

[<Test>]
let ``Add container`` () =

    task {

        // Test
        match! Containers.add someAddContainersRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Remove container`` () =

    task {

        // Test
        match! Containers.remove someAddContainersRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }
