using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            File.WriteAllText(
                "./server.pid", 
                Process.GetCurrentProcess().Id.ToString());

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {            
            app.Run(async (context) =>
            {
                var response = context.Response;
                response.ContentType = "application/json";
                await response.WriteAsync(
                    "{ \"status\": true, \"statusDate\":  \"" + DateTime.Now.ToString() + "\"}"
                    );
                                                               
            });
        }
    }
}
