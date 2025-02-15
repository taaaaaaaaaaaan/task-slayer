using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;

namespace task_slayer.Data.Repositories.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task<Categoria[]> GetCategoriaPages(int pageNumber,string userId, int pageSize = 20);
        Task<Categoria> GetCategoriaByIdAndUserId(int id, string userId,bool includeTarefas = false);
        Task<int> CountCategoriasByUserId( string userId);

    }
}