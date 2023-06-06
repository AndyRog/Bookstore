using Microsoft.Extensions.Configuration;
using Bookstore.Application;
using Bookstore.Application.Contracts;
using Bookstore.Ifrastructure.Repositories;
using Bookstore.Ifrastructure;
using Microsoft.AspNetCore.Identity;

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
        DIConfiguration.RegisterServices(services);
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddDbContext<ApplicationDbContext>();
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
         {
            options.SignIn.RequireConfirmedAccount = false;
         }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "auth_cookie";
            options.Cookie.SameSite = SameSiteMode.None;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };

        });

        services.AddScoped<IdentitySeed>();

    }

    
}