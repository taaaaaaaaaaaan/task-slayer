using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;
using task_slayer.Data.Repositories.Interfaces;
using task_slayer.Services.Interfaces;
using task_slayer.ViewModels;

namespace task_slayer.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository){
            _categoriaRepository = categoriaRepository;
        }
        public async Task<CategoriaViewModel[]> GetCategoriaPages(int pageNumber, int pageSize = 20)
        {
            var categorias = await _categoriaRepository.GetCategoriaPages(pageNumber, pageSize);
            return [.. categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome
            })];
        }
    }
}