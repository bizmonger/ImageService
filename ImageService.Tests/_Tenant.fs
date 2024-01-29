module ImageService.Tenant.Tests

open System.Configuration
open NUnit.Framework
open ImageService.DataGateway
open ImageService.TestAPI.Mock

[<Test>]
let ``Add tenant`` () =

    task {

        // Setup
        ServiceUri.Instance <- ConfigurationManager.AppSettings["StorageConectionString"]

        // Test
        match! Tenant.add someTenantRequest with
        | Error msg -> Assert.Fail msg
        | Ok _      ->

            // Teardown
            match! Containers.remove someContainersRequest with
            | Error msg -> Assert.Fail()
            | Ok _      -> Assert.Pass()
    }