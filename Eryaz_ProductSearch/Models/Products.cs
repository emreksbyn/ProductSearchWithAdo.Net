using System.ComponentModel.DataAnnotations;

namespace Eryaz_ProductSearch.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
    }
}
