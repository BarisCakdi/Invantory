using System.ComponentModel.DataAnnotations.Schema;

namespace Invantory.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; } = 1;
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public string? ImgPath { get; set; } = "";
        public DateTime? ExpirationDate { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
