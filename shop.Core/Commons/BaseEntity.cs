using System.ComponentModel.DataAnnotations;

namespace shop.Core.Commons
{
    public abstract class BaseEntity : Entity
    {
        [Key]
        public int Id { get; set; }

    }
}