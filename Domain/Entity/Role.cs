using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        public Role()
        {
            AccountHasRoles = new List<AccountHasRole>();
        }
        [Key]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        public virtual List<AccountHasRole> AccountHasRoles { get; set; }
    }
}