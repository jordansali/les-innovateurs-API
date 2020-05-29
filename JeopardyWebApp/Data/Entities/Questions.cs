using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeopardyWebApp.Data.Entities
{
    public class Questions
    {
        public Categories Category { get; set; }   // not sure if required based on entities' relationships

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Question_En { get; set; }
        [Required]
        public string Answer_Fr { get; set; }
        [Required]
        [Range(100, 1000)]
        public int? Points { get; set; }
        [Required]
        [Range(10, 60)]
        public int? TimeLimit { get; set; }

        // Optional attributes
        [StringLength(250)]
        public string Question_Fr { get; set; }
        public string Answer_En { get; set; }
        public string Hint { get; set; }
    }
}
