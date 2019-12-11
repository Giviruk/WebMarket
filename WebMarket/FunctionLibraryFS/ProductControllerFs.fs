namespace FunctionLibraryFS

open DataClassLibrary
open DataClassLibrary.DbContext;
open System.Linq
open Microsoft.EntityFrameworkCore
open System;

module ProductControllerFs =
    
    let GetaAllProductsList(context : AbstractDbContext) = 
        try
            Some(context.Products.ToList());
        with
            | _ -> None;


    let GetProductFromId(context: AbstractDbContext) (productId:int) =      
        try
            let mutable product = context.Products.FirstOrDefault(fun p -> p.Id = productId);
            let images = context.ProductImages.Where(fun pi -> pi.Productid.Value = product.Id).ToList();

            let defineImages = images.Select(fun i -> i.Image <- context.Images.FirstOrDefault(fun im -> im.Id = i.Id));
            defineImages |> ignore;

            product.MainpictureurlNavigation <- context.Images.Find(product.Mainpictureurl);
            product.ProductImages <- images;

            Some(product);
        with
            | _ -> None;
        
        

    let GetProductsFromCategory(context : AbstractDbContext) (categoryId:int) =
        try
            Some(context.Products.Where(fun p -> p.Id = categoryId).ToList());
        with
            | _ -> None;

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

    let AddProduct(context:AbstractDbContext) (newProduct:Product) =
        use trnsaction = context.Database.BeginTransaction() 
        try
                
            context.Products.Add(newProduct) |> ignore;
            context.SaveChanges() |> ignore;

            let newProductId = context.Products.ToList().LastOrDefault().Id;

            trnsaction.Commit() |> ignore;

            Some(newProductId);
        with
            | _ -> trnsaction.Rollback(); None;

    let DeleteProduct(context : AbstractDbContext) (productId : int) =
        try
            let product = context.Products.Find(productId);

            if isNull(product) then
                invalidArg "" ""

            context.Products.Remove(product) |>ignore;
            context.SaveChanges() |> ignore;

            Some(productId);
        with
            | _ -> None;



            
            

        
