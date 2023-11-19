using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Services
{
    public class DeleteModel : PageModel
    {
		private readonly RostrosFelicesContext _context;

		public DeleteModel(RostrosFelicesContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Service Service { get; set; } = default!;
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Services == null)
			{
				return NotFound();
			}
			var employee = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);

			if (employee == null)
			{
				return NotFound();
			}
			else
			{
				Service = employee;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.Services == null)
			{
				return NotFound();
			}
			var employee = await _context.Services.FindAsync(id);

			if (employee != null)
			{
				Service = employee;
				_context.Services.Remove(Service);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage("./Index");
		}
	}
}