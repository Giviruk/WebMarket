namespace FunctionLibraryFS

open DataClassLibrary
open DataClassLibrary.DbContext
open System.Linq
open Microsoft.EntityFrameworkCore
open Source

module CategoryControllerFs =

    let UpdateProduct(context: AbstractDbContext) (categoryId:int) (modifiedCategory:Category) = 
        try
            if modifiedCategory.Id <> categoryId then
                invalidArg "" ""


            let mutable category = context.Categories.Find(categoryId);

            category.Name <- modifiedCategory.Name;
            category.Characteristics <- modifiedCategory.Characteristics;
            category.Product <- modifiedCategory.Product;

            context.SaveChanges() |> ignore;

            context.Entry(category).State <- EntityState.Modified;


            Some(modifiedCategory.Id);

        with
            | :? DbUpdateException -> None;
            | _ -> None;

