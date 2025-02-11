using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.ViewModels;

namespace task_slayer.Data.Repositories.Interfaces
{
    public interface ITarefaRepository : IGenericRepository<Tarefa>
    {
        Task<Tarefa> GetTarefaByIdAndUser(int id, string usuarioId);
        Task<Tarefa[]> GetTarefasByUser(string userId);
    }
}