using JeopardyWebApp.Models;

namespace JeopardyWebApp.Data.EFCore
{
    public class QuestionRepository : QuestionBaseRepository<Questions, JeopardyDbContext>
    {

        public QuestionRepository(JeopardyDbContext context) : base(context)
        {

        }
    }
}
