namespace FunctionLibraryFS

open System.Linq
open DataClassLibrary.DbContext
open DataClassLibrary
open DataClassLibrary.Logic.Email
open Microsoft.EntityFrameworkCore
open System.Text
open System

module ImageControllerFs =

    let AddProductImages(context : AbstractDbContext,images : System.Collections.Generic.List<Image>,productId : int) =
        use transaction = context.Database.BeginTransaction()
        try  
            
            let getImagesIdAndAddImages (imagesList : System.Collections.Generic.List<Image>) =
                let lst = [for i in images -> (context.Images.Add(i)|>ignore;context.SaveChanges()|>ignore;context.Images.LastOrDefault().Id)]
                lst
             
            
            let addProductImagesInDb =
                for imageId in getImagesIdAndAddImages(images) do
                    let mutable productImage = new ProductImage()
                    productImage.Imageid <- new System.Nullable<int>(imageId)
                    productImage.Productid <- new System.Nullable<int>(productId)
                    context.ProductImages.Add(productImage) |> ignore
                    context.SaveChanges() |> ignore
               
            transaction.Commit() |> ignore
            Some(addProductImagesInDb)
        with
            | _ -> transaction.Rollback();None


    let UpdateProductImages(context : AbstractDbContext,images : System.Collections.Generic.List<Image>,productId : int) =
        try
            let dbImagesUrl = context.Images.Select(fun i -> i.Imagepath).ToList();
            let IsSutableImage (image : Image) =
                 let b = dbImagesUrl.Contains(image.Imagepath)
                 not(b); 

            let suitableImages(images :  System.Collections.Generic.List<Image>) =
                let sImages =
                    images
                    |> Seq.toList
                    |> List.filter IsSutableImage
                sImages
                

            let castListToICollection(list : 'T list) =
                let cast = ResizeArray<'T> list
                cast

            Some(AddProductImages(context,castListToICollection(suitableImages(images)),productId).Value)
        with
            | :? System.Exception as ex -> printfn "%s" (ex.Message); None
            | _ -> None

