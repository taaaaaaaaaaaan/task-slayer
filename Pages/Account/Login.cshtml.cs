using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using task_slayer.Data.Entities;
using task_slayer.Services.Interfaces;
namespace task_slayer.Pages.Account
{
    public class Login : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IAccountService _accountService;
        public Login(SignInManager<Usuario> signInManager,UserManager<Usuario> userManager,IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
            _userManager = userManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [RegularExpression("([a-zA-Z0-9_])+",ErrorMessage = "O nome de usuário só pode conter letras, números e _")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var token = await _accountService.GenerateJWToken(user);
                Response.Headers.Add("Authorization", $"Bearer {token}");

                return LocalRedirect(returnUrl ?? "/Index");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Conta bloqueada devido a várias tentativas de login. Tente novamente mais tarde.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Senha incorreta. Tente novamente.");
            }

            return Page();
        }



    }
}