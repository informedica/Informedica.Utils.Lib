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



module BigRationalTests =

    let checkOp op f b =  
        test <@ op |> f = b @>

    [<TestFixture>]
    type ``Given a multiplication`` () =
        
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
    type ``Given a division`` () =
        
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
    type ``Given a addition`` () =
        
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
    type ``Given a subtraction`` () =
        
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

