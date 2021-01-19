// First load all dependencies
#r "netstandard"

#load "../../../.paket/load/netstandard2.1/main.group.fsx"
#load "../../../.paket/load/netstandard2.1/Testing/testing.group.fsx"

#load "../Continuation.fs"
#load "../Memoization.fs"
#load "../BCL/Char.fs"
#load "../Reflection.fs"

#time

open Informedica.GenUtils.Lib


module ContinuationTests =

    printfn "Tests will follow"



module MemoizationTests =

    let f1 x = x * 2

    let f2 x = x + "a"

    let f1mem = Memoization.memoize f1
    
    let f2mem = Memoization.memoize f2

    f1mem 2 |> ignore
    printfn "Memoized f1 with 2 = %i" (f1mem 2)


    f2mem "b" |> ignore
    printfn "Memoized f2 with b = %s" (f2mem "b")

    let rec fibs n = 
        if n < 1 then 1 else
        (fibs (n - 1)) + (fibs (n - 2))

    let fibsmem = Memoization.memoize fibs

    40 |> fibsmem |> printfn "fibs 40 = %i"
    40 |> fibsmem |> printfn "Memoized fibs 40 = %i"
    

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
