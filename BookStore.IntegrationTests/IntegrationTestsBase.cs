using AutoMapper;
using Bookstore.Application;
using Bookstore.Ifrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.IntegrationTests;

public class IntegrationTestsBase : IClassFixture<WebApplicationFactory<Startup>>
{


    protected WebApplicationFactory<Startup> Factory { get; }
    protected HttpClient Client { get; }
    protected ApplicationDbContext DbContext { get; }
    protected IMapper Mapper { get; }



    public IntegrationTestsBase(WebApplicationFactory<Startup> factory) 
    {
        Environment.SetEnvironmentVariable("ADMIN_EMAIL", "admin@test.de");
       
        Environment.SetEnvironmentVariable("ADMIN_RW", "Admin!123Admin?");
        
        Factory = factory;
        Client = factory.CreateClient();

        var scopeFactory = factory.Services.GetService<IServiceScopeFactory>() ?? throw new Exception("Scope factory not found");
        var scope = scopeFactory.CreateScope() ?? throw new Exception("Could not create Scope");

        DbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>() ?? throw new Exception("cpold not get Application DbContext");

        Mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();
    }   
}