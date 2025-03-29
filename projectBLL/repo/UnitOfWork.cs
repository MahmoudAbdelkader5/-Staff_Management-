using data_Access_layer.context;
using Microsoft.EntityFrameworkCore;
using projectBLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace projectBLL.repo
{
    public class UnitOfWork :IunitOfWork,IDisposable
    {
        private readonly mvcAppDbcontext dbcontext;


        public IDepartmentRepo departmentRepo { get; set; }
        public IEmployeeRepo employeeRepo { get; set; }

        public UnitOfWork(mvcAppDbcontext dbcontext)
        {
            departmentRepo = new DepartmentRepo(dbcontext); // Use the property to assign  
            employeeRepo = new EmployeeRepo(dbcontext);     // Use the property to assign  
            this.dbcontext = dbcontext;
        }

        // You should implement a Save method that commits changes to the database   
        public int Save()
        {
            return dbcontext.SaveChanges(); // Assuming dbcontext has SaveChanges method  
        }

        public void Dispose()
        {
            dbcontext.Dispose();
        }
    }
}
