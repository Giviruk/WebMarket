namespace FunctionLibraryFS

open DataClassLibrary
open DataClassLibrary.DbContext
open System.Linq
open Microsoft.EntityFrameworkCore
open System.Collections
open System.Linq;
open Source

module ProductControllerFs =
    
    //work
    let GetaAllProductsList(context : AbstractDbContext) = 
        try
            Some(context.Products.ToList());
        with
            | _ -> None;


    let GetProductFromId(context: AbstractDbContext) (productId:int) =      
        try
            let maybe = new MaybeBuilder()
            let mutable product = context.Products.ToList().FirstOrDefault(fun p -> p.Id = productId)

            let IsSuitableImages (productImage : ProductImage) =
                productImage.Productid.Value = product.Id

            let  GetProductImages =
                let productImagesSet = context.ProductImages
                let producImagesList =
                    productImagesSet
                    |> Seq.toList
                    |> List.filter IsSuitableImages
                let defineImages (images : DbSet<Image>)   =
                    let suitableImageIds = producImagesList.Select(fun pi -> pi.Imageid.Value)    
                    let IsInProducImage (image : Image) =
                        suitableImageIds.Contains(image.Id)
                    images
                    |> Seq.toList
                    |> List.filter IsInProducImage
                defineImages(context.Images) |> ignore
                Some(producImagesList)

            let GetProductReviews =
                let IsSuitableReview (reviw : Review) =
                    reviw.ProductId = product.Id
                let reviews = 
                    context.Reviews
                    |> Seq.toList
                    |> List.filter IsSuitableReview
                reviews


    
            let castListToICollection (list :  'T list) =
                    let cast = ResizeArray<'T> list
                    cast

            let result = maybe {
                let! res = GetProductImages
                return res
            }
                            
            
            product.MainpictureurlNavigation <- context.Images.Find(product.Mainpictureurl)
            product.ProductImages <-  castListToICollection(result.Value)
            product.Review <- castListToICollection(GetProductReviews)

            Some(product)
        with
            | :? System.Exception as ex -> printfn "%s" (ex.Message); None
            | _ -> None
        
        
    //work
    let GetProductsFromCategory(context : AbstractDbContext) (categoryId:int) =
        try
            let IsProductInCategory (p : Product) = 
                p.Category.Value = categoryId

            let getProductsInCategory (productSet : DbSet<Product>) =
                let productList =
                    productSet 
                    |> Seq.toList
                    |> List.filter IsProductInCategory
                productList
                    
            Some(getProductsInCategory(context.Products));
        with
            | _ -> None;
    
    //work
    let UpdateProduct(context: AbstractDbContext) (productId:int) (modifiedProduct:Product) = 
        try
            if modifiedProduct.Id <> productId || not modifiedProduct.Category.HasValue then
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
    
    //work
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
    //work
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



            
            

        
