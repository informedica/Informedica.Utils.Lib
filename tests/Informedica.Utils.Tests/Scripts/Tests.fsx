#I __SOURCE_DIRECTORY__

#r "nuget: MathNet.Numerics.FSharp"
#r "nuget: Expecto"
#r "nuget: Expecto.FsCheck"

#load "../../../src/Informedica.Utils.Lib/Continuation.fs" 
#load "../../../src/Informedica.Utils.Lib/Memoization.fs" 
#load "../../../src/Informedica.Utils.Lib/Reflection.fs" 
#load "../../../src/Informedica.Utils.Lib/NullCheck.fs" 
#load "../../../src/Informedica.Utils.Lib/BCL/Char.fs" 
#load "../../../src/Informedica.Utils.Lib/BCL/String.fs" 
#load "../../../src/Informedica.Utils.Lib/BCL/Int32.fs" 
#load "../../../src/Informedica.Utils.Lib/BCL/Double.fs" 
#load "../../../src/Informedica.Utils.Lib/BCL/BigRational.fs" 
#load "../../../src/Informedica.Utils.Lib/BCL/DateTime.fs" 
#load "../../../src/Informedica.Utils.Lib/Option.fs" 
#load "../../../src/Informedica.Utils.Lib/Array.fs" 
#load "../../../src/Informedica.Utils.Lib/List.fs" 
#load "../../../src/Informedica.Utils.Lib/Seq.fs" 
#load "../../../src/Informedica.Utils.Lib/Path.fs" 
#load "../../../src/Informedica.Utils.Lib/File.fs" 
#load "../../../src/Informedica.Utils.Lib/App.fs" 

#load "../Tests.fs"

open System
open Expecto
open Expecto.Flip
open Expecto.Logging

Tests.BigRational.tests
|> runTestsWithCLIArgs [ CLIArguments.Verbosity LogLevel.Verbose; CLIArguments.Sequenced ] [||]

Tests.Double.tests
|> runTestsWithCLIArgs [ CLIArguments.Verbosity LogLevel.Verbose; CLIArguments.Sequenced ] [||]

Tests.List.tests
|> runTestsWithCLIArgs [ CLIArguments.Verbosity LogLevel.Verbose; CLIArguments.Sequenced ] [||]

Tests.Reflection.tests
|> runTestsWithCLIArgs [ CLIArguments.Verbosity LogLevel.Verbose; CLIArguments.Sequenced ] [||]

Tests.String.tests
|> runTestsWithCLIArgs [ CLIArguments.Verbosity LogLevel.Verbose; CLIArguments.Sequenced ] [||]

