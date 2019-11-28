using System;
using System.IO;
using System.Text;
using Aceout.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Middleware;
using Aceout.Web.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Globalization;
using Autofac;
using Alexinea.Autofac.Extensions.DependencyInjection;
using Aceout.Infrastructure.Modules;
using Serilog;
using Microsoft.Extensions.Logging;
using Hangfire;
using Aceout.Web;
using Microsoft.AspNetCore.Authorization;
using Aceout.Application;
using Aceout.Web.Security.Permissions;
using FluentEmail.Mailgun;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using AutoMapper;
using Aceout.Infrastructure;
using Aceout.WebUI.Areas.Administration.Mappings;
using Aceout.Domain.Model.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using Aceout.WebUI.Areas.Lms.Mappings;
using MediatR;
using System.Reflection;
using Aceout.WebUI.Areas.Organization.Mappings;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.FileProviders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.Memory;
using Swashbuckle.AspNetCore.Swagger;
using Aceout.WebUI.Areas.Cms.Mappings;

namespace Aceout.WebUI
{
    public class Startup
    {
        public static string WebRootPath { get; private set; }

        public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile($"aceout.{env.EnvironmentName.ToLower()}.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            Aceout.Migrations.Program.UpdateDb($"{System.IO.Directory.GetCurrentDirectory()}", env.EnvironmentName.ToLower());
		}

        public static string MapPath(string path, string basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                basePath = WebRootPath;
            }

            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(basePath, path);
        }

