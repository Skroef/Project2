using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using projectTwo.Database;
using projectTwo.Services;
using projectTwo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using EntityConfigurationBase;
using projectTwo.Helpers;

namespace projectTwo
{
    public class Startup
    { 
        
        public Startup(IConfiguration configuration)
        {
            Configuration1 = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration1 { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectTwoContext>(options =>
               options.UseSqlServer(
                   Configuration1.GetConnectionString("DefaultConnection")
               )
           );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ninno Hattingh",
                    Description = "AgileDev API",
                    Contact = new OpenApiContact
                    {
                        Name = "Ninno Hattingh",
                        Email = "ninnohattingh1999@gmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Ninno Hattingh",
                    }
                });

            });
            services.AddControllers();

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.Configure<AppSettings>(Configuration1.GetSection("AppSettings"));
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IProjectTwoUnitOfWork, ProjectTwoUnitOfWork>();
            services.AddScoped<IDragAndDropService, DragAndDropService>();
            services.AddScoped<HashThisService, HashThisService>();
            services.AddScoped<ProjectTwoContext, ProjectTwoContext>();
            services.AddScoped<IFileStorageHelper, AzureFileStorageHelper>();
            services.AddScoped<IPostLogService, PostLogService>();
            services.AddScoped<IViewPostService, ViewPostService>();
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<IPostCommentService, PostCommentService>();
            services.AddScoped<IAlbumUserAccessService, AlbumUserAccessService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IAlbumPhotosService, AlbumPhotosService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            app.UseRouting();
            app.UseCors("ApiCorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
