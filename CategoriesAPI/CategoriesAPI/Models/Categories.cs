using CategoriesAPI.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriesAPI.Models
{
    public partial class Categories : ICategoryEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Categories()
        {
            Questions = new HashSet<Questions>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CategoryName_En { get; set; }
        public string CategoryName_Fr { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
                
    }
}
