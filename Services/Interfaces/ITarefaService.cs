using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.ViewModels;

namespace task_slayer.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaViewModel[]> GetTarefasByUser(Usuario usuario);
        Task<TarefaViewModel> CreateTarefa(CreateTarefaViewModel createTarefaViewModel);    
    }
}