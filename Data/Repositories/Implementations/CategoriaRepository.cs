using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.Data.Contexts;
using task_slayer.Data.Repositories.Implementations;
using task_slayer.Data.Repositories.Interfaces;

namespace task_slayer.Data.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>,ICategoriaRepository
    {
        private readonly TaskSlayerContext _context;
        public CategoriaRepository(TaskSlayerContext context) : base(context)
        {
            _context = context; 
        }
    }
}