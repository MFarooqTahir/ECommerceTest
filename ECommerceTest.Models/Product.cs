namespace ECommerceTest.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Cost { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}