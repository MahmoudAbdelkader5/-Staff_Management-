using data_Access_layer.model;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication8.Models
{
    public class employeeViewModel  // Changed to PascalCase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        [Column(TypeName = "decimal(18,2)")]  // Added for proper decimal precision
        public decimal Salary { get; set; }

        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;  // Default value

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Hire date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }  // Renamed from HireTime for clarity
        public IFormFile Imagefile { get; set; }
        public string Image { get; set; }
        // Added for image support
        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Display(Name = "Department")]
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Department")]
        public virtual Department Department { get; set; }  // Added virtual for lazy loading
       public string CurrentImage { get; set; }
    }
}