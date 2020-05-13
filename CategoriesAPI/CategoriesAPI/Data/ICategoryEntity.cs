﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriesAPI.Data
{
    public interface ICategoryEntity
    {
        int Id { get; set; }
        string CategoryName_En { get; set; }

        string CategoryName_Fr { get; set; }
    }
}