using System.ComponentModel.DataAnnotations;

namespace LabClothingCollectionAPI.Models
{
    public class UserForUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "today", ErrorMessage = "Invalid birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        [EnumDataType(typeof(UserType))]
        public UserType Type { get; set; }
    }
}
