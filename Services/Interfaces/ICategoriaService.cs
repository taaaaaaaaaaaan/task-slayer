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
        Task<CategoriaViewModel[]> GetCategoriaPages(int pageNumber, int pageSize = 20);
    }
}