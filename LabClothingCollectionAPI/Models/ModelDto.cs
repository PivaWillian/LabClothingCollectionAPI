using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabClothingCollectionAPI.Models
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Modelo Models { get; set; }
        public Layout Layout { get; set; }
    }
}
