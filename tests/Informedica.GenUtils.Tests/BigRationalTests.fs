namespace Informedica.GenUtils.Tests

open Swensen.Unquote
open NUnit.Framework
open FsCheck
open FsCheck.NUnit

open Informedica.GenUtils.Lib


/// Create the necessary test generators
module Generators =

    let bigRGen (n, d) = 
            let d = if d = 0 then 1 else d
            let n' = abs(n) |> BigRational.FromInt
            let d' = abs(d) |> BigRational.FromInt
            n'/d'

    let bigRGenerator =
        gen {
            let! n = Arb.generate<int>
            let! d = Arb.generate<int>
            return bigRGen(n, d)
        }

    type MyGenerators () =
        static member BigRational () =
            { new Arbitrary<BigRational>() with
                override x.Generator = bigRGenerator }


[<SetUpFixture>]
type Config () =
    
    /// Make sure the generators are
    /// registered before running any
    /// test code.
    [<SetUp>]
    member x.Setup () = 

        Arb.register<Generators.MyGenerators>() |> ignore

module BigRationalTests =

    let checkOp op f b =  
        test <@ op |> f = b @>

    [<TestFixture>]
    type ``Given operator is multiplication`` () =
        
        let check = checkOp (*)

        [<Test>]
        member x.``Then is Multiplication returns true`` () =
            check BigRational.opIsMult true


        [<Test>]
        member x.``Then is Division returns false`` () =
            check BigRational.opIsDiv false

        [<Test>]
        member x.``Then is Addition returns false`` () =
            check BigRational.opIsAdd false

        [<Test>]
        member x.``Then is Subtraction returns false`` () =
            check BigRational.opIsSubtr false


    [<TestFixture>]
    type ``Given operator is division`` () =
        
        let check = checkOp (/)

        [<Test>]
        member x.``Then is Multiplication returns false`` () =
            check BigRational.opIsMult false


        [<Test>]
        member x.``Then is Division returns true`` () =
            check BigRational.opIsDiv true

        [<Test>]
        member x.``Then is Addition returns false`` () =
            check BigRational.opIsAdd false

        [<Test>]
        member x.``Then is Subtraction returns false`` () =
            check BigRational.opIsSubtr false



    [<TestFixture>]
    type ``Given operator is addition`` () =
        
        let check = checkOp (+)

        [<Test>]
        member x.``Then is Multiplication returns false`` () =
            check BigRational.opIsMult false


        [<Test>]
        member x.``Then is Division returns false`` () =
            check BigRational.opIsDiv false

        [<Test>]
        member x.``Then is Addition returns true`` () =
            check BigRational.opIsAdd true

        [<Test>]
        member x.``Then is Subtraction returns false`` () =
            check BigRational.opIsSubtr false


    [<TestFixture>]
    type ``Given operator is subtraction`` () =
        
        let check = checkOp (-)

        [<Test>]
        member x.``Then is Multiplication returns false`` () =
            check BigRational.opIsMult false


        [<Test>]
        member x.``Then is Division returns false`` () =
            check BigRational.opIsDiv false

        [<Test>]
        member x.``Then is Addition returns false`` () =
            check BigRational.opIsAdd false

        [<Test>]
        member x.``Then is Subtraction returns true`` () =
            check BigRational.opIsSubtr true

    let checkMult n d =
        if d = 0N then true
        else
            n 
            |> BigRational.toMultipleOf d
            |> BigRational.isMultiple d

    [<TestFixture>]
    type ``Given two numbers n and d`` () =
        
        [<Property>]
        member x.``When n is converted to multiple of d it is a multiple of d`` () =
            checkMult

        [<Test>]
        member x.``Throws a cannot divide by zero message exception if d is 0`` () =
            test <@ try 
                        1N |> BigRational.toMultipleOf 0N |> ignore
                        false
                    with
                    | BigRational.BigRationalException(m) ->
                        m = BigRational.Message.CannotDivideByZero @>

    [<TestFixture>]
    type ``Given a string blah`` () =
        
        [<Test>]
        member x.``Then parse will fail with cannot parse string message`` () = 
            test <@ try
                        "blah" |> BigRational.parse |> ignore
                        false
                    with
                    | BigRational.BigRationalException(m) ->
                        match m with
                        | BigRational.Message.CannotParseString(s) -> s = "blah"
                        | _ -> false @>