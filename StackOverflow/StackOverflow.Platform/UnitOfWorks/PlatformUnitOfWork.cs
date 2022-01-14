﻿using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Platform.Contexts;
using StackOverflow.Platform.Repositories;

namespace StackOverflow.Platform.UnitOfWorks
{
    public class PlatformUnitOfWork : UnitOfWork, IPlatformUnitOfWork
    {
        public IQuestionRepository Questions { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public PlatformUnitOfWork(IPlatformDbContext context,
            IQuestionRepository questions,
            ICommentRepository comments
            ) : base((DbContext)context)
        {
            Questions = questions;
            Comments = comments;
        }
    }
}
