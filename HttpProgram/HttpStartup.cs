using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;
namespace eckumoc_netcore_files
{





    public class HttpStartup 
    {


        


        public HttpStartup(IConfiguration config)
        {

        }

   

        public void ConfigureServices(IServiceCollection services)
        {            
        }

        public void Configure(IApplicationBuilder app  )
        {
            app.Run((http)=>
            {
                string code = @"new EventSource('/')";
                JavaScriptAgentAppWithUI(http, code);
                return Task.CompletedTask;
            });
        }

        private static void JavaScriptAgentAppWithUI(HttpContext http, string name)
        {
            http.Response.ContentType = "text/html;encoding=UTF-8";            
            http.Response.WriteAsync(@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='ASCII' />
                        <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                        <link rel='icon' href='logo' />
                    </head>
                    <body>
                        <div class='card text-center'>
                            <div class='card-header'>
                                <ul class='nav nav-tabs card-header-tabs'>
                                    <li class='nav-item'>
                                        <a class='nav-link active' aria-current='true' href='#'>Active</a>
                                    </li>                       
                                </ul>
                            </div>
                            <div class='card-body'>
                                <h5 class='card-title'>События</h5>
                                <p class='card-text'></p>
                                <a href='#' class='btn btn-primary'>Go somewhere</a>
                            </div>
                        </div>
                    </body>
                    </html>");
        }



        
    }
}
