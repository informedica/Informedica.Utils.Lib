// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

open Fake.Core
open Fake.IO

// Properties
let buildDir = "./build/"

// Targets
Target.create "Clean" <| fun _ ->
    Shell.cleanDir buildDir


Target.create "Default" <| fun _ ->
    Trace.log "Hello World"


// Dependencies
open Fake.Core.TargetOperators

"Clean"
==> "Default"

// Start build
Target.runOrDefault "Default"


