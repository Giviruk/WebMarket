namespace FunctionLibraryFS


open Source

open System.Linq
open DataClassLibrary.DbContext
open DataClassLibrary

module OrdersControllerFs =

    let GetAllOrders (context:AbstractDbContext) =
        try
            Some(context.Orders.ToList())
        with
            | _ -> None

    //work
    let GetOrderFromOrderId(context:AbstractDbContext,orderId) =
        try
            let mutable order = context.Orders.ToList().FirstOrDefault(fun o -> o.Id = orderId)

            let orderOwner = context.Users.ToList().FirstOrDefault(fun u -> u.Id = order.Owner.Value)
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
            | :? System.Exception as ex -> printf "%s" (ex.Message); None
            | _ -> None


