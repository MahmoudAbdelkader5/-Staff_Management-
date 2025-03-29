using data_Access_layer.context;
using data_Access_layer.model;
using projectBLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectBLL.repo
{
    public class DepartmentRepo :IDepartmentRepo
    {

            private mvcAppDbcontext _context;
            public DepartmentRepo(mvcAppDbcontext context)
            {
                _context = context;
            }
        public int add(Department item)
        {
            _context.Add(item);
            return _context.SaveChanges();
        }

        public int delete(Department item)
        {
            _context.Remove(item);
            return _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department getbyid(int id)
        {
            return _context.Set<Department>().Find(id);
        }

        public int update(Department item)
        {
            _context.Update(item);
            return _context.SaveChanges();
        }
       
    }
}