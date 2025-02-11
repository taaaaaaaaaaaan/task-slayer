using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.Data.Contexts;
using task_slayer.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace task_slayer.Data.Repositories.Implementations
{
    public class TarefaRepository : GenericRepository<Tarefa>, ITarefaRepository
    {
        private readonly TaskSlayerContext _context;
        public TarefaRepository(TaskSlayerContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<Tarefa> GetTarefaByIdAndUser(int id, string usuarioId)
        {
            return await _context.Tarefas
                    .Where(e => e.UsuarioId == usuarioId && e.Id == id && !e.IsDeleted)
                    .Include(t => t.Categoria)
                    .Include(t => t.Status)
                    .FirstOrDefaultAsync();
        }

        public async Task<Tarefa[]> GetTarefasByUser(string userId)
        {
            return await _context.Tarefas
                                .Where(e => e.UsuarioId == userId && !e.IsDeleted)
                                .Include(t => t.Categoria)
                                .Include(t => t.Status)
                                .ToArrayAsync();
        }
    }
}