using System;

namespace SicpaCrud.Models
{
    public class Enterprises
    {
        public int enterprisesId { get; set; }
        public string? enterprisesCreated_by { get; set; }
        public DateTime? enterpricesCreated_date { get; set; }
        public string? enterpricesModified_by { get; set; }
        public DateTime? enterpricesModified_date { get; set; }
        public Boolean? enterprisesStatus { get; set; }
        public string? enterprisesAddress { get; set; }
        public string? enterprisesName { get; set; }
        public string? enterprisesPhone { get; set; }


    }
}
