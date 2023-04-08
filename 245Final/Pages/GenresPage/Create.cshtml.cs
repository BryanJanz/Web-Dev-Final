using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _245Final.Data;
using _245Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace _245Final.GenresPage
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public CreateModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Genres Genres { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Genres.Add(Genres);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
