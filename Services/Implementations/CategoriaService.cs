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
        public async Task<CategoriaViewModel[]> GetCategoriaPages(int pageNumber,string userId, int pageSize = 20)
        {
            var categorias = await _categoriaRepository.GetCategoriaPages(pageNumber,userId, pageSize);
            return [.. categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome
            })];
        }
        public async Task<CategoriaViewModel> CreateCategoria(CreateCategoriaViewModel createCategoriaViewModel,string userId)
        {
            var categoria = new Categoria
            {
                Nome = createCategoriaViewModel.Nome,
                UsuarioId = userId
            };
            await _categoriaRepository.AddAsync(categoria);
            
            return new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

        }

        public async Task<CategoriaViewModel> GetCategoriaByUserAndId(int idCategoria,string userId)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAndUserId(idCategoria, userId);
            if(categoria== null)
            {
                return null;
            }
            return new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public async Task<CategoriaViewModel> UpdateCategoria(CategoriaViewModel updateCategoriaViewModel,string userId)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAndUserId(updateCategoriaViewModel.Id, userId);
            categoria.Nome = updateCategoriaViewModel.Nome;
            await _categoriaRepository.UpdateAsync(categoria);
            return new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public async Task<bool> DeleteCategoria(int idCategoria,string userId)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAndUserId(idCategoria, userId,true);
            if(categoria == null)
            {
                return false;
            }
            categoria.IsDeleted = true;
            if(categoria.Tarefas != null){
                foreach(var tarefa in categoria.Tarefas)
                {
                    tarefa.IsDeleted = true;
                }
            }
            

            await _categoriaRepository.UpdateAsync(categoria);
            return true;
        }

        public async Task<int> CountCategoriasByUserId(string userId)
        {
            return await _categoriaRepository.CountCategoriasByUserId(userId);
        }

 
    }
}