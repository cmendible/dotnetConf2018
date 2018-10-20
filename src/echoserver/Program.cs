using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace echoserver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .Configure(app =>
                    {
                        app.Run(httpContext =>
                        {
                            var request = httpContext.Request;
                            var response = httpContext.Response;

                            // Echo the Headers
                            foreach (var header in request.Headers)
                            {
                                System.Console.WriteLine($"Received Header from netConfBcn: {header.Key}");
                                response.Headers.Add(header);
                            }

                            // Echo the body
                            return request.Body.CopyToAsync(response.Body);
                        });
                    });
    }
}
