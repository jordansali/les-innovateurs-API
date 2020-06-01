using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeopardyWebApp.Data.Entities
{
    public class Players
    {
        public int PlayerId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }
        public int? Ranking { get; set; }
    }
}
