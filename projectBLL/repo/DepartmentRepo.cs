using data_Access_layer.context;
using data_Access_layer.model;
using projectBLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectBLL.repo
{
    public class DepartmentRepo : genericRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(mvcAppDbcontext context) : base(context)
        {
        }





    }
}
