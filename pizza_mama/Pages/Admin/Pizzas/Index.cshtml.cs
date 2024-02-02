using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using pizza_mama.Models;

namespace pizza_mama.Pages.Admin.Pizzas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        //elle cree une variable de type dataContext
        private readonly pizza_mama.Data.DataContext _context;

        //recupere les donnés dans le dataContext
        public IndexModel(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Pizza> Pizza { get;set; }

        //elle permet de recuperer la liste des pizzas
        public async Task OnGetAsync()
        {
            Pizza = await _context.Pizzas.ToListAsync();
        }
    }
}
