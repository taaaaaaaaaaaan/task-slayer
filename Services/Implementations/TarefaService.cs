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
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<TarefaViewModel> CreateTarefa(CreateTarefaViewModel createTarefaViewModel)
        {
            Tarefa tarefa = new Tarefa{
                Titulo = createTarefaViewModel.Titulo,
                Descricao = createTarefaViewModel.Descricao,
                DataConclusao = createTarefaViewModel.DataConclusao,
                CategoriaId = createTarefaViewModel.CategoriaId,
                StatusId = createTarefaViewModel.StatusId,
                UsuarioId = createTarefaViewModel.UsuarioId
            };
            await _tarefaRepository.AddAsync(tarefa);
            var tarefaEntity = await _tarefaRepository.GetTarefaByIdAndUser(tarefa.Id, createTarefaViewModel.UsuarioId);
            return new TarefaViewModel
            {
                Id = tarefaEntity.Id,
                Titulo = tarefaEntity.Titulo,
                Descricao = tarefaEntity.Descricao,
                DataConclusao = tarefaEntity.DataConclusao,
                Categoria = new CategoriaViewModel
                {
                    Id = tarefaEntity.Categoria.Id,
                    Nome = tarefaEntity.Categoria.Nome
                },
                Status = new StatusViewModel
                {
                    Id = tarefaEntity.Status.Id,
                    Nome = tarefaEntity.Status.Nome
                }
            };
        }

        public async Task<bool> DeleteTarefa(int idTarefa, string userId)
        {
            var tarefa = await _tarefaRepository.GetTarefaByIdAndUser(idTarefa, userId);
            if(tarefa == null)
            {
                return false;
            }
            tarefa.IsDeleted = true;
            await _tarefaRepository.UpdateAsync(tarefa);
            return true;

        }

        public async Task<TarefaViewModel> GetTarefaByIdAndUser(int id, string usuarioId)
        {
            var tarefa = await _tarefaRepository.GetTarefaByIdAndUser(id, usuarioId);
            if(tarefa == null)
            {
                return null;
            }
            return new TarefaViewModel
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataConclusao = tarefa.DataConclusao,
                Categoria = new CategoriaViewModel
                {
                    Id = tarefa.Categoria.Id,
                    Nome = tarefa.Categoria.Nome
                },
                Status = new StatusViewModel
                {
                    Id = tarefa.Status.Id,
                    Nome = tarefa.Status.Nome
                }
            };
        }

        public async Task<TarefaViewModel[]> GetTarefasByUser(string userId)
        {
            var tarefas = await _tarefaRepository.GetTarefasByUser(userId);
            return [.. tarefas.Select(e => new TarefaViewModel
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descricao = e.Descricao,
                DataConclusao = e.DataConclusao,
                Categoria = new CategoriaViewModel
                {
                    Id = e.Categoria.Id,
                    Nome = e.Categoria.Nome
                },
                Status = new StatusViewModel
                {
                    Id = e.Status.Id,
                    Nome = e.Status.Nome
                }
            })];
        }

        public async Task<TarefaViewModel> UpdateTarefa(UpdateTarefaViewModel createTarefaViewModel, string userId,int idTarefa)
        {
            var tarefa = await _tarefaRepository.GetTarefaByIdAndUser(idTarefa, userId);
            tarefa.Titulo = createTarefaViewModel.Titulo;
            tarefa.Descricao = createTarefaViewModel.Descricao;
            tarefa.DataConclusao = createTarefaViewModel.DataConclusao;
            tarefa.CategoriaId = createTarefaViewModel.CategoriaId;
            tarefa.StatusId = createTarefaViewModel.StatusId;
            await _tarefaRepository.UpdateAsync(tarefa);
            return new TarefaViewModel
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataConclusao = tarefa.DataConclusao,
                Categoria = new CategoriaViewModel
                {
                    Id = tarefa.Categoria.Id,
                    Nome = tarefa.Categoria.Nome
                },
                Status = new StatusViewModel
                {
                    Id = tarefa.Status.Id,
                    Nome = tarefa.Status.Nome
                }
            };
        }
    }
}