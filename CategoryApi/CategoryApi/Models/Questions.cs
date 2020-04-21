using System;
using System.Collections.Generic;

namespace CategoryApi.Models
{
    public partial class Questions
    {
        public int QuestionId { get; set; }
        public int? CategoryId { get; set; }
        public string TextEn { get; set; }
        public string TextFr { get; set; }
        public string DifficultyVal { get; set; }
        public string AnswerEn { get; set; }
        public string AnswerFr { get; set; }

        public virtual Category Category { get; set; }
    }
}
