namespace net_design_pattern.Domain.Models.DTOs
{
    public class ProductDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public float Price {get; set;}
        public int NumInStock {get; set;}
        public string Description {get; set;}
        public string Status {get; set;}
        public int CategoryId {get; set;}
        public CategoryDto Category {get; set;}

    }
}
//Data Transfer Object Design Pattern: is a data container for moving data between layers.