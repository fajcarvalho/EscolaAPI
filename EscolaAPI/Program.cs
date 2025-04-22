using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Infrastructure.Extensions;
using EscolaAPI.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar serviços da camada de aplicação
builder.Services.AddApplicationServices();

// Adicionar serviços da camada de infraestrutura
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructureServices(connectionString);

var app = builder.Build();

// Criar um endpoint de teste para verificar a conexão e criar o banco
app.MapGet("/test/database", async (ApplicationDbContext context) =>
{
    try
    {
        // Tentar criar o banco de dados
        await context.Database.EnsureCreatedAsync();
        
        // Obter lista de tabelas criadas
        var tables = await context.Database.ExecuteSqlRawAsync(
            "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'EscolaDB'");
        
        return Results.Ok(new 
        { 
            Status = "Sucesso", 
            Message = "Banco de dados criado com todas as tabelas"
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new 
        { 
            Status = "Erro", 
            Message = ex.Message,
            InnerException = ex.InnerException?.Message,
            StackTrace = ex.StackTrace
        });
    }
});

// Adicionar um endpoint para listar todas as tabelas
app.MapGet("/test/tables", async (ApplicationDbContext context) =>
{
    try
    {
        var tables = await context.Database.GetTablesAsync();
        return Results.Ok(new 
        { 
            Status = "Sucesso", 
            Tables = tables
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new 
        { 
            Status = "Erro", 
            Message = ex.Message
        });
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();