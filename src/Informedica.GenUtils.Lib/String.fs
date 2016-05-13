namespace Informedica.GenUtils.Lib.BCL

/// Helper functions for `System.String`
module String = 

    open System

    let apply f (s: String) = f s
    
    let get = apply id

    let splitAt c s = (s |> get).Split([|c|]) 

    let contains c s = (s |> get).Contains(c) 

    let trim s = (s |> get).Trim()

    let toLower s = (s |> get).ToLower()

    let length s = (s |> get).Length

    let isNullOrWhiteSpace = String.IsNullOrWhiteSpace

    let replace (os: string) ns s = (s |> get).Replace(os, ns)

    let toString s = s.ToString()

    let empty (s: string) = System.String.IsNullOrWhiteSpace(s)

    let notEmpty = empty >> not

    let toUpper (s: string) = s.ToUpper()

    let substring start length (s: string) = s.Substring(start, length)

    /// Get the first character of a string
    /// as a string
    let firstStringChar = substring 0 1

    /// Return the rest of a string as a string
    let restString s = substring 1 ((s |> length) - 1) s

    let firstToUpper = firstStringChar >> toUpper

    let capitalize s = (s |> firstToUpper) + (s |> restString |> toLower)

    let letters = ['a'..'z'] @ ['A'..'Z'] |> List.map toString

    let isLetter s = List.exists (fun s' -> s' = s) letters

    let equalsCapInsens s1 s2 = s1 |> toLower |> trim = (s2 |> toLower |> trim) 

    let split (dels: string) (s: string) = s.Split(dels.ToCharArray()) |> Array.toList