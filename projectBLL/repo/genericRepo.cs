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
    public class genericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly mvcAppDbcontext _context;
        public genericRepo(mvcAppDbcontext context)
        {
            _context = context;
        }
        public async Task AddAsync(T item)
        {
           
            await _context.AddAsync(item);
        }

        public void DeleteAsync(T item)
        {
            _context.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
            return (IEnumerable<T>) _context.Employees.Include(d=>d.Department).ToList();
            else
            {
                return await _context.Set<T>().ToListAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void UpdateAsync(T item)
        {
            _context.Update(item);
        }
    }
}
