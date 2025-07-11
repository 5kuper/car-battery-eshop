using System.ComponentModel.DataAnnotations;

namespace BattAPI.Domain.Entities
{
    public class Entity
    {
        [Key] public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
