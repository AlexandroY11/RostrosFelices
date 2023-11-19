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

			// Cargar datos necesarios para combo box de empleados
			Employees.Add("-");
			Employees.AddRange(await _context.Employees.Select(e => $"{e.Id}-{e.Name}").ToListAsync());

			// Cargar datos necesarios para combo box de clientes
			Clients.Add("-");
			Clients.AddRange(await _context.Clients.Select(c => $"{c.Id}-{c.Name}").ToListAsync());

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

			// Verificar la existencia del empleado antes de asignar el ID
			if (!string.IsNullOrEmpty(Service.EmployeeName))
			{
				int employeeIdIndex = Service.EmployeeName.IndexOf('-');
				if (employeeIdIndex != -1 && Service.EmployeeName.Length > employeeIdIndex + 2)
				{
					int employeeId = Convert.ToInt32(Service.EmployeeName.Substring(0, employeeIdIndex));

					// Verificar si el empleado existe en la base de datos
					if (!_context.Employees.Any(e => e.Id == employeeId))
					{
						// El empleado no existe, manejar el error (por ejemplo, mostrar un mensaje al usuario)
						ModelState.AddModelError("Service.EmployeeName", "Empleado no válido");
						return Page();
					}

					Service.EmployeeId = employeeId;
					Service.EmployeeName = Service.EmployeeName.Substring(employeeIdIndex + 1); // Ajuste del índice
				}
			}

			// Verificar la existencia del cliente antes de asignar el ID
			if (!string.IsNullOrEmpty(Service.ClientName))
			{
				int clientIdIndex = Service.ClientName.IndexOf('-');
				if (clientIdIndex != -1 && Service.ClientName.Length > clientIdIndex + 2)
				{
					int clientId = Convert.ToInt32(Service.ClientName.Substring(0, clientIdIndex));

					// Verificar si el cliente existe en la base de datos
					if (!_context.Clients.Any(c => c.Id == clientId))
					{
						// El cliente no existe, manejar el error (por ejemplo, mostrar un mensaje al usuario)
						ModelState.AddModelError("Service.ClientName", "Cliente no válido");
						return Page();
					}

					Service.ClientId = clientId;
					Service.ClientName = Service.ClientName.Substring(clientIdIndex + 1); // Ajuste del índice
				}
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
