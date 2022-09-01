using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApplication
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
            services.AddTransient<IUserRL, UserRL>();
            services.AddTransient<IUserBL, UserBL>();

            services.AddTransient<IBookBL, BookBL>();
            services.AddTransient<IBookRL, BookRL>();

            services.AddTransient<ICartBL, CartBL>();
            services.AddTransient<ICartRL, CartRL>();

            services.AddTransient<IWishlistBL, WishlistBL>();
            services.AddTransient<IWishlistRL, WishlistRL>();

            services.AddTransient<IAddressBL, AddressBL>();
            services.AddTransient<IAddressRL, AddressRL>();

            services.AddTransient<IOrderBL, OrderBL>();
            services.AddTransient<IOrderRL, OrderRL>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStoreApplication" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",

                    Name = "Authorization",

                    In = ParameterLocation.Header,

                    Type = SecuritySchemeType.Http,

                    Scheme = "bearer",

                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement

                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });

            //var jwtSection = Configuration.GetSection("Jwt:Key");
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,

                    ValidateAudience = false,

                    ValidateLifetime = false,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))

                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Swagger Configuration in API  
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreApplication");

                });
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
