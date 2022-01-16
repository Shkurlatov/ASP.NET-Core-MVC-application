﻿using School.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetStudentList();
        Task<StudentModel> GetStudentById(int studentId);
        Task<IEnumerable<StudentModel>> GetStudentByLastName(string lastName);
        Task<IEnumerable<StudentModel>> GetStudentByGroup(int groupId);
        Task<StudentModel> Create(StudentModel studentModel);
        Task Update(StudentModel studentModel);
        Task Delete(StudentModel studentModel);
    }
}
