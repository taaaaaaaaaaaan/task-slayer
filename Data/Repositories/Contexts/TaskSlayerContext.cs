using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using task_slayer.Data.Entities;

namespace task_slayer.Data.Repositories.Contexts
{
    public class TaskSlayerContext: IdentityDbContext<Usuario>
    {
        public TaskSlayerContext(DbContextOptions<TaskSlayerContext> options) 
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de relacionamento Categoria -> Tarefa
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Tarefas)
                .WithOne(t => t.Categoria)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Status>()
                .HasMany(c => c.Tarefas)
                .WithOne(t => t.Status)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Usuario>()
                .HasMany(c => c.Tarefas)
                .WithOne(t => t.Usuario)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

        }
        
    }
}