using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly RostrosFelicesContext _context;
        public IndexModel(RostrosFelicesContext context)
        {
            _context = context;
        }
        public IList<Employee> Employees { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Employees != null)
            {
                Employees = await _context.Employees.ToListAsync();
            }
        }
    }
}
