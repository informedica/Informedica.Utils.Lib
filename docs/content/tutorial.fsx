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