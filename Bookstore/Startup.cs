using Microsoft.Extensions.Configuration;
using Bookstore.Application;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers(); 
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        DIConfiguration
    }
}