using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_Access_layer.model
{


    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
       
        public int Age { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime HireTime { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Department")]

        public int ?DepartmentId { get; set; }
       // [InverseProperty("Department")]
        public Department Department { get; set; }
        public string Image { get; set; }

    }
}
