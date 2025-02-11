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

        public async Task<Tarefa[]> GetTarefasByUser(Usuario usuario)
        {
            return await _context.Tarefas.Where(e => e.UsuarioId == usuario.Id && !e.IsDeleted).ToArrayAsync();
        }
    }
}