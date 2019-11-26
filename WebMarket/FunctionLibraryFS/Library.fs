namespace FunctionLibraryFS

open DataClassLibrary
open System.Collections
open System.Linq
open Microsoft.EntityFrameworkCore

module Say =
    let hello name =
        printfn "Hello %s" name


    let GetProductsFromCategoryFS(productsSet:DbSet<Product>) (categoryId:int) =
                productsSet.Select(fun p -> p.Id = categoryId);
        
