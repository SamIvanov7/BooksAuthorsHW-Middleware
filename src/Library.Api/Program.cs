using System.Text.Json.Serialization;
using Library.Application;
using Library.Infrastructure;
using Library.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Exceptions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.ModelValidatorProviders.Clear())
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomOperationIds(apiDescription =>
        apiDescription.TryGetMethodInfo(out var methodInfo)
            ? methodInfo.Name
            : null);
});

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddLibrariesPersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseCustomExceptionHandler(app.Environment);
app.UseSwagger();
app.UseSwaggerUI(x => x.DisplayOperationId());
app.UseHttpsRedirection();

// Register the JSON content type middleware
app.UseMiddleware<JsonContentTypeMiddleware>();

app.MapControllers();
app.Run();
