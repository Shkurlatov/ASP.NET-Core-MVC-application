﻿using School.Domain.Entities;
using School.Domain.Specifications.Base;

namespace School.Domain.Specifications
{
    public sealed class StudentByGroupSpecification : BaseSpecification<Student>
    {
        public StudentByGroupSpecification(int groupId)
            : base(s => s.GroupId == groupId)
        {
            AddInclude(s => s.Group);
        }
    }
}
