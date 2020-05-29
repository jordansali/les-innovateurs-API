using System;
using System.Collections.Generic;

namespace JeopardyWebApp.Models
{
    public partial class PlayersModel
    {
        public int PlayerId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }
        public int? Ranking { get; set; }
    }
}
