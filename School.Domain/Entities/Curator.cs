namespace School.Domain.Entities
{
    public class Curator : User
    {
        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}
