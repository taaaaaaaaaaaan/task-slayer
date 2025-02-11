using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.ViewModels;

namespace task_slayer.Services.Interfaces
{
    public interface ICategoriaService 
    {
        Task<CategoriaViewModel[]> GetCategoriaPages(int pageNumber,string userId, int pageSize = 20);
        Task<CategoriaViewModel> CreateCategoria(CreateCategoriaViewModel createCategoriaViewModel,string userId);
        Task<CategoriaViewModel> UpdateCategoria(CategoriaViewModel updateCategoriaViewModel,string userId);
        Task<bool> DeleteCategoria(int idCategoria,string userId);
        Task<CategoriaViewModel> GetCategoriaByUserAndId(int idCategoria,string userId);
        Task<int> CountCategoriasByUserId(string userId);
    }
}