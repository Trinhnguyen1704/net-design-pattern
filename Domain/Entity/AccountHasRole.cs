using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("AccountHasRole")]
    public class AccountHasRole : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}