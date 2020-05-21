using JeopardyWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebApp.Controllers
{

    public abstract class QuestionsDBContextController<TEntity, TRepository> : ControllerBase where TEntity : class, IQuestionEntity where TRepository : IQuestionRepository<TEntity>
    {
    }
}
