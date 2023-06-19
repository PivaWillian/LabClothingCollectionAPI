using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabClothingCollectionAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string CPF_CNPJ { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserType Type { get; set; }
        public Status Status { get; set; }
    }
}
