using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RostrosFelices.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } //Tipo de servicio
        public DateTime Fecha { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public string Observation { get; set; }

        //public ICollection<Employee>? Employees { get; set; } = default;
        //public ICollection<Client>? Client { get; set; } = default;
		//public Employee Employee { get; set; }
  //      public Client Client { get; set; }
    }
}
