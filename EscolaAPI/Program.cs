using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Usar o método de extensão para configurar a infraestrutura
builder.Services.AddInfrastructureServices(
    builder.Configuration.GetConnectionString("DefaultConnection"));

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