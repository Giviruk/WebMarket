namespace FunctionLibraryFS

open System

module Source = 
    
    type MaybeBuilder() = 
        member this.Bind(x,f) =
            match x with
            | None -> None
            | Some a -> f a

        member this.Return(x) =
            Some x
            
