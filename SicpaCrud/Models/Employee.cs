using System;

namespace SicpaCrud.Models
{
    public class Employee
    {
        public int employeesId { get; set; }
        public string? employeeName { get; set; }
        public string? employeeSurname { get; set; }
        public DateTime? employeeCreated_date { get; set; }
        public string? employeedModified_by { get; set; }
        public string? employeesCreated_by { get; set; }
        public DateTime? employeeModified_date { get; set; }
        public Boolean? employeeStatus { get; set; }
        public string? employeeEmail { get; set; }
        public int? employeeAge { get; set; }
        public string? employeePosition { get; set; }

    }
}
