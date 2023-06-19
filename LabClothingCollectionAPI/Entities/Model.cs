using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabClothingCollectionAPI.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("CollectionId")]
        public Collection? Collection { get; set; }
        public int CollectionId { get; set; }
        [Required]
        [EnumDataType(typeof(Modelo))]
        public Modelo Models { get; set; }
        [Required]
        [EnumDataType(typeof(Layout))]
        public Layout Layout { get; set; }

        public Model(string name)
        {
            Name = name;
        }

    }
}
