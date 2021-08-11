using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using net_design_pattern.Domain.Repositories.Admin;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Domain.Repositories.User;
using net_design_pattern.Domain.Services.Admin;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Domain.Services.Common;
using net_design_pattern.Domain.Services.User;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Persistence.Helper;
using net_design_pattern.Persistence.Repositories.Admin;
using net_design_pattern.Persistence.Repositories.Authorization;
using net_design_pattern.Persistence.Repositories.Common;
using net_design_pattern.Persistence.Repositories.User;
using net_design_pattern.Services.Admin;
using net_design_pattern.Services.Authorization;
using net_design_pattern.Services.Common;
using net_design_pattern.Services.User;
using Swashbuckle.AspNetCore;

namespace net_design_pattern
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
            //IoC, Service Collection: 3 type Scope, Singleton and Transient
            //Register Service
            services.AddControllers();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddLogging();
            services.AddMvc();
            services.AddOptions();

            //Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "net_design_pattern",
                                                     Version = "v1",
                                                     Description = "A simple example ASP.NET Core Web API", 
                                                     Contact = new OpenApiContact { Name = "Jessie", Email = "trinh@gmail.coms", Url = new Uri("https://www.facebook.com/profile.php?id=100011643972090"),},
                                                     });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //Add authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
                {  
                    Name = "Authorization",  
                    Type = SecuritySchemeType.ApiKey,  
                    Scheme = "Bearer",  
                    BearerFormat = "JWT",  
                    In = ParameterLocation.Header,  
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
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

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AppCnn")));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IProfileService, ProfileService>();

            var key = Configuration.GetSection("JwtKey").Value;
            //configure a JWT based authentication service
            services.AddAuthentication(x =>
            {
                //setting the default authentication and challenge scheme 
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                //The AddJwtBearer will handle all requests and will check for a valid JWT Token in the header.
                // If it is not passed, or the token is expired, it will generate a 401 Unauthorized HTTP response.
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //setting the IssuerSigningKey
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            //Add singleton, 
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
            //
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IRegisterRepository, RegisterRepository>();
            services.AddTransient<IRegisterService, RegisterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "net_design_pattern v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //make the authentication service is available to the application.
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
