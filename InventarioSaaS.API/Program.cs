using InventarioSaaS.API.Exepciones;
using InventarioSaaS.Application.service;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using InventarioSaaS.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Area de servicios

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddOpenApi();

//configuracion del db context
builder.Services.AddDbContext<AppDbcontext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnections")));
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbcontext>()
    .AddDefaultTokenProviders();

//inyeccion de dependecnias
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepositorio>();
builder.Services.AddScoped<IProductoService, ProductoService>();

//configuracion del JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    });

builder.Services.AddAuthorization(option => option.AddPolicy("admin", politica => politica.RequireClaim("rol", "admin")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// area de Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MiddlewareEx>();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
