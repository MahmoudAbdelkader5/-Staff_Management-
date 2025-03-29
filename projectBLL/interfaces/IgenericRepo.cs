using data_Access_layer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBLL.interfaces
{
    public interface IgenericRepo<T>
    {
         IEnumerable<T> GetAll();

        T getbyid(int id);
        public void add(T dep);

        public void update(T dep);
        public void delete(T dep);
    }
}
