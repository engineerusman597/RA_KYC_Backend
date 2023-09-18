using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.Clients
{
    public class ClientsDto
    {
        public int Id { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
