namespace ProductShop.DTOs.Import
{
    public class ImportProductDto
    {
        // here we don't need to add attributes for the names of properties, because they are the same convention as in C#

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int SellerId { get; set; }

        public int? BuyerId { get; set; }
    }
}
