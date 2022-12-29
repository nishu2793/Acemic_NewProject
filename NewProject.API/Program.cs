using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using NewProject.API.Infrastructure.Extensions;
using NewProject.API.Infrastructure.Filters;
using NewProject.API.Infrastructure.Middlewares;
using NewProject.API.Infrastructure.Swagger;
using NewProject.Utility;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NewProject.Services.Interfaces;
using NewProject.Services.Services;
using NewProject.Services.Entities.User;
using NewProject.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.Configure<FormOptions>(opt =>
{
    opt.MultipartBodyLengthLimit = long.MaxValue;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "NewProject API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
    option.OperationFilter<SwaggerHeader>();
});

builder.Services.RegisterServices();

builder.Services.RegisterRepositories();

builder.Services.ConfigureDatabases(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddSignalR();


builder.Services.ConfigureCors();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permission", policyBuilder =>
    {
        policyBuilder.Requirements.Add(new CommercialAuthorizationRequirement());
    });

});
builder.Services.AddScoped<IAuthorizationHandler, CommercialAuthorizationHandler>();
var facebookAuthSettings = new FacebookAuthSettings();
builder.Configuration.Bind(key: nameof(FacebookAuthSettings), facebookAuthSettings);
builder.Services.AddSingleton(facebookAuthSettings);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IFacebookService, FacebookService>();

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
// .AddCookie(options =>
// {
//     options.LoginPath = "/facebook/facebook-login";

// })
//  .AddFacebook(options =>
//   {
//       options.AppId = "492327696205227";
//       options.AppSecret = "a4bd3a577462531abb0f1df5a059e8f3";
//   });

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})

//   .AddFacebook(options =>
//   {
//       options.AppId = "492327696205227";
//       options.AppSecret = "a4bd3a577462531abb0f1df5a059e8f3";
//   }).AddCookie();
//var configuration = builder.Configuration;

//builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
//{
//    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
//    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
//});



// Add services to the container.



//var configuration = builder.Configuration;

//builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
//{
//    facebookOptions.AppId = configuration["492327696205227"];
//    facebookOptions.AppSecret = configuration["a4bd3a577462531abb0f1df5a059e8f3"];
//});

//builder.Services.AddSingleton<IFacebookAuthService, FacebookAuthService>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

//.SetIsOriginAllowed(origin => true) // allow any origin
//.AllowCredentials()); ; // allow credentials

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors("NewProjectCors");

//app.UseForwardedHeaders(new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//});

app.UseRouting();

app.UseAuthorization();

//app.MapHub<ChatHub>("/chatHub");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatHub");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<JwtMiddleware>();




//app.MapControllers();

app.Run();

