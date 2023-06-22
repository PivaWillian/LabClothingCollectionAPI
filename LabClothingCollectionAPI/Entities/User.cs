using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabClothingCollectionAPI.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Gender { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "today", ErrorMessage = "Invalid birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string CPF_CNPJ { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        [EnumDataType(typeof(UserType))]
        public UserType Type { get; set; }
        [Required]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public User(string fullName, string gender, string email, string cpf_cnpj, string phoneNumber)
        {
            FullName = fullName;
            Gender = gender;
            Email = email;
            CPF_CNPJ = cpf_cnpj;
            PhoneNumber = phoneNumber;
        }
    }
}
