using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using task_slayer.Data.Entities;
using task_slayer.Services.Interfaces;
using task_slayer.ViewModels;

namespace task_slayer.Pages.Tarefa
{
    public class Tarefas : PageModel
    {       
        private readonly ITarefaService _tarefaService;
        private readonly UserManager<Usuario> _userManager;

        public Tarefas(ITarefaService tarefaService, UserManager<Usuario> userManager)
        {
            _tarefaService = tarefaService;
            _userManager = userManager;
        }

        public TarefaViewModel[] ListTarefas { get; set; }
        public string UsuarioNome { get; set; }
        public TarefaViewModel TarefaSelecionada { get; set; }
        public bool MostrarModal { get; set; } = false;
        public async Task OnGet()
        {
            var usuario = await _userManager.GetUserAsync(User);

            UsuarioNome = usuario?.UserName.ToUpper() ?? "AVENTUREIRO"; 


            ListTarefas = await _tarefaService.GetTarefasByUser(usuario.Id);
        }
        public async Task<IActionResult> OnPostAbrirModal(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            TarefaSelecionada = await _tarefaService.GetTarefaByIdAndUser(id, usuario.Id);

            if (TarefaSelecionada == null)
            {
                return RedirectToPage(); // Redireciona caso a tarefa não exista
            }

            MostrarModal = true;
            ListTarefas = await _tarefaService.GetTarefasByUser(usuario.Id);

            return Page();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            await _tarefaService.DeleteTarefa(id,usuario.Id);
            return RedirectToPage();
        }
    }
}