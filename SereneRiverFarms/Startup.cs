using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SereneRiverFarms
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            //Disabled for authentication reasons.  
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Alllow for Session Variables
            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });
        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error500");
                app.UseHsts();
            }

            //404-500 routing (has to come before app.UseMvc() to route to the 404 page
            app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Error{0}");
            //End of 404-500 routing

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); //For Sessions
            app.UseCookiePolicy();

            //Allow Authentication.  Important: this must be called before app.UseMVC();
            app.UseAuthentication();

            //Modified to allow Sessions
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");                   
            });
        }
    }
}
