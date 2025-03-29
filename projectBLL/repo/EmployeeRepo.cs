using data_Access_layer.context;
using data_Access_layer.model;
using Microsoft.EntityFrameworkCore;
using projectBLL.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBLL.repo
{
    public class EmployeeRepo : genericRepo<Employee>, IEmployeeRepo
    {
        private readonly mvcAppDbcontext _context;

        public EmployeeRepo(mvcAppDbcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.Department).ToListAsync();
        }

        public IQueryable<Employee> SearchEmployees(string search)
        {
           return _context.Employees.Include(d=>d.Department).Where(_context => _context.Name.ToLower().StartsWith(search));
        }
    }
}