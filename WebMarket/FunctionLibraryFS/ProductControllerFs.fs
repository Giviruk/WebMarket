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

            let mutable product = context.Products.Find(productId);

            product.Category <- modifiedProduct.Category;
            product.CategoryNavigation <- modifiedProduct.CategoryNavigation;
            product.Characteristics <- modifiedProduct.Characteristics;
            product.Description <- modifiedProduct.Description;
            product.Mainpictureurl <- modifiedProduct.Mainpictureurl;
            product.MainpictureurlNavigation <- modifiedProduct.MainpictureurlNavigation;
            product.Name <- modifiedProduct.Name;
            product.OrderProducts <- modifiedProduct.OrderProducts;
            product.Price <- modifiedProduct.Price;
            product.Producer <- modifiedProduct.Producer;
            product.ProductImages <- modifiedProduct.ProductImages;
            product.ProductRating <- modifiedProduct.ProductRating;
            product.Review <- modifiedProduct.Review;

            context.SaveChanges() |> ignore;
            
            context.Entry(product).State <- EntityState.Modified;

            Some(modifiedProduct.Id);

        with
            | :? DbUpdateException -> None;
            | _ -> None;
            
            

        
