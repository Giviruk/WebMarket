namespace FunctionLibraryFS

open DataClassLibrary
open System.Collections
open System.Linq

module Say =
    let hello name =
        printfn "Hello %s" name
    let funct(productsSet:Product) =
        printf "Hello %s" productsSet.Name
