using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebApp.Data
{
    public interface IQuestionEntity
    {
        int Id { get; set; }
        string Question_En { get; set; }
        string Question_Fr { get; set; }
        string Answer_En { get; set; }
        string Answer_Fr { get; set; }
        int? Category_Id { get; set; }
        int? Points { get; set; }
        string Hint { get; set; }
    }
}
