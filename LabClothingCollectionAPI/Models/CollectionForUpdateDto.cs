using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Models
{
    public class CollectionForUpdateDto
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        [Required]
        public string Owner { get; set; } = string.Empty;
        [MaxLength(50)]
        [Required]
        public string Brand { get; set; } = string.Empty;
        [MaxLength(20)]
        [Required]
        public decimal Budget { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "today", ErrorMessage = "Invalid release date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [EnumDataType(typeof(Season))]
        public Season Season { get; set; }
    }
}
