using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool DisplayInvalidAccountMessage = false;
        //on instancie le IConfiguration de la classe startup
        IConfiguration configuration;
        public IndexModel(IConfiguration configuration) 
        {   
            this.configuration = configuration;
        }
        public IActionResult OnGet()
        {
            Console.WriteLine(HttpContext.User);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }

        public async Task <IActionResult> OnPostAsync(string username, string password, string ReturnUrl)
        { 
            var authSection = configuration.GetSection("Auth");

            //verifie la connection mis dans le appJson
            string adminLogin = authSection["AdminLogin"];
            string adminPassword = authSection["AdminPassword"];
            DisplayInvalidAccountMessage = false;
            if (username == adminLogin && password == adminPassword)
            {
                var claims = new List<Claim>
                {
                 new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
               ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
            }
            DisplayInvalidAccountMessage = true;
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
        
    }
}
