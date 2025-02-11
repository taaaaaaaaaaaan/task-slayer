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

        public async Task<Categoria> GetCategoriaByIdAndUserId(int id, string userId)
        {
            var main_query = from categoria in _context.Categorias
                             where categoria.Id == id && categoria.UsuarioId == userId
                             select categoria;
           
            return await main_query
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<Categoria[]> GetCategoriaPages(int pageNumber, int pageSize = 20)
        {
            var main_query = from categoria in _context.Categorias
                             select categoria;
           
            return await main_query
                .Where(e => !e.IsDeleted)
                .Skip(pageSize * (pageNumber-1))
                .Take(pageSize).ToArrayAsync();

        }
    }
}