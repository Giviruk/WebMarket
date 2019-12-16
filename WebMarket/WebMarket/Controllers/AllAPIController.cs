using Microsoft.AspNetCore.Mvc;

namespace WebMarket.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AllAPIController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {

            var controllers =
                "https://webmarket911.herokuapp.com/api/categories/all  - все категории\n" +
                "https://webmarket911.herokuapp.com/api/categories/{id} - товары в категории\n" +
                "https://webmarket911.herokuapp.com/api/Others - не знаю что, надеюсь Артур пояснит(Артур пояснил)\n" +
                "https://webmarket911.herokuapp.com/api/others/{id} - не знаю что, надеюсь Артур пояснит\n" +
                "https://webmarket911.herokuapp.com/api/others/novelties - новинки\n" +
                "https://webmarket911.herokuapp.com/api/others/Bestsellers - бестселлеры\n" +
                "https://webmarket911.herokuapp.com/api/productimages -какие то картинки к продуктам\n" +
                "https://webmarket911.herokuapp.com/api/productimages/{id} - какая то картинка к продукту\n" +
                "https://webmarket911.herokuapp.com/api/products/all - все продукты?\n" +
                "https://webmarket911.herokuapp.com/api/products/product/{id} - конкретный продукт\n" +
                "https://webmarket911.herokuapp.com/api/products/category/{id} - не знаю что здесь\n" +
                "https://webmarket911.herokuapp.com/api/profile  - put создание профиля/get все профили\n" +
                "https://webmarket911.herokuapp.com/api/profile/{id} - конкретный профиль\n" +
                "https://webmarket911.herokuapp.com/api/profile/auth - post возвращает true/false проверка логина и пароля при авторизации\n" +
                "https://webmarket911.herokuapp.com/api/city - работа с городами, get - все, post - обновление, put - создание \n" +
                "https://webmarket911.herokuapp.com/api/Others/GetUserFromToken - post запрос,который принимает токен\n" +
                "https://webmarket911.herokuapp.com/api/reviews/{productId} - get метод принимает id продутка,возращает список отзывов \n" +
                "https://webmarket911.herokuapp.com/api/reviews - post метод принимает на вход объект типа review и добавляет его в бд \n" +
                "https://webmarket911.herokuapp.com/api/Others \n" +
                "https://webmarket911.herokuapp.com/api/search \n" +
                "https://webmarket911.herokuapp.com/api/orders - get(все ордеры),post - обновление, put - создание\n" +
                "https://webmarket911.herokuapp.com/api/orders/send - post метод,который принимает на вход объект типа OrderWithProductList \n" +
                "https://webmarket911.herokuapp.com/api/orders/getFromId/{userId} -get метод,который принимает на вход id пользователя и возвращает список его заказов \n" +
                "https://webmarket911.herokuapp.com/api/orders/getFromEmail/{userEmail} -get метод,который принимает на вход email пользователя и возвращает список его заказов,если пользователь зареган,кидает null \n" +
                "https://webmarket911.herokuapp.com/api/Categories/addCategory - post метод,который принимает на вход категорию,возвращает id новой категории.Если такое имя категории уже есть в бд, выкидывает ошибку. \n" +
                "https://webmarket911.herokuapp.com/api/Categories/delete/{id} - delete метод,принимает на вход id категории \n" +
                "https://webmarket911.herokuapp.com/api/Categories/update/{id} - put метод, принимает id категории и [FromBody]Category саму категорию \n" +
                "https://webmarket911.herokuapp.com/api/Products/update/{id} -put метод,принимает id категори и [FromBody]Product сам продукт \n" +
                "https://webmarket911.herokuapp.com/api/Products/delete/{id} -delete метод,который принимает на вход id товара и удаляет из бд \n" +
                "https://webmarket911.herokuapp.com/api/Products/addProduct -post метод,который добавляет новый продукт в бд и возвращает его id \n" +
                "https://webmarket911.herokuapp.com/api/profile/generateKod -post метод, который принимает email пользователя. Отправляет пользователю на почту код для смены пароля. Возвращает этот же код \n" +
                "https://webmarket911.herokuapp.com/api/profile/changePassword -post метод, который принимает на вход email пользователя и хэш нового пароля. Возвращает id пользователя \n" +
                "https://webmarket911.herokuapp.com/api/orders/getOrderFromOrderId/{orderId} - get метод,который возвращает объект типа Order по OrderId \n" +
                "https://webmarket911.herokuapp.com/api/orders/getAll - get метод,который возвращает все заказы \n" +
                "https://webmarket911.herokuapp.com/api/orders/update/{orderId} - put метод,который принимает на вход orderId и объект типа order. Отправляет оповещение по email \n" +
                "https://webmarket911.herokuapp.com/api/image/addProductImages/{productId} - put метод, который принимает на вход id продукта и List<Image> \n" + 
                "https://webmarket911.herokuapp.com/api/image/updateProducImages/{producId} - put метод, который принимает на вход id продукта и List<Image> сущуствующие в бд картинки не добавляет \n";


            //var controllers2 =


            return controllers;
        }
    }    
}
