﻿using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Platform.Services
{
    public interface ICommentService
    {
        void CreateCommentAsync(string commentBody, Guid questionId);
        void Delete(Guid id);
        void Delete(IList<BO.Comment> comments);
        BO.Comment GetComment(Guid id);
        void AcceptAnswer(Guid id);
    }
}
