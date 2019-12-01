using Microsoft.AspNetCore.Mvc;

namespace WebMarket.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AllAPIController : ControllerBase
    {
        //private TransientDependency _td;
        //private SingletonDependency _sd;
        //private ScopedDependency _scd;
        //public AllAPIController
        //    (
        //        TransientDependency td, 
        //        SingletonDependency sd,
        //        ScopedDependency scd
        //    )
        //{
        //    _td = td;
        //    _sd = sd;
        //    _scd = scd;
        //}
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
                "https://webmarket911.herokuapp.com/api/Others \n" +
                "https://webmarket911.herokuapp.com/api/search \n";

            return controllers;
            //return _td.GetString() + " \t" + _sd.ToString() + " \t" + _scd.GetString();
        }
    }

    public interface ITransientDependency
    {
        string GetString();
        string ToString();
    }

    public class TransientDependency : ITransientDependency
    {
        private SingletonDependency singleton;
        
        public TransientDependency(SingletonDependency sd)
        {
            singleton = sd;
        }

        public string GetString()
        {
            return singleton.ToString() + " " + this.ToString();
        }

        public override string ToString()
        {
            return "Transient";
        }
    }

    public interface ISingletonDependency
    {
        string ToString();
    }

    public class SingletonDependency : ISingletonDependency
    {
        public SingletonDependency()
        {
            
        }

        public override string ToString()
        {
            return "Singleton";
        }
    }

    public interface IScopedDependency
    {
        string GetString();
        string ToString();
    }

    public class ScopedDependency : IScopedDependency
    {
        private SingletonDependency singleton;

        public ScopedDependency(SingletonDependency sd)
        {
            singleton = sd;
        }

        public string GetString()
        {
            return singleton.ToString() + " " + this.ToString();
        }
        public override string ToString()
        {
            return "Scoped";
        }
    }
    
}
