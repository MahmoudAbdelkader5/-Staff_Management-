using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBLL.interfaces
{
   public interface IunitOfWork
    {
        public IDepartmentRepo departmentRepo { get; set; }
        public IEmployeeRepo employeeRepo { get; set; }
        int Save();
    }
}
