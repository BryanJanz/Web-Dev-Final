using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _245Final.Data;
using _245Final.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _245Final.GamesPage
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public CreateModel(_245Final.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            Genres = _context.Genres.ToList();
            Platforms = _context.Platform.ToList();
            return Page();
        }
        
        [BindProperty]
        public Games Games { get; set; }
        [BindProperty]
        public List<Genres> Genres { get; set; }
        [BindProperty]
        public List<Platform> Platforms { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Games.Picture = Upload.FileName;

            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            _context.Games.Add(Games);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
