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
        public async Task<CategoriaViewModel[]> GetCategoriaPages(int pageNumber,Usuario usuario, int pageSize = 20)
        {
            var categorias = await _categoriaRepository.GetCategoriaPages(pageNumber,usuario.Id, pageSize);
            return [.. categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome
            })];
        }
        public async Task<CategoriaViewModel> CreateCategoria(CreateCategoriaViewModel createCategoriaViewModel,Usuario usuario)
        {
            var categoria = new Categoria
            {
                Nome = createCategoriaViewModel.Nome,
                Usuario = usuario
            };
            await _categoriaRepository.AddAsync(categoria);
            
            return new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

        }

        public async Task<CategoriaViewModel> GetCategoriaByUserAndId(int idCategoria, Usuario usuario)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAndUserId(idCategoria, usuario.Id);
            return new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public async Task<CategoriaViewModel> UpdateCategoria(CategoriaViewModel updateCategoriaViewModel, Usuario usuario)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAndUserId(updateCategoriaViewModel.Id, usuario.Id);
            categoria.Nome = updateCategoriaViewModel.Nome;
            await _categoriaRepository.UpdateAsync(categoria);
            return new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public async Task<bool> DeleteCategoria(int idCategoria, Usuario usuario)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAndUserId(idCategoria, usuario.Id);
            categoria.IsDeleted = true;
            await _categoriaRepository.UpdateAsync(categoria);
            return true;
        }

        public async Task<int> CountCategoriasByUserId(Usuario usuario)
        {
            return await _categoriaRepository.CountCategoriasByUserId(usuario.Id);
        }

 
    }
}