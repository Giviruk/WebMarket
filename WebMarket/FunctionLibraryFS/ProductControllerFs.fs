namespace FunctionLibraryFS

open DataClassLibrary
open System.Collections
open System.Linq
open Microsoft.EntityFrameworkCore

module ProductControllerFs =
    let hello name =
        printfn "Hello %s" name

    let GetProductsFromCategoryFS(productsSet:DbSet<Product>) (categoryId:int) =
                productsSet.Where(fun p -> p.Id = categoryId);

        
