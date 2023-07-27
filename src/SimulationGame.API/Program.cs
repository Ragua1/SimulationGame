using SimulationGame.API.Controllers;

namespace SimulationGame.API;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateWebAppBuilder(args).Build();

        ConfigureApp(app);
        app.Run();
    }

    public static WebApplicationBuilder CreateWebAppBuilder(string[] args)
    {
        // ignore SSL errors
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //builder.Services.AddApplicationLayer();

        builder.Services.AddControllers().AddApplicationPart(typeof(WeatherForecastController).Assembly);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static void ConfigureApp(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
    }
}