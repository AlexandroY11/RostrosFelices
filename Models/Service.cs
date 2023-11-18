namespace RostrosFelices.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } //Tipo de servicio
        public DateTime Fecha { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }  
        public int ClientId { get; set; }  
        public string ClientName { get; set; }  
        public string Observation { get; set; }  

          
    }
}
