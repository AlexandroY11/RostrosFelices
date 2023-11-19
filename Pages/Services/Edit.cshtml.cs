using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Services
{
    public class EditModel : PageModel
    {
		private readonly RostrosFelicesContext _context;
		public List<string> Employees = new List<string>();
		public List<string> Clients = new List<string>();

		public EditModel(RostrosFelicesContext context)
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
			var category = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			Service = category;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_context.Attach(Service).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ServiceExists(Service.Id))
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

		private bool ServiceExists(int id)
		{
			return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
