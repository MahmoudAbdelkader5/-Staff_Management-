using data_Access_layer.model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBLL.interfaces
{
    public interface IEmployeeRepo : IgenericRepo<Employee>
    {
        public IQueryable<Employee> SearchEmployees(string search);

    }
}