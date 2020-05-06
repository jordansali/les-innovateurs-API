using System;
using System.Collections.Generic;

namespace CategoriesAPI.Models
{
    public partial class Players
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }
        public int? Ranking { get; set; }
    }
}
