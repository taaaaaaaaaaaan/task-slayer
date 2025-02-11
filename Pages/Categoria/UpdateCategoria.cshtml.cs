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

namespace task_slayer.Pages.Categoria
{
    public class UpdateCategoria : PageModel
    {
        private readonly ICategoriaService _categoriaService;
        private readonly UserManager<Usuario> _userManager;
        public UpdateCategoria(ICategoriaService categoriaService, UserManager<Usuario> userManager)
        {
            _categoriaService = categoriaService;
            _userManager = userManager;
        }

        [BindProperty]
        public CategoriaViewModel Categoria { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Usuario? usuario = await _userManager.GetUserAsync(User); 
            var categoria = await _categoriaService.GetCategoriaByUserAndId(id,usuario.Id);
            if(categoria == null){
                return RedirectToPage("./Categorias");
            }
            Categoria = new CategoriaViewModel
            {
                Nome = categoria.Nome,
                Id = categoria.Id
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Usuario? usuario = await _userManager.GetUserAsync(User); // Obtém o usuário logado 
            if (usuario == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" }); // Redireciona para a página de login
            }

            await _categoriaService.UpdateCategoria(Categoria,usuario.Id); // Cria a categoria

            return RedirectToPage("./Categorias"); // Redireciona para a lista de categorias
        }
    }
}