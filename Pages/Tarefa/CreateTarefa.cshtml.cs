using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using task_slayer.Data.Entities;
using task_slayer.Services.Interfaces;
using task_slayer.ViewModels;

namespace task_slayer.Pages.Tarefa
{
    public class CreateTarefa : PageModel
    {

        private readonly ITarefaService _tarefaService;
        private readonly IStatusService _statusService;
        private readonly ICategoriaService _categoriaService;
        private readonly UserManager<Usuario> _userManager;

        public CreateTarefa(ITarefaService tarefaService, IStatusService statusService, ICategoriaService categoriaService, UserManager<Usuario> userManager)
        {
            _tarefaService = tarefaService;
            _statusService = statusService;
            _categoriaService = categoriaService;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> CategoriaList { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(255)]
            public string Titulo { get; set; }

            [StringLength(1000)]
            public string Descricao { get; set; }

            [Required]
            public int StatusId { get; set; }

            [Required]
            public int CategoriaId { get; set; }

            [Required]
            public DateTime DataConclusao { get; set; }

            [Range(1, 20, ErrorMessage = "A dificuldade deve estar entre 1 e 20.")]
            public short Dificuldade { get; set; } = 10; // Valor padrão
        }

        public async Task OnGet()
        {
            var usuario = await _userManager.GetUserAsync(User);

            await LoadSelectLists(usuario);
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuario = await _userManager.GetUserAsync(User);
            await LoadSelectLists(usuario);
            if (Input.StatusId == 0)
            {
                
                ModelState.AddModelError("Input.StatusId", "O status não pode ser vazio");
                return Page(); 
            }
            if (Input.CategoriaId == 0)
            {
                
                ModelState.AddModelError("Input.CategoriaId", "A categoria não pode ser vazia, caso não tenha categorias cadastradas, cadastre uma.");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            DateTime dataConclusaoUtc = DateTime.SpecifyKind(Input.DataConclusao, DateTimeKind.Utc);


            if (Input.StatusId != 3 && dataConclusaoUtc < DateTime.UtcNow.Date)
            {
                
                ModelState.AddModelError("Input.DataConclusao", "A data de conclusão não pode ser menor que a data vigente se a tarefa não estiver concluída.");
                return Page();
            }

            await _tarefaService.CreateTarefa(new CreateTarefaViewModel
            {
                Titulo = Input.Titulo,
                Descricao = Input.Descricao,
                StatusId = Input.StatusId,
                CategoriaId = Input.CategoriaId,
                DataConclusao = dataConclusaoUtc,
                Dificuldade = Input.Dificuldade,
                UsuarioId = usuario.Id
            });

            return RedirectToPage("/Tarefa/Tarefas");
        }
        private async Task LoadSelectLists(Usuario usuario)
        {
            StatusList = [.. (await _statusService.GetAllStatus()).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Nome
            })];

            CategoriaList = [.. (await _categoriaService.GetCategoriaPages(1, usuario.Id, 0)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nome
            })];
        }
    }
}