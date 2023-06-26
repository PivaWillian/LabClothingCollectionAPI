using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LabClothingCollectionAPI.Enums;
using LabClothingCollectionAPI.Entities;

namespace LabClothingCollectionAPI.Models
{
    public class ModelForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("CollectionId")]
        public Collection? Collection { get; set; }
        public int CollectionId { get; set; }
        [Required]
        [EnumDataType(typeof(Modelo))]
        public Modelo Models { get; set; }
        [Required]
        [EnumDataType(typeof(Layout))]
        public Layout Layout { get; set; }
    }
}
