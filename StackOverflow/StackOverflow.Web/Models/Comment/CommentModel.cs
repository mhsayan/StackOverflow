﻿using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Comment
{
    public class CommentModel
    {
        [Required]
        public Guid QuestionId { get; set; }
        public BO.Question Question { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        private ICommentService _commentService;
        private IQuestionService _questionService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public CommentModel()
        {

        }

        public CommentModel(IMapper mapper, ICommentService commentService,
            IQuestionService questionService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _questionService = questionService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _commentService = _scope.Resolve<ICommentService>();
            _questionService = _scope.Resolve<IQuestionService>();
        }

        public void Delete(Guid id)
        {
            _commentService.Delete(id);
        }

        public void GetComment(Guid id)
        {
            var comment = _commentService.GetComment(id);
            _mapper.Map(comment, this);
        }
    }
}
