namespace FunctionLibraryFS

open DataClassLibrary
open System.Linq
open Microsoft.EntityFrameworkCore

module ProductControllerFs =
    

    let GetProductFromId(productSet:DbSet<Product>) (productId:int) =
        productSet.FirstOrDefault(fun p -> p.Id = productId);

    let GetProductsFromCategoryFS(productsSet:DbSet<Product>) (categoryId:int) =
                productsSet.Where(fun p -> p.Id = categoryId);

        
