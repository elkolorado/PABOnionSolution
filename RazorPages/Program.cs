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
    webBuilder.UseStartup<Startup>()
              .ConfigureKestrel(options =>
              {
                  options.ConfigureEndpointDefaults(listenOptions =>
                  {
                      //listenOptions.UseHttps(); // Remove or comment out this line to disable HTTPS
                  });
              });
});


}
