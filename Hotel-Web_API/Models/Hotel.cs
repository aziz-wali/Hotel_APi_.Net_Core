using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Web_API.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public decimal? Price  { get; set; }
        public string? Website  { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        public string? Image { get; set; }

            
    }
}
