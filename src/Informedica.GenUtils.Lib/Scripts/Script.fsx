#load "load-project-release.fsx"

open Informedica.GenUtils.Lib

type Test = |Pass | Fail

Reflection.toString Pass
Reflection.fromString<Test> "blah"
