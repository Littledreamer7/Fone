using FoneApi.Data;
using FoneApi.Service.Interface;
using FoneApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using FoneApi.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Models;
using IdentityServer4;
using NuGet.Configuration;
using Swashbuckle.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Sql Connection
builder.Services.AddDbContext<FoneDb>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FoneConn")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

//builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("UsersList"));
builder.Services.AddControllers();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{   // for development only
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"]
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FONE",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme." +
        " \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below" +
        "\r\n\r\nExample: \"Bearer jhfdkj.jkdsakjdsa.jkdsajk\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


//var IssueUrl = builder.Configuration["IssueUrl"].TrimEnd('/');
//var applicationUrl = builder.Configuration["ApplicationUrl"].TrimEnd('/');

//var identity = builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.IssuerUri = IssueUrl;
//}).AddDeveloperSigningCredential();

//identity.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
//{
//    options.Clients.Add(new Client()
//    {
//        ClientId = "FONEAPI",
//        AccessTokenType = AccessTokenType.Jwt,
//        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
//        AllowAccessTokensViaBrowser = true,
//        RequireClientSecret = false,
//        AllowedScopes = {
//                                IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
//                                IdentityServerConstants.StandardScopes.Profile,
//                                "FONE"
//                             },
//        AllowOfflineAccess = true, // For refresh token.
//        RefreshTokenExpiration = TokenExpiration.Sliding,
//        RefreshTokenUsage = TokenUsage.OneTimeOnly,
//    });
//    options.Clients.Add(new Client
//    {
//        ClientId = "swaggerui",
//        ClientName = "Swagger UI",
//        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
//        AllowAccessTokensViaBrowser = true,
//        RequireClientSecret = false,
//        AllowedScopes = {
//                        "FONE"
//                    }
//    });
//}).AddProfileService<>();

//builder.Services.AddAuthentication()
//     .AddJwtBearer(jwt =>
//     {
//         jwt.Authority = applicationUrl;
//         jwt.RequireHttpsMetadata = false;
//         jwt.Audience = "FONE.APIAPI";
//     })
//     .AddIdentityServerJwt();

////////////////////////////////////

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FONE", Version = "v1" });
//    c.OperationFilter<AuthorizeCheckOperationFilter>();
//    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Type = SecuritySchemeType.OAuth2,
//        Flows = new OpenApiOAuthFlows
//        {
//            Password = new OpenApiOAuthFlow
//            {
//                TokenUrl = new Uri("/connect/token", UriKind.Relative)
//            }
//        }
//    });
//});

///////////////////////////////


#region Automapper configuration
builder.Services.AddAutoMapper(typeof(Program));
#endregion


#region Bussiness logic service
builder.Services.AddScoped<IFoneService, FoneService>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FONE V1");
        c.OAuthClientId("SwaggerApi");
        c.OAuthAppName("Swagger Api Calls");


    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
