using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace task_slayer.Pages.Tarefa
{
    public class UpdateTarefas : PageModel
    {
        private readonly ILogger<UpdateTarefas> _logger;

        public UpdateTarefas(ILogger<UpdateTarefas> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}