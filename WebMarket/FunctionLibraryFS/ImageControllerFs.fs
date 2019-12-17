namespace FunctionLibraryFS

open System.Linq
open DataClassLibrary.DbContext
open DataClassLibrary
open DataClassLibrary.Logic.Email
open Microsoft.EntityFrameworkCore
open System.Text
open System

module ImageControllerFs =

    let DeleteImage(context : AbstractDbContext,imageId : int, productId : int) =
        try
            let deleteFromProductImages =
                context.ProductImages.Remove(context.ProductImages.ToList().FirstOrDefault(fun pi -> pi.Productid.Value = productId && pi.Imageid.Value = imageId))

            let deleteFromImages = 
                context.Images.Remove(context.Images.ToList().FirstOrDefault(fun i -> i.Id = imageId))

            deleteFromProductImages |> ignore
            context.SaveChanges() |> ignore
            deleteFromImages |> ignore
            context.SaveChanges() |>ignore
            Some()
        with
            | _ -> None;


    let AddProductImages(context : AbstractDbContext,images : System.Collections.Generic.List<Image>,productId : int) =
        use transaction = context.Database.BeginTransaction()
        try  
            
            let addImage(image:Image) = 
                context.Images.Add(image) |> ignore
                context.SaveChanges() |> ignore
                let getlastId =
                    context.Images  
                    |> Seq.toList
                    |> List.last
                getlastId.Id
                    
                

            let getImagesIdAndAddImages (imagesList : System.Collections.Generic.List<Image>) =
                let lst = [for i in images do yield (addImage(i))]
                lst

            let imagesIds = getImagesIdAndAddImages(images)

            let getLastPiId = 
               let lastPi = 
                    context.ProductImages.ToList().Select(fun pi -> pi.Id).ToList() 
                    |> Seq.toList
                    |> List.sort
                    |> List.last
               let i = lastPi + 1
               i
                

            let addProductImage(iId : int) =
                let productImage = new ProductImage()
                productImage.Imageid <- new System.Nullable<int>(iId)
                productImage.Productid <- new System.Nullable<int>(productId)
                context.ProductImages.Add(productImage) |> ignore
                context.SaveChanges() |> ignore

            let addProductImagesInDb =
                for imageId in imagesIds do
                    addProductImage(imageId)
               
            transaction.Commit() |> ignore
            Some(addProductImagesInDb)
        with
            | :? System.Exception as ex -> printfn "%s" (ex.Message);transaction.Rollback(); None
            | _ -> transaction.Rollback();None


    let UpdateProductImages(context : AbstractDbContext,images : System.Collections.Generic.List<Image>,productId : int) =
        try
            let dbImagesUrl = context.Images.ToList().Select(fun i -> i.Imagepath).ToList();
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

