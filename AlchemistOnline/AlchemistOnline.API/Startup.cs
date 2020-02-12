using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemistOnline.API.Services;
using AlchemistOnline.API.Services.Accounts;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.API.Services.Cryptography;
using AlchemistOnline.API.Services.Cryptography.Hash;
using AlchemistOnline.API.Services.Cryptography.Token;
using AlchemistOnline.API.Services.Explorers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RandomNameGeneratorLibrary;
using Swashbuckle.AspNetCore.Swagger;

namespace AlchemistOnline.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<AlchemistContext>(options => options.UseSqlServer(Configuration.GetConnectionString(AlchemistContext.CONNECTION_STRING_NAME)));

            CryptographySettings cryptographySettings = new CryptographySettings();
            Configuration.GetSection(CryptographySettings.CRYPTO_SECTION).Bind(cryptographySettings);
            services.AddSingleton(cryptographySettings);

            services.AddSingleton<IHashFactory, Sha256HashFactory>();
            services.AddSingleton<ITokenFactory, JwtTokenFactory>();

            services.AddSingleton<Random, Random>();
            services.AddTransient<PersonNameGenerator, PersonNameGenerator>();

            //Domain Services
            services.AddTransient<IExplorerService, ExplorerService>();
            services.AddTransient<IAccountService, AccountService>();

            //Context Services
            services.AddTransient<IDataGenerator, EnvironmentDifficulties>();
            services.AddTransient<IDataGenerator, EnvironmentTypes>();
            services.AddTransient<IDataGenerator, EnvironmentLocations>();
            services.AddTransient<IDataGenerator, IngredientTypes>();
            services.AddTransient<IDataGenerator, Ingredients>();
            services.AddTransient<IDataGenerator, ExplorerTypes>();

            //JWT Auth
            byte[] key = Encoding.ASCII.GetBytes(cryptographySettings.TokenSecret);
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Require auth on all controllers by default
            services.AddMvc(config =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                                                .RequireAuthenticatedUser()
                                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Alchemist", Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Alchemist v1");
                config.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
