using Microsoft.EntityFrameworkCore;
using Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSqlServer")));

builder.Services.AddControllers();
builder.Services.AddOpenApi(); 

// Configuração do CORS para permitir que o Angular (que roda na porta 4200) acesse a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Ativa o ecossistema de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Gera o arquivo de rotas em: /openapi/v1.json
}

app.UseHttpsRedirection();

// Ativa a regra do CORS antes dos Controllers/ Não sabia o que era CORS*
app.UseCors("PermitirAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();