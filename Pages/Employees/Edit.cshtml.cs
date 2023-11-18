using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Employees
{
    public class EditModel : PageModel
    {
		private readonly RostrosFelicesContext _context;

		public EditModel(RostrosFelicesContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Employee Employee { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Employees == null)
			{
				return NotFound();
			}
			var category = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			Employee = category;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_context.Attach(Employee).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EmployeeExists(Employee.Id))
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

		private bool EmployeeExists(int id)
		{
			return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}