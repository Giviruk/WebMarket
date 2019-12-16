namespace FunctionLibraryFS


open Source
open DataClassLibrary.Logic.Email
open System.Text
open System
open System.Linq
open DataClassLibrary.DbContext
open Microsoft.EntityFrameworkCore
open System.Collections.Generic

module ProfileControllerFs =
    
    //work
    let GeneratePasswordChangeKod(userEmail : string) =
        try
            let rnd = System.Random()

            let getRanSymbol(r:int) =
                let selector = r % 3
              
                match selector with
                | 0 -> Convert.ToChar(rnd.Next(48,57))
                | 1 -> Convert.ToChar(rnd.Next(65,90))
                | _ -> Convert.ToChar(rnd.Next(97,122))

            let GetRandomSymbols count =
                List.init count (fun _ -> getRanSymbol(rnd.Next()))
        
            let generedRandomSymbols = (GetRandomSymbols 8)

            let sendToUserEmail generedKod =
                EmailSender.SendEmail(userEmail,"Уважаемый пользователь","Код для смены пароля -> " + generedKod,"Смена пароля")
                generedKod

            let getSendPassKod =
                let sb = new StringBuilder()
                let genSymbolsToString =
                    for s in generedRandomSymbols do
                        sb.Append(s) |> ignore;
                    sb.ToString()
                sendToUserEmail genSymbolsToString               

            Some(getSendPassKod)
        with
            //| :? System.Exception as ex -> printf "%s" (ex.Message); None
            | _ -> None

    //work
    let ChangePassword(context : AbstractDbContext,userEmail: string,userPassword:string) =
        try
            let mutable user = context.Users.ToList().FirstOrDefault(fun u -> u.Login = userEmail)
            user.Pass <- userPassword
            context.SaveChanges() |> ignore
            context.Entry(user).State <- EntityState.Modified
            Some(user.Id)
        with
            |  :? System.Exception as ex -> printf "%s" (ex.Message); None
            | _ -> None
        

        

