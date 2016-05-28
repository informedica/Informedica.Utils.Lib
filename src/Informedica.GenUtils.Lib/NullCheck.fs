namespace Informedica.GenUtils.Lib

/// Function to perform a safe null check
module NullCheck =

    /// This is the F# 4 implementation of
    /// checking whether a value is null.
    [<CompiledName("IsNull")>]
    let inline isNull (value : 'T) = 
        match value with 
        | null -> true 
        | _ -> false


