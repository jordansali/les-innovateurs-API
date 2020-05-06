using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CategoriesAPI.Models
{
    public partial class Questions
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionEn { get; set; }
        public string QuestionFr { get; set; }
        public string AnswerEn { get; set; }
        public string AnswerFr { get; set; }
        public int? CategoryId { get; set; }
        public int? Points { get; set; }
        public int? TimeLimit { get; set; }
        public string Hint { get; set; }

        public virtual Categories Category { get; set; }
    }
}
