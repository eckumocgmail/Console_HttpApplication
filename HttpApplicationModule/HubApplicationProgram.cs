using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Console_HubApplication
{
    public class ApplicationHub: Hub
    {

    }
    public class HubApplicationProgram: HubApplicationProgram<ApplicationHub>
    {

    }
    public class HubApplicationProgram<THub> where THub : Hub
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapDefaultControllerRoute();

                endpoints.MapHub<THub>($"/hubs/{typeof(THub).Name}");
            });
            app.Run(http =>
            {               
                return Task.CompletedTask;
            });
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<HubApplicationProgram>();
                });
    }
}
