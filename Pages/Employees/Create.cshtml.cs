using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Employees
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

		public Employee Employee { get; set; } = default!;

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Employees == null || Employee == null)
			{
				return Page();
			}
			_context.Employees.Add(Employee);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");


		}
	}
}
