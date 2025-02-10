using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using task_slayer.Data.Repositories.Interfaces;
using task_slayer.Services.Interfaces;
using task_slayer.ViewModels;

namespace task_slayer.Pages.Categoria
{
    public class Categorias : PageModel
    {
        private readonly ILogger<Categorias> _logger;
        private readonly ICategoriaService _categoriaService;
        public Categorias(ILogger<Categorias> logger,ICategoriaService categoriaService)
        {
            _logger = logger;
            _categoriaService = categoriaService;
        }

        public CategoriaViewModel[] ListCategorias { get; set; }

        public async Task OnGet()
        {
            ListCategorias = await _categoriaService.GetCategoriaPages(1, 20);
        }
    }
}