using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Projecto.ApiCatalogo.Context;
using Projecto.ApiCatalogo.DTOs.Mapping;
using Projecto.ApiCatalogo.Extensions;
using Projecto.ApiCatalogo.Filter;
using Projecto.ApiCatalogo.Logging;
using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories._ProductoRepository;
using Projecto.ApiCatalogo.Repositories.GenericRepository;
using Projecto.ApiCatalogo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(optns =>
    {
        optns.Filters.Add(typeof(ApiExcepionFilter));
    })
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));

builder.Services.AddTransient<IMeuServico, MeuServico>(); //AddTransient indica que vai criar um novo obj sempre que for silicitada uma instancia desse serviço

builder.Services.AddScoped<ApiLoggerFilter>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // AddScoped seguifica que vai criar uma instancia unica (pra ser usado em todos os repositorios) 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Logging.AddProvider(new CustumerLoggerProvider(new CustumerLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddAutoMapper(typeof(ProductoDTOMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler(); // método de eztenção
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();