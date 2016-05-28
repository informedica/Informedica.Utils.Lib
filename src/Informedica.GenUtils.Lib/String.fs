namespace Informedica.GenUtils.Lib.BCL

/// Helper functions for `System.String`
module String = 

    open System

    /// Apply `f` to string `s`
    let apply f (s: String) = f s
    
    /// Utility to enable type inference
    let get = apply id

    /// Split string `s` at character `c`
    let splitAt c s = (s |> get).Split([|c|]) 

    /// Check if string `s2` contains string `s1`
    let contains s1 s2 = (s2 |> get).Contains(s1) 

    /// Trim string `s`
    let trim s = (s |> get).Trim()

    /// Make string all lower chars
    let toLower s = (s |> get).ToLower()

    /// Make string all upper chars
    let toUpper (s: string) = s.ToUpper()

    /// Get the length of s
    let length s = (s |> get).Length

    /// Check if string is null or only white space
    let isNullOrWhiteSpace = String.IsNullOrWhiteSpace

    /// Check if string is null or only white space
    let empty (s: string) = System.String.IsNullOrWhiteSpace(s)

    /// Check if string is not null or only white space
    let notEmpty = empty >> not

    /// Replace `os` with `ns` in string `s`.
    let replace (os: string) ns s = (s |> get).Replace(os, ns)

    /// Convert object to string
    let toString s = s.ToString()

    /// Get a substring starting at `start` with length `length`
    let substring start length (s: string) = s.Substring(start, length)

    /// Get the first character of a string
    /// as a string
    let firstStringChar = substring 0 1

    /// Return the rest of a string as a string
    let restString s = substring 1 ((s |> length) - 1) s

    /// Make the first char of a string upper case
    let firstToUpper = firstStringChar >> toUpper

    /// Make the first character upper and the rest lower of a string
    let capitalize s = (s |> firstToUpper) + (s |> restString |> toLower)

    /// Get all letters as a string list
    let letters = ['a'..'z'] @ ['A'..'Z'] |> List.map toString

    /// Check if a string is a letter
    let isLetter s = List.exists (fun s' -> s' = s) letters

    let equals s1 s2 = s1 = s2

    /// Check if string `s1` equals `s2` caps insensitive
    let equalsCapInsens s1 s2 = s1 |> toLower |> trim = (s2 |> toLower |> trim) 

    /// Split a string `s` at string `dels`
    let split (dels: string) (s: string) = s.Split(dels.ToCharArray()) |> Array.toList

    /// Check whether **s1** starts with
    /// **s2** using string comparison **eqs**
    let startsWithEqs eqs s2 s1 =
        if s2 |> String.length > (s1 |> String.length) then false
        else
            s1 |> substring 0 (s2 |> String.length) |> eqs s2

    /// Check whether **s1** starts with
    /// **s2** caps sensitive
    let startsWith s2 s1 = startsWithEqs equals

    /// Check whether **s1** starts with
    /// **s2** caps insensitive
    let startsWithCapsInsens = startsWithEqs equalsCapInsens