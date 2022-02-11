using System.ComponentModel.DataAnnotations;

namespace School.Domain.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}