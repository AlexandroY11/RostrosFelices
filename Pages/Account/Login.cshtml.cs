using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RostrosFelices._Repositories;
using RostrosFelices.Models;
using System.Security.Claims;

namespace RostrosFelices.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        private readonly UserRepository _repository;

        public LoginModel(UserRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            IEnumerable<User> userList = _repository.GetByValue(User.Email);

            if (userList.Any())
            {
                User userFromRepository = userList.First();

                if (userFromRepository.Password == User.Password)
                {
                    var emailParts = User.Email.Split('@');
                    string userName = emailParts.Length > 0 ? emailParts[0] : User.Email;

                    var claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Name, userName),
                         new Claim(ClaimTypes.Email, User.Email),
                    };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                    return RedirectToPage("/Index");
                }
            }

            ErrorMessage = "Error: Incorrect user or password.\"";
            TempData["ErrorMessage"] = ErrorMessage;
            return Page();

        }
    }
}
