using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Models
{
    public class ModelForUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EnumDataType(typeof(Modelo))]
        public Modelo Models { get; set; }
        [Required]
        [EnumDataType(typeof(Layout))]
        public Layout Layout { get; set; }
    }
}
