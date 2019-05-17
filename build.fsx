// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket:
nuget FSharp.Core
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"

#load "./.fake/build.fsx/intellisense.fsx"

//Temporary fix until this is resolved : https://github.com/mono/mono/issues/9315
#if !FAKE
#r "Facades/netstandard"
#r "netstandard"
#endif
//Temporary fix until this is resolved : https://github.com/mono/mono/issues/9315


System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__


open Fake.Core
open Fake.IO
open Fake.DotNet


// Properties
let buildDir = "./build/"
let project = "src/Informedica.GenUtils.Lib/Informedica.GenUtils.Lib.fsproj"

// Targets

Target.create "Clean" <| fun _ ->
    Trace.trace "Cleaning up stuff..."
    Shell.cleanDir buildDir


Target.create "Build" <| fun _ ->
    Trace.trace "Build the project..."
    DotNet.build id project  


Target.create "Test" <| fun _ ->
    Trace.trace "Running tests..."

    let cmd = "run"
    let args = "--project tests/Informedica.GenUtils.Tests/Informedica.GenUtils.Tests.fsproj"
    let result = 
        DotNet.exec 
            (fun x -> { x with DotNetCliPath = "dotnet" }) 
            cmd 
            args
    if not result.OK then 
        failwithf "`dotnet %s %s` failed" cmd args


Target.create "DoNothing" ignore


// Dependencies

open Fake.Core.TargetOperators


"Clean"
==> "Build"
==> "Test"


// Start build

Target.runOrDefault "Build"


