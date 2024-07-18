namespace CrudOperationsInNetCore.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? SurName { get; set; }

        public string? Category { get; set; }

        public string? Description { get; set; }

        public int IsActive { get; set; }
    }
}
