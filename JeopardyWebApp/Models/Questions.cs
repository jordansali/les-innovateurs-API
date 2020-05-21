using System.Collections.Generic;
using JeopardyWebApp.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeopardyWebApp.Models
{
    public partial class Questions : IQuestionEntity
    {      
        public virtual Categories Category { get; set; }

        [Key]
        public int Id { get; set; }
        public string Question_En { get; set; }
        public string Question_Fr { get; set; }
        public string Answer_En { get; set; }
        public string Answer_Fr { get; set; }
        public int? Category_Id { get; set; }

        public int? Points { get; set; }
        public int? TimeLimit { get; set; }
        public string Hint { get; set; }
    }
}
