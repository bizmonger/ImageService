module ImageService.Upload.Tests

open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock

[<Test>]
let ``Upload image`` () =

    task {

        // Test
        match! Upload.image someAddImageRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Upload images`` () =

    task {

        // Test
        match! Upload.images someAddImagesRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }