module ImageService.Tenant.Tests

open NUnit.Framework
open BeachMobile.ImageService.Language
open ImageService.DataGateway

open ImageService.TestAPI.Mock

[<Test>]
let ``Add tenant`` () =

    task {

        // Test
        match! Tenant.add someAddTenantRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      -> Assert.Pass()
    }