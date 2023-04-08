using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _245Final.Pages.PlatformsPage
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public EditModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Platform Platform { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Platform == null)
            {
                return NotFound();
            }

            var platform =  await _context.Platform.FirstOrDefaultAsync(m => m.ID == id);
            if (platform == null)
            {
                return NotFound();
            }
            Platform = platform;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Platform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(Platform.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlatformExists(int id)
        {
          return (_context.Platform?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
