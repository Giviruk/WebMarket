using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMarket.Controllers;

namespace WebMarket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<d6h4jeg5tcb9d8Context>(); //new
<<<<<<< HEAD
            //services.AddControllers();


            services.AddSingleton<ISingleton, SingletonDependency>();
            services.AddTransient<ITransient,TransientDependensy>();
            services.AddScoped<IScoped,ScopedDependency>();


=======
            //services.AddTransient<ITransientDependency,TransientDependency>();
            //services.AddSingleton<ISingletonDependency,SingletonDependency>();
            //services.AddScoped<IScopedDependency,ScopedDependency>();
>>>>>>> e5edf6bda4bd152bb873c328058491237197d3d6
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
