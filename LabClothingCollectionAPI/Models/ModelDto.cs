using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LabClothingCollectionAPI.Enums;
using LabClothingCollectionAPI.Entities;

namespace LabClothingCollectionAPI.Models
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Collection? Collection { get; set; }
        public int CollectionId { get; set; }
        public Modelo Models { get; set; }
        public Layout Layout { get; set; }
    }
}
