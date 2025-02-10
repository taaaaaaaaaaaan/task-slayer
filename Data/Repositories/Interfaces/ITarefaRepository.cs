using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;

namespace task_slayer.Data.Repositories.Interfaces
{
    public interface ITarefaRepository : IGenericRepository<Tarefa>
    {
       Task<Categoria[]> GetCategoriaPages(int pageNumber, int pageSize = 20);
    }
}