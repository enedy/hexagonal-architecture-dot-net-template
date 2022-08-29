using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiConfigExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddControllers();

            // FORMATA O RETORNO JSON
            services.AddControllersWithViews().
                    AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddCors();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("Development",
            //        builder =>
            //            builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader());


            //    options.AddPolicy("Production",
            //        builder =>
            //            builder
            //                .WithMethods("GET")
            //                .WithOrigins("http://mywebsite.com.br")
            //                .SetIsOriginAllowedToAllowWildcardSubdomains()
            //                .AllowAnyHeader());
            //});

            return services;
        }
    }
}
