using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWAlks_2.API.Data;
using NZWAlks_2.API.Repositories;
using NZWAlks_2.API.Repository;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options=>
        {
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name="JWT Authentication",
                Description="Enter a valid JWT bearer token",
                In=ParameterLocation.Header,
                Type=SecuritySchemeType.Http,
                Scheme="bearer",
                BearerFormat="JWT",
                Reference = new OpenApiReference()
                {
                    Id=JwtBearerDefaults.AuthenticationScheme,
                    Type=ReferenceType.SecurityScheme,
                }
            };

            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme,new string[] { } }
            });

        });

        //Add Fluent Validation Services
        builder.Services.
            AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

        builder.Services.AddDbContext<NZWalksDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks_2"));
        });

        builder.Services.AddScoped<IRegionRepository, RegionRepository>();
        builder.Services.AddScoped<IWalkRepository, WalkRepository>();
        builder.Services.AddScoped<IWalkDifficultiesRepository, WalkDifficultiesRepository>();
        builder.Services.AddScoped<ITokenHandler, NZWAlks_2.API.Repositories.TokenHandler>();

        //builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();


        //used version 11 of Automapper
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,    
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();    
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}