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
        Task<TarefaViewModel[]> GetTarefasByUser(string userId);
        Task<TarefaViewModel> CreateTarefa(CreateTarefaViewModel createTarefaViewModel);  
        Task<TarefaViewModel> UpdateTarefa(UpdateTarefaViewModel createTarefaViewModel, string userId,int idTarefa);  
        Task<TarefaViewModel> GetTarefaByIdAndUser(int id, string usuarioId);  
        Task<bool> DeleteTarefa(int idTarefa, string userId);
    }
}