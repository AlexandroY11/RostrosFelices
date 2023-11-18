using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Services
{
    public class CreateModel : PageModel
    {
		private readonly RostrosFelicesContext _context;

		public CreateModel(RostrosFelicesContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public Service Service { get; set; } = default!;


		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Services == null || Service == null)
			{
				return Page();
			}

			_context.Services.Add(Service);
			await _context.SaveChangesAsync();

			return RedirectToPage("./index");
		}
	}
}