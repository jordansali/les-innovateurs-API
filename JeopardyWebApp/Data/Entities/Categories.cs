using JeopardyWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeopardyWebApp.Data.Entities
{
    public class Categories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName_En { get; set; }
        public string CategoryName_Fr { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

    }
}
