using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Models;

namespace RostrosFelices.Pages.Services
{
	public class CreateModel : PageModel
	{
		private readonly RostrosFelicesContext _context;
		public List<string>Employees = new List<string>();
		public List<string> Clients = new List<string>();

		public CreateModel(RostrosFelicesContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			//In
			Employees.Add("-");
			string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RostrosFelices;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Consultar las categorías desde la base de datos
				using (SqlCommand command = new SqlCommand("select concat(Id, '-',[Name]) from Employees", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{


						// Leer las categorías y agregarlas a la lista
						while (reader.Read())
						{
							string employee = reader.GetString(0);

							Employees.Add(employee);
						}

						// Pasar la lista de categorías a la vista
						//cat = categorias;
					}
				}
			}

			//************************************************

			Clients.Add("-");
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Consultar las categorías desde la base de datos
				using (SqlCommand command = new SqlCommand("select concat(Id, '-',[Name]) from Clients", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{


						// Leer las categorías y agregarlas a la lista
						while (reader.Read())
						{
							string employee = reader.GetString(0);

							Clients.Add(employee);
						}

						// Pasar la lista de categorías a la vista
						//cat = categorias;
					}
				}
			}

			//************************************************
			return Page();
		}

		[BindProperty]
		public Service Service { get; set; } = default!;
		private Service Final { get; set; } = default!;


		public async Task<IActionResult> OnPostAsync()
		{
			 if (!ModelState.IsValid || _context.Services == null || Service == null)
    {
        return Page();
    }

    Service finalService = new Service(); // Crear una nueva instancia de Service

    finalService.Id = Service.Id;
    finalService.Name = Service.Name;
    finalService.Fecha = Service.Fecha;

    if (!string.IsNullOrEmpty(Service.EmployeeName))
    {
        int employeeIdIndex = Service.EmployeeName.IndexOf('-');
        if (employeeIdIndex != -1 && Service.EmployeeName.Length > employeeIdIndex + 2)
        {
            finalService.EmployeeId = Convert.ToInt32(Service.EmployeeName.Substring(0, employeeIdIndex));
            finalService.EmployeeName = Service.EmployeeName.Substring(employeeIdIndex + 2);
        }
    }

    if (!string.IsNullOrEmpty(Service.ClientName))
    {
        int clientIdIndex = Service.ClientName.IndexOf('-');
        if (clientIdIndex != -1 && Service.ClientName.Length > clientIdIndex + 2)
        {
            finalService.ClientId = Convert.ToInt32(Service.ClientName.Substring(0, clientIdIndex));
            finalService.ClientName = Service.ClientName.Substring(clientIdIndex + 2);
        }
    }

    finalService.Observation = Service.Observation;

    _context.Services.Add(finalService);
    await _context.SaveChangesAsync();

    return RedirectToPage("./index");
		}
	}
}