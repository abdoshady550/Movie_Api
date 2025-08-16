using System.Text.Json.Serialization;
using Asp.net_Web_Api.Meddlewares;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie_Api.Data;
using Movie_Api.Middleware;
using Movie_Api.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true;
                });
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<AppDbContext>
(option => option.UseSqlServer((builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<IGenraService, GenraService>();
builder.Services.AddScoped<IMovieService, MovieService>();



// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // يطلع v1, v2 ...
    options.SubstituteApiVersionInUrl = false; // عشان مش بنستخدم URL versioning
});

// Swagger + Versioning
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // نخلي Swagger يضيف Parameter للـ MediaType Versioning
    options.OperationFilter<SwaggerDefaultValues>();
    // نضيف Header اسمه Accept فيه الـ version
    options.OperationFilter<AddAcceptHeaderOperationFilter>();
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Movies API {description.GroupName.ToUpperInvariant()}");
        }
    });
}


app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseMiddleware<RateLimiterMiddleware>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
