using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebAPI.Models
{
    public class CategoriesModel
    {
        public int Id { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
