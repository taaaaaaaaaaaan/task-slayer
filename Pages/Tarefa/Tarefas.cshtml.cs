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

        public async Task OnGet()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                RedirectToPage("/Account/Login"); // Redireciona para login se não autenticado
            }

            ListTarefas = await _tarefaService.GetTarefasByUser(usuario);
        }
    }
}