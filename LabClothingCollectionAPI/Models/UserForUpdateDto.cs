using System.ComponentModel.DataAnnotations;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Models
{
    public class UserForUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Gender { get; set; } = string.Empty;
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "23/06/2023", ErrorMessage = "Invalid birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string UserType { get; set; } = string.Empty;
    }
}
