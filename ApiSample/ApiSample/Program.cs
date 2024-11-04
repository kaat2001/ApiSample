

using ApiSample.Constants;
using ApiSample.Extentions;
using ApiSample.Interfaces;
using Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using DataAccess.MsSql;
using DataAccess.PgSql;

Version? AppVersion = Assembly.GetEntryAssembly()?.GetName().Version;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

ConfigureSwaggerWithAuth(builder);

ConfigureAuth(builder);

ConfigureServices(builder);

ConfigureSeriLog(AppVersion, builder);

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();




static void ConfigureAuth(WebApplicationBuilder builder)
{
    var jwtSecretKey = " builder.Configuration.GetSection(\"TokenProviderOptions:JWTSecretKey\").Value;";

    var tokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
    };
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
        authenticationScheme: JwtBearerDefaults.AuthenticationScheme,
        configureOptions: options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
            options.SaveToken = true;
            options.IncludeErrorDetails = true;
        });
    Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

    builder.Services.AddAuthorization();
}

static void ConfigureSeriLog(Version? AppVersion, WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.WithProperty("Version", AppVersion)
                        .WriteTo.Console()
                        .WriteTo.Map("ControllerName", LogKeys.Common, (name, wt) =>
                        {
                            wt.File($"{context.Configuration["SerilogBasePath"]}/log_api_{name}_.txt",
                            rollingInterval: RollingInterval.Day,
                            encoding: Encoding.UTF8);
                        },
                                    sinkMapCountLimit: 0
                            )
                        ;
    });
}

static void ConfigureSwaggerWithAuth(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.CustomOperationIds(e =>
        {
            var controller = e.ActionDescriptor.RouteValues["controller"];
            var action = e.ActionDescriptor.RouteValues["action"] ?? e.HttpMethod ?? "get";
            return $"{controller}_{action.ToCamelCase()}";
        });
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSample", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        });
        c.SupportNonNullableReferenceTypes();
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
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    });
    //TODO - support PgSql
    //if (builder.Configuration.IsMsSql)
    builder.Services.AddDataAccessMsSqlModule(builder.Configuration);
    //else
    //builder.Services.AddDataAccessPgSqlModule(builder.Configuration);

    builder.Services.AddMemoryCache();
    builder.Services.AddScoped<IUserService, UserService>();
}

