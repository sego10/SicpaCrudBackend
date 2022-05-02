using System;

namespace SicpaCrud.Models
{
    public class DepartamentoEmpleado
    {
        public int DepEmpId { get; set; }
        public string? DepEmpCreated_by { get; set; }
        public DateTime? DepEmpCreated_date { get; set; }
        public string? DepEmpModified_by { get; set; }
        public Boolean? DepEmpStatus { get; set; }
        public int? id_department { get; set; }
        public int? id_employee { get; set; }
        public DateTime? DepEmpModified_date { get; set; }

    }
}
