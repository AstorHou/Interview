namespace Interview.DTOs
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; } = String.Empty;
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
    }
}
