(*** hide ***)
#I "../../src/Informedica.GenUtils.Lib/Scripts"
#load "load-project-release.fsx"


(** 
## To use String functions

Open the *string basic core library` 
*)

open Informedica.GenUtils.Lib.BCL

(** 
Now all string utility functions are accessible as `String.someFunction`.

Example
*)

// Trim a string
"  Hello world.  " |> String.trim

(** 
Will return:

> val it : string = "Hellow World"

*)

(** 
## To use BigRational functions

Open the GenUtils lib
*)

open Informedica.GenUtils.Lib

(** 
Get the greatest common divisor of two bigrational string values
*)


// Use 4 utility functions to create a 'string gcd'
let gcd s1 s2 = 
    let s1 = s1 |> BigRational.parse
    let s2 = s2 |> BigRational.parse
    BigRational.gcd s1 s2
    |> BigRational.toString

// Get the greatest common divisor as a string
"20" |> gcd "30" 

(** 
Returns:

> val it : string = "10"

*)

// A safe version uses tryParse and returns an empty string if failure
let gcd2 s1 s2 =
    match s1 |> BigRational.tryParse, s2 |> BigRational.tryParse with
    | Some _, Some _ -> gcd s1 s2
    | _ -> ""

"Oops" |> gcd2 "30"

(** 
Returns:

> val it : string = ""

*)

(** 
## Use *Continuation* functions when possible

If there are *continuation* functions, use those instead of functions that can throw an error.

For example the `BigRational.parse` function had a *continuation* counterpart.
*)

type Result<'T, 'Msg> =
    | Succ of 'T
    | Fail of 'Msg list

let parse =
    let succ n = n |> Succ
    let fail m = [m] |> Fail
    BigRational.parseCont succ fail 

"blah" |> parse

(** 
Returns:

> val it : Result<BigRational,BigRational.Message> =
>  Fail [CannotParseString "blah"]

While
*)

"2" |> parse

(** 
Returns:

> val it : Result<BigRational,BigRational.Message> = Succ 2N

Thus, a success failure type monadic system can be build with continuation functions.
*)
