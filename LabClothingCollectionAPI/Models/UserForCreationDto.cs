using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LabClothingCollectionAPI.Models
{
    public class UserForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Gender { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "today", ErrorMessage = "Invalid birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string CPF_CNPJ { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [EnumDataType(typeof(UserType))]
        public UserType Type { get; set; }
        [Required]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
    }
}
