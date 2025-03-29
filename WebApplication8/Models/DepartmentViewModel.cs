using data_Access_layer.model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebApplication8.Models
{
    public class DepartmentViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Code field is required.")]
        public string code { get; set; } = string.Empty;

        public DateTime dateCreation { get; set; } = DateTime.UtcNow;
        // [InverseProperty("Employee")]

        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();
    }
}
