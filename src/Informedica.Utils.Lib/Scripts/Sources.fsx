
#I __SOURCE_DIRECTORY__

#r "nuget: MathNet.Numerics.FSharp"

#load "../Continuation.fs" 
#load "../Memoization.fs" 
#load "../Reflection.fs" 
#load "../NullCheck.fs" 
#load "../BCL/Char.fs" 
#load "../BCL/String.fs" 
#load "../BCL/Int32.fs" 
#load "../BCL/Double.fs" 
#load "../BCL/BigRational.fs" 
#load "../BCL/DateTime.fs" 
#load "../Option.fs" 
#load "../Array.fs" 
#load "../List.fs" 
#load "../Seq.fs" 
#load "../Path.fs" 
#load "../File.fs" 
#load "../App.fs" 

open System
open Informedica.Utils.Lib
open Informedica.Utils.Lib.BCL

BigRational.one

// ToDo: need to fix this
module Double =

    let getPrecision n f =
        try
            let n = if n < 0 then 0 else n
            if f = 0. || n = 0 then n
            else
                let s = (f |> abs |> string).Split([|'.'|])
                // calculate number of remaining decimal digits (after '.')
                let p = n - (if s.[0] = "0" then 0 else s.[0].Length)
                let p = if p < 0 then 0 else p
                printfn $"parse int: {s.[0]}"
                if (int s.[0]) > 0 then
                    p
                else
                    // calculate the the first occurance of a non-zero decimal digit
                    let c = (s.[1] |> String.countFirstChar '0')
                    c + p
        with
        | e ->
            printfn "cannot get precision %i for %f" n f 
            printfn "catching error %A" e
            printfn "returning 1 as default value"
            1




Double.getPrecision 1 0.00001

0.00001.ToString("F10")
