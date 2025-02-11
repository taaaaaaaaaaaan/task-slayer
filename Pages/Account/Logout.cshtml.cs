using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using task_slayer.Data.Entities;

namespace task_slayer.Pages.Account
{
    public class Logout: PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;

        public Logout(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync(); // Realiza o logout
            return RedirectToPage("/Account/Login"); // Redireciona para a página de login
        }
    }
}