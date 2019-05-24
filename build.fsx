// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket:
nuget FSharp.Core
nuget Fake.DotNet.Cli
nuget Fake.DotNet.AssemblyInfoFile
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
open Fake.IO.Globbing.Operators
open Fake.DotNet


// Utils

// Helper active pattern for project types
let (|Fsproj|Csproj|Vbproj|) (projFileName:string) = 
    match projFileName with
    | f when f.EndsWith("fsproj") -> Fsproj
    | f when f.EndsWith("csproj") -> Csproj
    | f when f.EndsWith("vbproj") -> Vbproj
    | _                           -> failwith (sprintf "Project file %s not supported. Unknown project type." projFileName)



// Properties
let buildDir = "./build/"
let project = "src/Informedica.GenUtils.Lib/Informedica.GenUtils.Lib.fsproj"


// Targets

// Generate assembly info files with the right version & up-to-date information
Target.create "AssemblyInfo" <| fun _ ->
    let getAssemblyInfoAttributes projectName =
        [ Attribute.Title (projectName)
          Attribute.Product project
          Attribute.Description summary
          Attribute.Version release.AssemblyVersion
          Attribute.FileVersion release.AssemblyVersion ]

    let getProjectDetails projectPath =
        let projectName = System.IO.Path.GetFileNameWithoutExtension(projectPath)
        ( projectPath, 
          projectName,
          System.IO.Path.GetDirectoryName(projectPath),
          (getAssemblyInfoAttributes projectName)
        )

    !! "src/**/*.??proj"
    |> Seq.map getProjectDetails
    |> Seq.iter (fun (projFileName, projectName, folderName, attributes) ->
        match projFileName with
        | Fsproj -> CreateFSharpAssemblyInfo (folderName @@ "AssemblyInfo.fs") attributes
        | Csproj -> CreateCSharpAssemblyInfo ((folderName @@ "Properties") @@ "AssemblyInfo.cs") attributes
        | Vbproj -> CreateVisualBasicAssemblyInfo ((folderName @@ "My Project") @@ "AssemblyInfo.vb") attributes
        )



Target.create "clean" <| fun _ ->
    Trace.trace "Cleaning up stuff..."
    Shell.cleanDir buildDir


Target.create "build" <| fun _ ->
    Trace.trace "Build the project..."
    DotNet.build id project  


Target.create "test" <| fun _ ->
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


Target.create "nothing" ignore


// Dependencies

open Fake.Core.TargetOperators


"clean"
==> "build"
==> "test"
==> "publish"

// Start build

Target.runOrDefault "build"


