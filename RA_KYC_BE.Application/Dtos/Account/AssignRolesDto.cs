using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account
{
    public class AssignRolesDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}