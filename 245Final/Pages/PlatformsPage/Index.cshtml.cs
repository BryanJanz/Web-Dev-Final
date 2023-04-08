using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _245Final.Pages.PlatformsPage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public IndexModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Platform> Platform { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Platform != null)
            {
                Platform = await _context.Platform.ToListAsync();
            }
        }
    }
}
