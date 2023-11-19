using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Clients
{
	[Authorize]

	public class IndexModel : PageModel
    {
        private readonly RostrosFelicesContext _context;

        public IndexModel(RostrosFelicesContext context)
        {
            _context = context;
        }

        public IList<Client> Clients { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if(_context.Clients != null)
            {
                Clients = await _context.Clients.ToListAsync();
            }
        }
    }
}
