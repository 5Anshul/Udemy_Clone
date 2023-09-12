using Edtech_backend_API.Data;
using Edtech_backend_API.FileServices;
using Edtech_backend_API.FileServices.FileServiceContract;
using Edtech_backend_API.Identity;
using Edtech_backend_API.Repository;
using Edtech_backend_API.Repository.IRepository;
using Edtech_backend_API.Services;
using Edtech_backend_API.Services.ServiceContract;
using Edtech_backend_API.StandaryDictionary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edtech_backend_API
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
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options
             .UseSqlServer(Configuration.GetConnectionString("conStr"), b => b.MigrationsAssembly("Edtech_backend_API")));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddDefaultTokenProviders();

            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Edtech_backend_API", Version = "v1" });
            });
            // JWT Authentication
            var appsettingSection = Configuration.GetSection("Appsettings"); // this appsettings is present in appsettings.cs
            services.Configure<AppSettings>(appsettingSection); // here appsettings is a class
            var appSetting = appsettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddCors(Options =>
            {
                Options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                           .AllowAnyOrigin()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Edtech_backend_API v1"));
            }
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            //// Data
            //IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.
            //    GetRequiredService<IServiceScopeFactory>();
            //using (IServiceScope scope = serviceScopeFactory.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService
            //        <RoleManager<ApplicationRole>>();
            //    var userManager = scope.ServiceProvider.GetRequiredService
            //        <UserManager<ApplicationUser>>();
            //    //Create Admin Role
            //    if (!await roleManager.RoleExistsAsync("Admin"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Admin";
            //        await roleManager.CreateAsync(role);
            //    }

            //    ////Create Instructor Role
            //    //if (!await roleManager.RoleExistsAsync("Instructor"))
            //    //{
            //    //    var role = new ApplicationRole();
            //    //    role.Name = "Instructor";
            //    //    await roleManager.CreateAsync(role);
            //    //}

            //    //Create User Role
            //    if (!await roleManager.RoleExistsAsync("User"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "User";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Admin User

            //    if (await userManager.FindByNameAsync("admin") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "admin@gmail.com";
            //        user.Email = "admin@gmail.com";
            //        var userPassword = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Admin");
            //        }
            //    }

            //    ////Create Instructor User
            //    //if (await userManager.FindByNameAsync("instructor") == null)
            //    //{
            //    //    var user = new ApplicationUser();
            //    //    user.UserName = "instructor";
            //    //    user.Email = "instructor@gmail.com";
            //    //    var userPassword = "Admin@123";
            //    //    var chkuser = await userManager.CreateAsync(user, userPassword);
            //    //    if (chkuser.Succeeded)
            //    //    {
            //    //        await userManager.AddToRoleAsync(user, "Instructor");
            //    //    }
            //    //}

            //    //Create Individual User

            //    if (await userManager.FindByNameAsync("user") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "user@gmail.com";
            //        user.Email = "user@gmail.com";
            //        var userPassword = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "user");
            //        }
            //    }
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
