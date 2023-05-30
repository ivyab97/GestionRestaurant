using Aplication.Interface.IComanda;
using Aplication.Interface.IComandaMercaderia;
using Aplication.Interface.IFormaEntrega;
using Aplication.Interface.IMercaderia;
using Aplication.Interface.ITipoMercaderia;
using Aplication.UseCase.Services.SComanda;
using Aplication.UseCase.Services.SComandaMercaderia;
using Aplication.UseCase.Services.SFormaEntrega;
using Aplication.UseCase.Services.SMercaderia;
using Aplication.UseCase.Services.STipoMercaderia;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.0",
        Title = "Restaurante",
        Description = "Gestiona las comandas y la mercaderias.",
        Contact = new OpenApiContact
        {
            Name = "IvanBrestt",
            Url = new Uri("https://github.com/ivyab97")
        }
    });

    //documentar swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
//custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
            app.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IMercaderiaCommand, MercaderiaCommand>();
builder.Services.AddScoped<IMercaderiaQuery, MercaderiaQuery>();
builder.Services.AddScoped<IMercaderiaCommandServices, MercaderiaCommandServices>();
builder.Services.AddScoped<IMercaderiaQueryServices, MercaderiaQueryServices>();

builder.Services.AddScoped<IComandaCommand, ComandaCommand>();
builder.Services.AddScoped<IComandaQuery, ComandaQuery>();
builder.Services.AddScoped<IComandaCommandServices, ComandaCommandServices>();
builder.Services.AddScoped<IComandaQueryServices, ComandaQueryServices>();

builder.Services.AddScoped<IComandaMercaderiaCommand, ComandaMercaderiaCommand>();
builder.Services.AddScoped<IComandaMercaderiaCommandServices, ComandaMercaderiaCommandServices>();

builder.Services.AddScoped<ITipoMercaderiaQuery, TipoMercaderiaQuery>();
builder.Services.AddScoped<ITipoMercaderiaQueryServices, TipoMercaderiaQueryServices>();

builder.Services.AddScoped<IFormaEntregaQuery, FormaEntregaQuery>();
builder.Services.AddScoped<IFormaEntregaQueryServices, FormaEntregaQueryServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NewPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
