using AutoMapper;
using Biblioteca.API;
using Biblioteca.Infrastructure;
using Biblioteca.Infrastructure.Migrations;
using Biblioteca.Service;
using Biblioteca.Service.Services.Autores;
using Biblioteca.Service.Services.Libros;
using Biblioteca.Service.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Biblioteca.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BibilotecaDbContext>(options => {
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILibroService, LibrosService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenService, TokenService>();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
        ClockSkew = TimeSpan.Zero
    });




builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("EsAdmin", policy => policy.RequireClaim("esAdmin"));
    options.AddPolicy(ApplicationConstants.AdminClaim, policy => policy.RequireClaim(ApplicationConstants.AdminClaim));
    options.AddPolicy(ApplicationConstants.EmpleadoClaim, policy => policy.RequireClaim(ApplicationConstants.EmpleadoClaim));
    options.AddPolicy(ApplicationConstants.ClienteClaim, policy => policy.RequireClaim(ApplicationConstants.ClienteClaim));
    options.AddPolicy(ApplicationConstants.AdminOrClienteClaim, policy =>
    {
        policy.RequireAssertion(ContextBoundObject => ContextBoundObject.User.HasClaim(c => c.Type == ApplicationConstants.AdminClaim ||
        c.Type == ApplicationConstants.ClienteClaim));

    });
    options.AddPolicy(ApplicationConstants.AdminOrEmpleadoClaim, policy =>
    {
        policy.RequireAssertion(ContextBoundObject => ContextBoundObject.User.HasClaim(c => c.Type == ApplicationConstants.AdminClaim ||
        c.Type == ApplicationConstants.EmpleadoClaim));
    });
    options.AddPolicy(ApplicationConstants.EmpleadoOrClienteClaim, policy =>
    {
        policy.RequireAssertion(ContextBoundObject => ContextBoundObject.User.HasClaim(c => c.Type == ApplicationConstants.EmpleadoClaim||
        c.Type == ApplicationConstants.ClienteClaim));
    });
    options.AddPolicy(ApplicationConstants.AllClaims, policy =>
    {
        policy.RequireAssertion(ContextBoundObject => ContextBoundObject.User.HasClaim(c => c.Type == ApplicationConstants.AdminClaim ||
        c.Type == ApplicationConstants.ClienteClaim || c.Type == ApplicationConstants.EmpleadoClaim));
    });
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BibilotecaDbContext>()
.AddDefaultTokenProviders();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BibilotecaDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await SeedData.Initialize(context, userManager);
}
catch (Exception e)
{
    logger.LogError(e, "Error Seeding data");

}
    app.Run();
