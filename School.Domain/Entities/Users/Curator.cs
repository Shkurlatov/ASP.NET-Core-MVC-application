using School.Domain.Entities.Studies;

namespace School.Domain.Entities.Users
{
    public class Curator : User
    {
        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}
