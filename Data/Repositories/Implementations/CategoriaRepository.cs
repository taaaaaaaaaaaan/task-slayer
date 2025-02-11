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

        public async Task<Categoria> GetCategoriaByIdAndUserId(int id, string userId)
        {

            return await _context.Categorias
                .Where(e => !e.IsDeleted && e.UsuarioId == userId && e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Categoria[]> GetCategoriaPages(int pageNumber,string userId, int pageSize = 20)
        {
    
            return await _context.Categorias
                .Where(e => !e.IsDeleted && e.UsuarioId == userId)
                .Skip(pageSize * (pageNumber-1))
                .Take(pageSize).ToArrayAsync();

        }
    }
}