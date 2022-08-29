using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Template.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerConfigExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = $"Insira o token JWT desta maneira: Bearer seu-token",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            //app.UseMiddleware<SwaggerAuthorizedMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }

                    options.RoutePrefix = string.Empty;
                });
            return app;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "API",
                Version = description.ApiVersion.ToString(),
                Description = "Template Api.",
                Contact = new OpenApiContact() { Name = "TEMPLATE", Email = "template@template.com.br" }
            };

            if (description.IsDeprecated)
            {
                info.Description += " OBSOLETE VERSION!";
            }

            return info;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                {
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public SwaggerAuthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger")
                && !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}
