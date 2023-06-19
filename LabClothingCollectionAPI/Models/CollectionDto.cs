using System.ComponentModel.DataAnnotations;

namespace LabClothingCollectionAPI.Models
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Season Season { get; set; }
        public Status Status { get; set; }
    }
}
