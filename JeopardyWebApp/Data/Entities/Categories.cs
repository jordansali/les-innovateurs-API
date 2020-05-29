using JeopardyWebApp.Models;
using System.Collections.Generic;

namespace JeopardyWebApp.Data.Entities
{
    public class Categories
    {               
        public int CategoryId { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }

        public ICollection<Questions> Questions { get; set; }

    }
}
