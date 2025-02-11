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
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public async Task OnGet(int pageNumber = 1)
        {
            const int pageSize = 10; //  Define quantas categorias aparecem por página
            ListCategorias = await _categoriaService.GetCategoriaPages(pageNumber, pageSize);
            var usuario = await _userManager.GetUserAsync(User);
            CurrentPage = pageNumber;
            //  Define o numero de páginas, se o número possuir parte decimal, arredonda para cima
            TotalPages = (int)Math.Ceiling(await _categoriaService.CountCategoriasByUserId(usuario)/ (double)pageSize);
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            await _categoriaService.DeleteCategoria(id,usuario);
            return RedirectToPage();
        }
    }
}