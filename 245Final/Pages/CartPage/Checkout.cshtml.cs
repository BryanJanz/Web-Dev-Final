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
        public List<SelectListItem> Months { get; set; }

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

            Months = new()
            {
                new SelectListItem {Value = "01", Text = "January"},
                new SelectListItem {Value = "02", Text = "February"},
                new SelectListItem {Value = "03", Text = "March"},
                new SelectListItem {Value = "04", Text = "April"},
                new SelectListItem {Value = "05", Text = "May"},
                new SelectListItem {Value = "06", Text = "June"},
                new SelectListItem {Value = "07", Text = "July"},
                new SelectListItem {Value = "08", Text = "August"},
                new SelectListItem {Value = "09", Text = "September"},
                new SelectListItem {Value = "10", Text = "October"},
                new SelectListItem {Value = "11", Text = "November"},
                new SelectListItem {Value = "12", Text = "December"}
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
