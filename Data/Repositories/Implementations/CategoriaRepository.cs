using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.Data.Contexts;
using task_slayer.Data.Repositories.Implementations;
using task_slayer.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace task_slayer.Data.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>,ICategoriaRepository
    {
        private readonly TaskSlayerContext _context;
        public CategoriaRepository(TaskSlayerContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<int> CountCategoriasByUserId(string userId)
        {
           
            return await _context.Categorias
                .Where(e => !e.IsDeleted && e.UsuarioId == userId)
                .CountAsync();
        }

        public async Task<Categoria> GetCategoriaByIdAndUserId(int id, string userId,bool includeTarefas = false)
        {
            var query =_context.Categorias.Where(e => !e.IsDeleted && e.UsuarioId == userId && e.Id == id );
            if(includeTarefas)
               query.Include(e => e.Tarefas);

             return await query.FirstOrDefaultAsync();
        }

        // Caso o tamanho da página seja 0, retorna todos os registros de categorias.
        public async Task<Categoria[]> GetCategoriaPages(int pageNumber,string userId, int pageSize = 20)
        {
            var query = _context.Categorias
                .Where(e => !e.IsDeleted && e.UsuarioId == userId);
            // Retorna todos os registros de categorias.
            if( pageSize == 0)
                return await query.ToArrayAsync();
            // Retorna as páginas
            return await query
                .Skip(pageSize * (pageNumber-1))
                .Take(pageSize).ToArrayAsync();

        }
    }
}