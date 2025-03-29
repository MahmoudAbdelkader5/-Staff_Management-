using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_Access_layer.model;
using System.Xml;
using Microsoft.EntityFrameworkCore;


namespace data_Access_layer.context
{

   public class mvcAppDbcontext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-8BGSPC5;Database=mvcAppDb;Trusted_Connection=True;Encrypt=False");
        //}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        public mvcAppDbcontext(DbContextOptions<mvcAppDbcontext> options) : base(options)
        {
        }

    
}
}
