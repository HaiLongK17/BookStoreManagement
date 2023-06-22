using BookStoreManagement.Core;
using BookStoreManagement.Core.Mappers;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Data;
using BookStoreManagement.Service;
using BookStoreManagement.Service.Photo;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace BookStoreManagement.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Hà Hải Long",
                        Email = "hailongsn99@gmail.com",
                    }
                });
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt/*.UseLazyLoadingProxies()*/
                .UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(MapperProfile).Assembly);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            return services;
        }
    }
}
