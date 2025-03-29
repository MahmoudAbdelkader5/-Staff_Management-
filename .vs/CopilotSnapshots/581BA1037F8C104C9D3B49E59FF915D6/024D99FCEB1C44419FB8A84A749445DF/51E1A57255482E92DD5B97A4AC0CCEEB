using data_Access_layer.context;
using data_Access_layer.model;
using Microsoft.EntityFrameworkCore;
using projectBLL.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBLL.repo
{
    public class EmployeeRepo : IemployeeRepo
    {
        private readonly mvcAppDbcontext _context;

        public EmployeeRepo(mvcAppDbcontext context)
        {
            _context = context;
        }

        public int add(Employee item)
        {
             _context.Employees.Add(item);
            return  _context.SaveChanges();
        }

      
        public int delete(Employee item)
        {
            _context.Employees.Remove(item);
            return _context.SaveChanges();
        }

       
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.Include(d=>d.Department).ToList();
        }

        
        public Employee getbyid(int id)
        {
            return _context.Set<Employee>().Find(id);
        }


        public int update(Employee dep)
        {
            _context.Employees.Update(dep);
            return _context.SaveChanges();
        }
    }
}