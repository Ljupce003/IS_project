using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
