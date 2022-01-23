using School.Core.Entities;

namespace School.Infrastructure.Tests.Builders
{
    public class GroupBuilder
    {
        private Group _group;
        public int TestGroupId => 18;
        public int TestCourseId => 5;
        public string TestGroupName => "Group";

        public GroupBuilder()
        {
            _group = WithDefaultValues();
        }
        public Group Build()
        {
            return _group;
        }

        public Group WithDefaultValues()
        {
            return Group.Create(TestGroupId, TestCourseId, TestGroupName);
        }
    }
}
