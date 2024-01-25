module ImageService.Download.Tests

open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock

[<Test>]
let ``Download image`` () =

    task {

        // Test
        match! DownloadImages.byItem someImageRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }

[<Test>]
let ``Download images`` () =

    task {

        // Setup


        // Test
        match! Upload.images someAddImagesRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }