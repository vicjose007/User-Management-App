using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using UserManagementApp.API.Config;
using UserManagementApp.Application;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services
    .AddApplication()
    .AddRepositories();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
}).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateUser>())
.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreatePhone>())
.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateUserPhone>())
.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UpdateUser>())
.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UpdatePhone>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddAuthorization();
builder.Services.ConfigDbConnection(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
      .AllowAnyOrigin()
    //.WithOrigins("https://localhost:5173")
    .AllowAnyHeader()
        .AllowAnyMethod();
        //.AllowCredentials();
        //.SetIsOriginAllowed((host) => true);
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
