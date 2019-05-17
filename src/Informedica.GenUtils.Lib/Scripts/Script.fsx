// First load all dependencies
#r "netstandard"

#load "../../../.paket/load/netstandard2.1/main.group.fsx"
#load "../../../.paket/load/netstandard2.1/Testing/testing.group.fsx"

#load "../BCL/Char.fs"
#load "../Reflection.fs"



module CharTests =

    open Informedica.GenUtils.Lib.BCL
    open Swensen.Unquote
    open FsCheck

    // all lower case letters are not capitals
    test
        <@
            Char.letters
            |> Seq.forall (fun c ->
                c |> (Char.isCapital >> not)
            ) 
        @>

    // all capitals are capitals
    test 
        <@
            Char.capitals
            |> Seq.forall (fun c ->
                c |> Char.isCapital
            ) 
        @>


    let testLower = 
        Char.toLower 
        >> Char.isCapital 
        >> not

    Check.Quick testLower
    
    let testUpper c =
        if c |> Char.isLetter |> not then true
        else
            c
            |> Char.toUpper
            |> Char.isCapital

    Check.Quick testUpper


module ReflectionTests =

    open Informedica.GenUtils.Lib
    
    type Test = |Pass | Fail

    Reflection.toString Pass
    Reflection.fromString<Test> "blah"
    Reflection.fromString<Test> "Pass"
