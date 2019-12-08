﻿using Microsoft.AspNetCore.Mvc;

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
                "https://webmarket911.herokuapp.com/api/orders/get/{userId} -get метод,который принимает на вход id пользователя и возвращает список его заказов \n";

            return controllers;
        }
    }    
}
