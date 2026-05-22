using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //aqui tô definindo a tabela Tarefas
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}