using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.CustomerTypes
{
    public class CustomerTypesDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
