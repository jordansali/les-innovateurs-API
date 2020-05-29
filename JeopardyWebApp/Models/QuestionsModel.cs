using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeopardyWebApp.Models
{
    public class QuestionsModel
    {      
        public CategoriesModel Category { get; set; }   // not sure if required based on entities' relationships
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        [Required]
        [StringLength(250)]
        public string QuestionEn { get; set; }                        
        [Required]
        public string AnswerFr { get; set; }                        
        [Required]
        [Range(100,1000)]
        public int? Points { get; set; }        
        [Required]
        [Range(10,60)]
        public int? TimeLimit { get; set; }

        // Optional attributes
        [StringLength(250)]
        public string QuestionFr { get; set; }
        public string AnswerEn { get; set; }
        public string Hint { get; set; }
    }
}
