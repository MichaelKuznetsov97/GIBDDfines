using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GIBDDfines.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace GIBDDfines
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            var connection = @"Server=DESKTOP-A3EOVHI;Database=modeldbGIBDD;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = @"Server=DESKTOP-A3EOVHI;Database=usersstoredb;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<modeldbGIBDDContext>(options => options.UseSqlServer(connection).ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning)));


            // добавление сервисов Idenity
            /*services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<modeldbGIBDDContext>();*/
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseStaticFiles();

            //app.UseAuthentication();

            app.UseMvc();           
        }
    }
}
