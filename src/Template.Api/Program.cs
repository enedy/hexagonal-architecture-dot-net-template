using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Template.Api.Extensions;
using Template.Api.Logging;
using Template.Api.Middleware;
using Template.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.WebApiConfig();
builder.Services.AddSwaggerConfig();

builder.Services.RegisterServices();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

app.UseHttpsRedirection();

// CULTURE
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(SuportedCultures.DefaultCulture)
    .AddSupportedCultures(SuportedCultures.Cultures)
    .AddSupportedUICultures(SuportedCultures.Cultures);

app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfig(provider);

app.Run();
