using System.Collections.Generic;

namespace JeopardyWebAPI.Models
{
    public class CategoriesModel
    {
        public int Id { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }


        public ICollection<QuestionsModel> Questions { get; set; }

    }
}
