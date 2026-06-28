using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly AuthApiClient _auth;

        public LoginModel(AuthApiClient auth) => _auth = auth;

        [BindProperty]
        public LoginViewModel Input { get; set; } = new();

        public IActionResult OnGet(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Index");
            Input.ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var (success, data, error) = await _auth.LoginAsync(Input);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "Invalid credentials.");
                return Page();
            }

            // Store JWT in session
            HttpContext.Session.SetString("jwt_token", data!.Token);
            HttpContext.Session.SetString("user_id", data.UserId);
            HttpContext.Session.SetString("user_name", data.FullName);

            // Simple cookie-based auth claim (complement with proper auth middleware in production)
            TempData["SuccessMessage"] = $"Welcome back, {data.FullName}!";

            var returnUrl = Input.ReturnUrl;
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToPage("/Index");
        }
    }
}
