using BookStoreManagement.ClientApp.Services.CookieService;
using BookStoreManagement.ClientApp.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookStoreManagement.ClientApp.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
            });

            services.AddSingleton<ICookieService, CookieService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
