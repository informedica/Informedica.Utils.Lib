namespace Informedica.Utils.Lib.BCL

/// Helper functions for `BigRational`
[<AutoOpen>]
module BigRational = 

    open System
    open MathNet.Numerics


    /// Message type to be used when
    /// an exception message is warranted
    type Message =
        | CannotMatchOperator
        | CannotDivideByZero
        | CannotParseString of string


    /// Exception type
    exception BigRationalException of Message


    /// Raise exception with message `m`
    let raiseExc m = m |> BigRationalException |> raise


    /// Apply a `f` to bigrational `x`
    let apply f (x: BigRational) = f x


    /// Utility to enable type inference
    let get = apply id


    /// Parse a string to a bigrational
    /// Raises an exception `Message` when
    /// the string cannot be parsed
    let parse s = 
        try 
            s |> BigRational.Parse
        with
        | _ -> s |> CannotParseString |> raiseExc


    /// Parse a string and pass the result 
    /// either to `succ` or `fail` function
    let parseCont succ fail s =
        try 
            parse s |> succ
        with
        | BigRationalException(m) -> m |> fail


    /// Try to parse a string and 
    /// return `None` if it fails 
    /// otherwise `Some` bigrational
    let tryParse = 
        parseCont Some (fun _ -> None)


    /// Create a bigrational from an int
    let fromInt = BigRational.FromInt


    /// Get the greatest common divisor
    /// of two bigrationals `a` and `b`
    let gcd (a : BigRational) (b: BigRational) =
        let den = a.Denominator * b.Denominator
        let rec gcd' a' b' =
            match b' with
            | _  when b' = 0I -> abs a'
            | _ -> gcd' b' (a' % b')
        let num = gcd' (a.Numerator * b.Denominator) (b.Numerator * a.Denominator)
        (num |> BigRational.FromBigInt) / (den |> BigRational.FromBigInt)


    /// Convert a bigrational to a string
    let toString v = (v |> get).ToString()


    /// Convert an optional `BigRational` to a `string`.
    /// If `None` then return empty `string`.
    let optToString = function
        | Some v' -> v' |> toString
        | None    -> ""


    /// Convert `n` to a multiple of `d`.
    /// Passes an `CannotDivideByZero` message
    /// to `fail` when `d` is zero.
    let toMultipleOfCont succ fail d n  =
        if d = 0N then CannotDivideByZero |> fail
        else
            let m = (n / d) |> BigRational.ToBigInt |> BigRational.FromBigInt
            if m * d < n then (m + 1N) * d else m * d
            |> succ


    /// Convert `n` to a multiple of `d`.
    /// Raises an `CannotDivideByZero` message
    /// exception when `d` is zero.
    let toMultipleOf = toMultipleOfCont id raiseExc

    /// Convert `n` to a multiple of `d`.
    /// Returns `None` when `d` is zero.
    let toMultipleOfOpt = toMultipleOfCont Some (fun _ -> None)


    /// Checks whether `v` is a multiple of `incr`
    let isMultiple incr v =
        if incr = 0N then false
        else
            let incr, v = incr |> get, v |> get 
            (v.Numerator * incr.Denominator) % (incr.Numerator * v.Denominator) = 0I

    /// Constant 0
    let zero = 0N

    /// Constant 1
    let one = 1N

    /// Constant 2
    let two = 2N

    /// Constant 3
    let three = 3N

    /// Check whether the operator is subtraction
    let opIsSubtr op = (three |> op <| two) = three - two // = 1

    /// Check whether the operator is addition
    let opIsAdd op   = (three |> op <| two) = three + two // = 5

    /// Check whether the operator is multiplication
    let opIsMult op  = (three |> op <| two) = three * two // = 6

    /// Check whether the operator is divsion
    let opIsDiv op   = (three |> op <| two) = three / two // = 3/2

    /// Match an operator `op` to either
    /// multiplication, division, addition
    /// or subtraction. </br> 
    /// Returns NoMatch otherwise
    let (|Mult|Div|Add|Subtr|NoMatch|) op =
        match op with
        | _ when op |> opIsMult  -> Mult
        | _ when op |> opIsDiv   -> Div
        | _ when op |> opIsAdd   -> Add
        | _ when op |> opIsSubtr -> Subtr
        | _ -> NoMatch


    /// Try to convert a float `f` to
    /// a `BigRational`.
    let fromFloat f =
        f
        |> Double.floatToFract
        |> Option.bind (fun (n, d) -> BigRational.FromBigInt(n) / BigRational.FromBigInt(d) |> Some)


    /// Convert a BigRational to a float
    let toFloat br =
        ((br |> get).Numerator |> float) / (br.Denominator |> float)

    /// Perform a calculation when 
    /// both `n1` and `n2` are 'some'
    let calculate n1 o n2 = 
        match n1, n2 with
        |Some x1, Some x2 -> x1 |> o <| x2 |> Some
        |_ -> None


    let inline triangular n = (n * (n + (n/n))) / ((n + n) / n)  


    let farey n asc =
        seq {
            let p = if asc then ref 0I else ref 1I
            let q = ref 1I
            let p' = if asc then ref 1I else ref (n - 1I)
            let q' = ref n
            yield (!p, !q)
            while (asc && not (!p = 1I && !q = 1I)) || (not asc && !p > 0I) do
                let c = (!q + n) / !q'
                let p'' = c * !p' - !p
                let q'' = c * !q' - !q
                p := !p'
                q := !q'
                p' := p''
                q' := q''
                yield (!p, !q) }


    let calcConc max conc =
        seq { for f in (farey max false) do
                let fn, fd = f
                let r = (fn |> BigRational.FromBigInt) / (fd |> BigRational.FromBigInt)
                yield r * conc } |> Seq.cache


    let rec BigPow (a:bigint) (p:bigint) :bigint =
      match p with
        | _ when (p = 0I) -> 1I
        | _ when (p >= 1I) -> a * (BigPow (a) (p - 1I))
        | _ -> failwith "Shouldn't Happen"


    let ( ** ) : bigint -> bigint -> bigint = BigPow


    let inline divisorsOfN2 zero one two n =
        let n = abs n
        match n with
        | _ when n = zero-> []
        | _ -> List.append ([one..(n/two)] |> List.filter(fun x -> n % x = zero)) [n]


    let divisorsOfBigInt = divisorsOfN2 0I 1I 2I


    let divisorsOfN n = 
        let n = abs n
        match n with
        | _ when n = 0N-> []
        | _ -> List.append ([1N..(n/2N)] |> List.filter(fun x -> (n.Numerator % x.Numerator) = 0I)) [n]


    let inline isDividerOf3 zero dividend divider =
        dividend % divider = zero


    let isDividerOf  (dividend:BigRational) (divider:BigRational) = isDividerOf3 0I dividend.Numerator divider.Numerator


    let isDividerOf2 (dividend:bigint)      (divider:bigint)      = isDividerOf3 0I dividend           divider


    let reduceRatio n d = 
        let num   = n / (gcd n d)
        let denom = d / (gcd n d)
        (num, denom)        


    let numdenom (v:BigRational) = (v.Numerator |> BigRational.FromBigInt, v.Denominator |> BigRational.FromBigInt) 


    let numdenomRatio (v:BigRational) = (v.Numerator |> BigRational.FromBigInt, v.Denominator |> BigRational.FromBigInt)


    let valueToFactorRatio v r =
        let vn, vd = numdenomRatio v
        let toBigR = BigRational.FromBigInt

        match r with
        | (Some n, true,  Some d, true)                            -> (n, d)
        | (Some n, false, Some d, false)                           -> let r = (vn * d) / (vd * n)
                                                                      ((r.Numerator |> toBigR) * n), ((r.Denominator |> toBigR) * d)  
        | (None   , _ ,   Some d, true) when (vd |> isDividerOf d) -> (vn * (d / vd), d)
        | (None   , _ ,   Some d, false)                           -> ((d / (gcd d vd)) * vn, (d / (gcd d vd)) * vd)
        | (Some n, true,  None,   _ )   when (vn |> isDividerOf n) -> (n, (n / vn) * vd)  
        | (Some n, false, None,   _ )                              -> ((n / (gcd n vn)) * vn, (n / (gcd n vn)) * vd) 
        | (None,   _ ,    None,   _ )                              -> (vn, vd)                              
        | _                                                        -> (0N, 0N)


    let valueToFactorRatio2 v r = 
        let n, nv, d, dv = r
        let toBigR x = match x with |Some i -> i |> BigRational.FromBigInt |> Some |None -> None
        let n, d = (n |> toBigR, nv, d |> toBigR, dv) |> valueToFactorRatio v
        (n.Numerator, d.Numerator)


    let toNumListDenom (vl: BigRational list) =
        let d = 
            vl |> List.map(fun v -> v.Denominator)
            |> Seq.distinct
            |> Seq.toList
            |> Seq.fold(fun p d -> d * p) 1I
            |> BigRational.FromBigInt
        (vl |> List.map(fun v -> v * d), d)      
