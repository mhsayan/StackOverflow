using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public interface IQuestionService
    {
        Task CreateQuestionAsync(BO.Question question);
        IList<BO.Question> GetQuestionListAsync();
        BO.Question GetQuestionAsync(Guid id);
    }
}
