module ImageService.Download.Tests

open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock

[<Test>]
let ``Download image`` () =

    task {

        // Test
        match! Download.item someImageRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Download container images`` () =

    task {

        // Test
        match! Download.container someContainerRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Download all images`` () =

    task {

        // Test
        match! Download.all someAllImagesRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }