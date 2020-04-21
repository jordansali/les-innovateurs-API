using System;
using System.Collections.Generic;

namespace CategoryApi.Models
{
    public partial class Category
    {
        public Category()
        {
            Questions = new HashSet<Questions>();
        }

        public int CategoryId { get; set; }
        public string TitleEn { get; set; }
        public string TitleFr { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