        public IHostingEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.SuppressModelStateInvalidFilter = true;
                
            });

            var config = Configuration.GetSection("AppSettings").Get<AppConfiguration>();

            var sender = new MailgunSender(config.Email.Domain, config.Email.ApiKey);
            Email.DefaultSender = sender;

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AdministrationProfile());
                mc.AddProfile(new LmsProfile());
                mc.AddProfile(new CmsProfile());
                mc.AddProfile(new OrganizationProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config.Authentication.Issuer,
                        ValidAudience = config.Authentication.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Authentication.Key))
                    };
                });

            services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddTransient<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<MemoryAllocator, ArrayPoolMemoryAllocator>();

            services.AddResponseCaching();

            services.AddCors(options =>
            {
                options.AddPolicy("Default", b =>
                {
                    b.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("Default"));
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(Aceout.Web.Filters.ModelValidationFilterAttribute));
                options.Filters.Add(typeof(Aceout.Web.Filters.GlobalExceptionFilter));
            });

            services.AddApiVersioning(x =>
                {
                    x.ApiVersionReader = new UrlSegmentApiVersionReader();
                    x.AssumeDefaultVersionWhenUnspecified = true;
                    x.DefaultApiVersion = new ApiVersion(1, 0);
                }
            );
                

            //services.AddMvcCore()
            //    .AddApiExplorer();


            services.AddSingleton(config);
			services.AddDistributedRedisCache(x =>
			{
				x.InstanceName = "master";
                x.Configuration = config.Cache.Redis.Address;
			});

            services.AddMemoryCache();
            //services.AddHangfire(x =>
            //{
            //    x.UseRedisStorage(config.Cache.Redis.Address);
            //    x.UseColouredConsoleLogProvider();
            //});



            services.AddImageSharpCore(
                    options =>
                    {
                        options.Configuration = SixLabors.ImageSharp.Configuration.Default;
                        options.MaxBrowserCacheDays = 7;
                        options.MaxCacheDays = 365;
                        options.CachedNameLength = 8;
                        options.OnParseCommands = _ =>
                        {
                        };
                        options.OnBeforeSave = _ =>
                        {
                        };
                        options.OnProcessed = _ =>
                        {
                        };
                        options.OnPrepareResponse = _ => {  };
                    })
                .SetRequestParser<QueryCollectionRequestParser>()
                .SetMemoryAllocator<ArrayPoolMemoryAllocator>()
                .SetCache(pr =>
                {
                    var p = new PhysicalFileSystemCache(
                        pr.GetRequiredService<IHostingEnvironment>(),
                       new ArrayPoolMemoryAllocator(),
                        pr.GetRequiredService<IOptions<ImageSharpMiddlewareOptions>>());

                    p.Settings[PhysicalFileSystemCache.Folder] = config.ImageCacheDirectory;

                    return p;
                })
                .SetCacheHash<CacheHash>()
                .AddProvider<PhysicalFileSystemProvider>()
                .AddProcessor<ResizeWebProcessor>()
                .AddProcessor<FormatWebProcessor>()
                .AddProcessor<BackgroundColorWebProcessor>();

            services.AddLogging(loggerBuilder => loggerBuilder.AddSerilog(dispose: true));

            services.AddIdentity<User, Role>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "API V1",

                });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Aceout.WebUI.xml";
                c.IncludeXmlComments(xmlPath, true);
            });

            services.AddTransient<IDirectoryManager>(x => new DirectoryManager(config.Language.FilesPath));

            var provider = services.BuildServiceProvider();

            services.AddSingleton<IStringLocalizerFactory>(new FileStringLocalizerFactory(
                provider.GetService<IMemoryCache>(),
                provider.GetService<IDirectoryManager>(), 
                CultureInfo.GetCultureInfo(config.Language.Languages.First(x => x.IsDefault).Name),
                config.Language.Languages.Select(x => CultureInfo.GetCultureInfo(x.Name)).ToArray()));

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new RepositoryModule(Environment, config));
            builder.RegisterModule(new ValidatorsModule(Environment, config));
            builder.RegisterModule(new InfrastructureModule(Environment, config));
            builder.RegisterModule(new DomainServiceModule(Environment, config));
            builder.RegisterModule(new WebModule(Environment, config));
            builder.RegisterModule(new QueryModule(Environment, config));
            builder.RegisterModule(new ServiceModule(Environment, config));
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(QueryModule).GetTypeInfo().Assembly).AsImplementedInterfaces();

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {


            app.UseImageSharp();
            app.UseRequestLocalization();
            app.UseCors(x =>
            {
                x.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowCredentials()
                .WithMethods("GET", "POST", "DELETE", "PUT", "OPTIONS")
                ;
            });
            app.UseAuthentication();


            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            loggerFactory.AddSerilog();

            // app.UseDefaultFiles();
             app.UseStaticFiles();


            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "storage/Files")),           
            //});
            //app.UseHangfireServer(new BackgroundJobServerOptions
            //{
            //    Queues = new[]
            //    {
            //        "messages"
            //    },
            //    WorkerCount = System.Environment.ProcessorCount * 2
            //});

            // Lokalizacja
            var config = app.ApplicationServices.GetService<AppConfiguration>();
            app.UseRouteLocalizationMiddleware(GetRoutes);

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(config.Language.Languages.First(x => x.IsDefault).Name),
                SupportedCultures = config.Language.Languages.Select(x => CultureInfo.GetCultureInfo(x.Name)).ToList()
            };

            localizationOptions.RequestCultureProviders.Clear();
            localizationOptions.RequestCultureProviders.Add(
                new HeaderCultureProvider("Accept-Language",
                    new Microsoft.AspNetCore.Localization.ProviderCultureResult(config.Language.Languages.First(x => x.IsDefault).Name)));

            app.UseRequestLocalization(localizationOptions);

            // app.UseMiddleware(typeof(CorsMiddleware));

            app.UseMvc(routes =>
            {   
                GetRoutes(routes);
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("http://localhost/aceout/swagger/v1/swagger.json", "API V1");
            });

            WebRootPath = env.WebRootPath;
        }


        private readonly Action<IRouteBuilder> GetRoutes = routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");

            routes.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Home", action = "Index" });

        };
    }

    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(s => 
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
    // Added "Accept-Encoding" to this list
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Accept-Encoding, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
    // New Code Starts here
    if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    return context.Response.WriteAsync(string.Empty);
                }

                return Task.CompletedTask;
    // New Code Ends here
}, context);

            await _next(context);
        }
    }
}
