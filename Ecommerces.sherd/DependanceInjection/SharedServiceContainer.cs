using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerces.sherd.DependanceInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>
            (this IServiceCollection services,IConfiguration config, string FileName) where TContext : DbContext
           
        {
            // Register shared services here
            // services.AddScoped<YourSharedService>();
            return services;
        }
    }
}
