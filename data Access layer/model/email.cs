using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_Access_layer.model
{
   public class email
    {
        public int id { get; set; }
        public  string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
