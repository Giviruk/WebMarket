namespace FunctionLibraryFS

open DataClassLibrary
open DataClassLibrary.DbContext;
open System.Linq
open Microsoft.EntityFrameworkCore

module ProductControllerFs =
    
    let GetaAllProductsList(context : AbstractDbContext) = 
        context.Products.ToList();

    let GetProductFromId(context: AbstractDbContext) (productId:int) =
        context.Products.FirstOrDefault(fun p -> p.Id = productId);

    let GetProductsFromCategoryFS(context : AbstractDbContext) (categoryId:int) =
                context.Products.Where(fun p -> p.Id = categoryId);

    let UpdateProduct(context: AbstractDbContext) (productId:int) (modifiedProduct:Product) = 
        try
            if modifiedProduct.Id <> productId || modifiedProduct.Category.HasValue then
                invalidArg "" ""

            context.Entry(modifiedProduct).State <- EntityState.Modified;

            let product = context.Products.Find(productId);

            product.Name <- modifiedProduct.Name;

            let update = context.Products.Update(modifiedProduct);
            update |> ignore;
            context.SaveChanges() |> ignore;

            modifiedProduct.Id.ToString();

        with
            | :? DbUpdateException -> "DbUpdateExeption";
            | _ -> "Not DbUpate exeption";
            

        
