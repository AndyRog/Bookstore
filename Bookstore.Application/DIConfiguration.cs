using Bookstore.Application.Dtos;
using Bookstore.Application.Services;
using Bookstore.Application.Unittests.Services;
using Bookstore.Application.Validation;
using Bookstore.Domain.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application;

public static class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<BookCreateService>();
        services.AddScoped<BookUpdateService>();
        services.AddScoped<BookDeliveryService>();
        services.AddScoped<BookSaleService>();
        services.AddScoped<BookFilterService>();
        services.AddScoped<AuthorCreateService>();
        services.AddScoped<AuthorUpdateService>();
        services.AddScoped<BookCreateValidator>();
        services.AddScoped<BookUpdateValidator>();
        services.AddScoped<BookDeliveryValidator>();
        services.AddScoped<BookSaleValidator>();
        services.AddScoped<BookValidator>();
        services.AddScoped<AuthorCreateValidator>();
        services.AddScoped<AuthorUpdateValidator>();
        
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
    }
}
