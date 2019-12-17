namespace FunctionLibraryFS

open System.Linq
open DataClassLibrary.DbContext
open DataClassLibrary
open DataClassLibrary.Logic.Email
open Microsoft.EntityFrameworkCore
open System.Text

module ReviewControollerFs =
        let GetProductReviews(context:AbstractDbContext,productId : int) =     
            try
                let IsSuitableReview(review:Review) =
                    review.ProductId = productId

                let productReview =
                    context.Reviews
                    |> Seq.toList
                    |> List.filter IsSuitableReview

                let castListToICollection (list :  'T list) =
                    let cast = ResizeArray<'T> list
                    cast
                   
                Some(castListToICollection(productReview))                  
            with
                | _ -> None

        let AddProduct(context:AbstractDbContext,review :Review) =
            try
                context.Reviews.Add(review) |> ignore
                context.SaveChanges() |> ignore

                Some()
            with
                | _ -> None

        


