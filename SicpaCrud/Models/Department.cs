using System;

namespace SicpaCrud.Models
{
    public class Department
    {
        public int departmentsId { get; set; }
        public string? departmentsName { get; set; }
        public string? departmentsPhone { get; set; }
        public DateTime? departmentsCreated_date { get; set; }
        public string? departmentsModified_by { get; set; }
        public string? departmentsCreated_by { get; set; }
        public DateTime? departmentsModified_date { get; set; }
        public Boolean? departmentsStatus { get; set; }
        public string? departmentsDescription { get; set; }
        public int id_enterprises { get; set; } 


    }
}
