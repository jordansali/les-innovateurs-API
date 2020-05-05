using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CategoryApi.Models
{
    public class Categories
    {
        public Categories()
        {
            Questions = new HashSet<Questions>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
