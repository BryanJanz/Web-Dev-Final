using _245Final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace _245Final.Pages.CartPage
{
    [Authorize(Roles = "User")]

    public class CheckoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartViewModel Cart { get; set; }
        public List<SelectListItem> Provinces { get; set; }

        public CheckoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {

            Cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");

            Provinces = new List<SelectListItem>
            {
                new SelectListItem { Value = "AB", Text = "Alberta" },
                new SelectListItem { Value = "BC", Text = "British Columbia" },
                new SelectListItem { Value = "MB", Text = "Manitoba" },
                new SelectListItem { Value = "NB", Text = "New Brunswick" },
                new SelectListItem { Value = "NL", Text = "Newfoundland and Labrador" },
                new SelectListItem { Value = "NS", Text = "Nova Scotia" },
                new SelectListItem { Value = "NT", Text = "Northwest Territories" },
                new SelectListItem { Value = "NU", Text = "Nunavut" },
                new SelectListItem { Value = "ON", Text = "Ontario" },
                new SelectListItem { Value = "PE", Text = "Prince Edward Island" },
                new SelectListItem { Value = "QC", Text = "Quebec" },
                new SelectListItem { Value = "SK", Text = "Saskatchewan" },
                new SelectListItem { Value = "YT", Text = "Yukon" }
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _httpContextAccessor.HttpContext.Session.Remove("Cart");


                return RedirectToPage("ThankYou");
            }
            else
            {
                return Page();
            }
        }
    }
}
