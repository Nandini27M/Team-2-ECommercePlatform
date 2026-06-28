using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly AuthApiClient _auth;

        public RegisterModel(AuthApiClient auth) => _auth = auth;

        [BindProperty]
        public RegisterViewModel Input { get; set; } = new();

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var (success, error) = await _auth.RegisterAsync(Input);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "Registration failed.");
                return Page();
            }

            TempData["SuccessMessage"] = "Account created! Please log in.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
