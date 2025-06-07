using System.Text;
using HitsInternship.Api.Extensions.Middlewares;
using HitsInternship.Api.Extensions.Swagger;
using System.Text.Json.Serialization;
using AuthModel.Service.Service;
using HitsInternship.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Extensions.Validation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        config => config.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.AddSwaggerConfig();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(AuthSettings.PrivateKey))
        };
    });
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .ConfigureApiBehaviorOptions(options =>
        options.InvalidModelStateResponseFactory = FailedAnnotationValidationResponse.MakeValidationResponse);

builder.Services.AddApplicationModules(builder.Configuration, builder.Environment);

Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));

var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerConfiguration();

app.UseCors("AllowAllOrigins");

app.Services.UseApplicationModules();
app.UseAuthentication();
app.UseAuthorization();

app.AddMiddleware();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();