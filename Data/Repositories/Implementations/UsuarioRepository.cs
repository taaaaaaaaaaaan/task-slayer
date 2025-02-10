using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.Data.Repositories.Contexts;
using task_slayer.Data.Repositories.Interfaces;

namespace task_slayer.Data.Repositories.Implementations
{
    public class UsuarioRepository : GenericRepository<Usuario>,IUsuarioRepository
    {
        private readonly TaskSlayerContext _context;
        public UsuarioRepository(TaskSlayerContext context) : base(context)
        {
            _context = context; 
        }
    }
}