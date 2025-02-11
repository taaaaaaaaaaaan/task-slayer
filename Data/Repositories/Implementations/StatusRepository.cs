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
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        private readonly TaskSlayerContext _context;
        public StatusRepository(TaskSlayerContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<Status[]> GetAllStatus()
        {
            return await _context.Status.Where(e => !e.IsDeleted).ToArrayAsync();
        }
    }
}