using data_Access_layer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBLL.interfaces
{
    public interface IGenericRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync(); // Corrected method name to `GetAllAsync` for clarity  

        Task<T> GetByIdAsync(int id); // Added return type and corrected name to `GetByIdAsync`  

        Task AddAsync(T entity); // Standardized method name to `AddAsync`  

        void UpdateAsync(T entity); 

        void DeleteAsync(T entity);
    }
}
