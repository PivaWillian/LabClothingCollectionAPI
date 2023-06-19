using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabClothingCollectionAPI.Entities
{
    public class Collection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Owner { get; set; }
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(20)]
        public decimal Budget { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "today", ErrorMessage = "Invalid release date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [EnumDataType(typeof(Season))]
        public Season Season { get; set; }
        [Required]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public Collection(string name, string owner, string brand)
        {
            Name = name;
            Owner = owner;
            Brand = brand;
        }
    }
}
