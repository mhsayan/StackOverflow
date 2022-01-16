using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public interface ICommentService
    {
        void CreateCommentAsync(string commentBody, Guid questionId);
        IList<BO.Question> GetQuestionListAsync();
        BO.Question GetQuestionAsync(Guid id);
        void Delete(Guid id);
        BO.Comment GetComment(Guid id);
        void AcceptAnswer(Guid id);
    }
}
