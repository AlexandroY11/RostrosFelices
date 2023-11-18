using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Services
{
    public class IndexModel : PageModel
    {
		private readonly RostrosFelicesContext _context;
		public IndexModel(RostrosFelicesContext context)
		{
			_context = context;
		}
		public IList<Service> Services { get; set; } = default!;
		public async Task OnGetAsync()
		{
			if (_context.Services != null)
			{
				Services = await _context.Services.ToListAsync();
			}
		}
	}
}
