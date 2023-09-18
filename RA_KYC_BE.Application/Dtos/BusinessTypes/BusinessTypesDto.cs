using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.BusinessTypes
{
    public class BusinessTypesDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
