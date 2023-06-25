using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Gender { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "23/06/2023", ErrorMessage = "Invalid birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string DocNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string UserType { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public User(string fullName, string gender, string email, string docNumber, string phoneNumber, string userType, string status)
        {
            FullName = fullName;
            Gender = gender;
            Email = email;
            DocNumber = docNumber;
            PhoneNumber = phoneNumber;
            UserType = userType;
            Status = status;
        }
    }
}
