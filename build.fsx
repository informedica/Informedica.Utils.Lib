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
    Shell.cleanDir buildDir


Target.create "Build" <| fun _ ->
    DotNet.build id project  


Target.create "Default" <| fun _ ->
    Trace.log "Hello World"


Target.create "DoNothing" <| ignore


// Dependencies

open Fake.Core.TargetOperators


"Clean"
==> "Default"


// Start build

Target.runOrDefault "Default"


