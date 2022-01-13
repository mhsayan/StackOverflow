﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflow.Data;
using StackOverflow.Membership.Entities;

namespace StackOverflow.Platform.Entities
{
    public class Question : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
