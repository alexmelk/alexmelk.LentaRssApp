using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using alexmelk.LentaRss.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace alexmelk.LentaRss
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string ClientRootPath = "Client/app/dist";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new LentaRss.Services.LentaRss("https://lenta.ru/rss/"));
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = ClientRootPath; });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(configuration =>
            {
                configuration.Options.SourcePath = ClientRootPath;
                configuration.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/" + ClientRootPath)
                };
            });

        }
    }
}
