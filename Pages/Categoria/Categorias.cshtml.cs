using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using task_slayer.Data.Entities;
using task_slayer.Data.Repositories.Interfaces;
using task_slayer.Services.Interfaces;
using task_slayer.ViewModels;

namespace task_slayer.Pages.Categoria
{
    public class Categorias : PageModel
    {
        private readonly ILogger<Categorias> _logger;
        private readonly ICategoriaService _categoriaService;
        private readonly UserManager<Usuario> _userManager;
        public Categorias(ILogger<Categorias> logger,ICategoriaService categoriaService,UserManager<Usuario> userManager)
        {
            _logger = logger;
            _categoriaService = categoriaService;
            _userManager = userManager;
        }

        public CategoriaViewModel[] ListCategorias { get; set; }

        public async Task OnGet()
        {
            ListCategorias = await _categoriaService.GetCategoriaPages(1, 20);
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            await _categoriaService.DeleteCategoria(id,usuario);
            return RedirectToPage();
        }
    }
}