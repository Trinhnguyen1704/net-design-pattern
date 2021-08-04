using System;
using net_design_pattern.Domain.Models;

namespace net_design_pattern.Persistence.BaseEntity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
        public DateTime? CreatedAt {get; set;}

        public DateTime? UpdatedAt {get; set;}
        public Account UpdatedBy {get; set;}
        public bool IsDeleted {get; set;}
        
    }
}
//This follows the DRY principle, you don't have to repeat these fields in each entity 