﻿namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Informedica.GenUtils.Lib")>]
[<assembly: AssemblyProductAttribute("Informedica.GenUtils.Lib")>]
[<assembly: AssemblyCompanyAttribute("halcwb")>]
[<assembly: AssemblyDescriptionAttribute("A library with utility functions")>]
[<assembly: AssemblyVersionAttribute("0.3.0")>]
[<assembly: AssemblyFileVersionAttribute("0.3.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.3.0"
    let [<Literal>] InformationalVersion = "0.3.0"
