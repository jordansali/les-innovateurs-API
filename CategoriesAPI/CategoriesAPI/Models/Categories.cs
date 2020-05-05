using System;
using System.Collections.Generic;

namespace CategoriesAPI.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Questions = new HashSet<Questions>();
        }

        public int Id { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
