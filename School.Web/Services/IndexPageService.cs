using School.Application.Interfaces;
using School.Web.Interfaces;
using School.Web.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Services
{
    public class IndexPageService : IIndexPageService
    {
        private readonly IStudentService _studentAppService;
        private readonly IGroupService _groupAppService;
        private readonly IMapper _mapper;

        public IndexPageService(IStudentService studentAppService, IGroupService groupAppService, IMapper mapper)
        {
            _studentAppService = studentAppService ?? throw new ArgumentNullException(nameof(studentAppService));
            _groupAppService = groupAppService ?? throw new ArgumentNullException(nameof(groupAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudents()
        {
            var list = await _studentAppService.GetStudentList();
            var mapped = _mapper.Map<IEnumerable<StudentViewModel>>(list);
            return mapped;
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroups()
        {
            var list = await _groupAppService.GetGroupList();
            var mapped = _mapper.Map<IEnumerable<GroupViewModel>>(list);
            return mapped;
        }
    }
}
