using ChatService;
using Grpc.Core;
using System.Threading.Channels;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddGrpc();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//app.Run();

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();


    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });


}
