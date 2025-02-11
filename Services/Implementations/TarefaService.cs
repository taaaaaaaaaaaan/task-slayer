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

        public async Task<TarefaViewModel[]> GetTarefasByUser(Usuario usuario)
        {
            var tarefas = await _tarefaRepository.GetTarefasByUser(usuario);
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
    }
}