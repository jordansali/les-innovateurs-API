using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeopardyWebApp.Models
{
    public class CategoriesModel
    {        
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName_En { get; set; }
        public string CategoryName_Fr { get; set; }

        public virtual ICollection<QuestionsModel> Questions { get; set; }


        /*
        // Include Question information <- optional
        public int QuestionId { get; set; }
        public string QuestionQuestionEn { get; set; }
        public string QuestionQuestionFr { get; set; }
        public string QuestionAnswerEn { get; set; }
        public string QuestionAnswerFr { get; set; }        
        public int? QuestionPoints { get; set; }
        public int? QuestionTimeLimit { get; set; }
        public string QuestionHint { get; set; }
        */
    }
}
