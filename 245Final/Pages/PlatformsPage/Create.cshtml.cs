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
using System.Data;

namespace _245Final.Pages.PlatformsPage
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
        public Platform Platform { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Platform == null || Platform == null)
            {
                return Page();
            }

            _context.Platform.Add(Platform);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
