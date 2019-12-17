namespace FunctionLibraryFS

open System.Linq
open DataClassLibrary.DbContext
open DataClassLibrary
open DataClassLibrary.Logic.Email
open Microsoft.EntityFrameworkCore
open System.Text

module OrdersControllerFs =


    let UpdateOrder(context:AbstractDbContext)(orderId : int)(modifidOrder:Order) =
        try     
            if modifidOrder.Id <> orderId then
                invalidArg "" ""

            let mutable order = context.Orders.Find(orderId)

            order.Status <- modifidOrder.Status

            context.SaveChanges() |> ignore
            context.Entry(order).State <- EntityState.Modified

            let orderProducts = 
                let productsId = 
                    context.OrderProducts.ToList().Where(fun op -> op.Orderid.Value = order.Id).Select(fun op -> op.Productid.Value).ToList()
                let products = context.Products.ToList().Where(fun p -> productsId.Contains(p.Id)).Distinct().ToList()

                //let pq = 
                //   query{
                //        for p in products do
                //        groupBy p into group
                //        where(true)
                //        select (group.Key,group.Count)
                //    }
                products
                //pq

            let NotifyUser =
                let messageSubject= 
                    let sb =new StringBuilder()
                    sb.Append("Изменение статуса заказа") |> ignore
                    sb.ToString()
                let messabeBody = 
                    let status = context.Statuses.ToList().FirstOrDefault(fun s -> s.Id = order.Status.Value).Name
                    let sb = new StringBuilder()
                    sb.AppendFormat("Статус ващего заказа был именен на \"{1}\"\n\n Номер заказа : {0}\n\n", order.Id,status) |> ignore
                    sb.Append("Список продкутов: \n\n") |> ignore
                    let productInOrder =
                        for p in orderProducts do
                            sb.AppendFormat("{0} \n",p.Name) |> ignore
                        sb.ToString()
                    productInOrder
                let send = EmailSender.SendEmail(order.Email,"Уважаемый пользователь",messabeBody,messageSubject); 0
                send 
            Some(NotifyUser)
        with
        //| :? System.Exception as ex -> printf "%s" (ex.Message); None
        | _ -> None



                
    //work
    let GetAllOrders (context:AbstractDbContext) =
        try
            Some(context.Orders.ToList())
        with
            | _ -> None

    //work
    let GetOrderFromOrderId(context:AbstractDbContext,orderId) =
        try
            let mutable order = context.Orders.ToList().FirstOrDefault(fun o -> o.Id = orderId)

            let orderOwner =           
                if order.Owner.HasValue then
                    let ow = context.Users.ToList().FirstOrDefault(fun u -> u.Id = order.Owner.Value)
                    let owner = 
                        let mutable o = new User()
                        o.Id <- ow.Id
                        o.Address <- ow.Address
                        o.Firstname <- ow.Firstname
                        o.Lastname <- ow.Lastname
                        o.Login <- ow.Login
                        o
                    owner
                else
                    null      

            let orderStatus = context.Statuses.ToList().FirstOrDefault(fun s -> s.Id = order.Status.Value)
            let orderProducts = context.OrderProducts.ToList().Where(fun op -> op.Orderid.Value = order.Id).ToList()

            let status = 
                let mutable s = new Status()
                s.Id <- orderStatus.Id
                s.Name <- orderStatus.Name
                s

            order.OwnerNavigation <- orderOwner
            order.StatusNavigation <- status
            order.Productinorder <- orderProducts
            

            Some(order)
        with
            //| :? System.Exception as ex -> printf "%s" (ex.Message); None
            | _ -> None


