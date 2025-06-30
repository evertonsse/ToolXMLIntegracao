using Microsoft.EntityFrameworkCore;
using ToolXMLIntegracao;
using ToolXMLIntegracao.Controllers;


public class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


        var app = builder.Build();

        // Forçar Swagger em todos os ambientes
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
            c.RoutePrefix = "swagger"; 
        });

        app.MapControllers();

        // Rota de teste na raiz
        app.MapGet("/", () => "API está funcionando! Acesse /swagger para ver a documentação.");

        Console.WriteLine("Aplicação rodando em: http://localhost:5041");
        Console.WriteLine("Swagger disponível em: http://localhost:5041/swagger");

        app.Run();
    }
}