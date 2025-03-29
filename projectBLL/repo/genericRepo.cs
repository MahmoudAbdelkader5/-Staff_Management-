using data_Access_layer.context;
using data_Access_layer.model;
using Microsoft.EntityFrameworkCore;
using projectBLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBLL.repo
{
    public class genericRepo<T> : IgenericRepo<T> where T : class
    {
        private readonly mvcAppDbcontext _context;
        public genericRepo(mvcAppDbcontext context)
        {
            _context = context;
        }
        public void add(T item)
        {
           
            _context.Add(item);
        }

        public void delete(T item)
        {
            _context.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            return (IEnumerable<T>) _context.Employees.Include(d=>d.Department).ToList();
            else
            {
                return _context.Set<T>();
            }
        }

        public T getbyid(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void update(T item)
        {
            _context.Update(item);
        }
    }
}
