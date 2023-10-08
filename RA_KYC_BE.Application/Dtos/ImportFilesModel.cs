using Microsoft.AspNetCore.Http;

namespace RA_KYC_BE.Application.Dtos
{
    public class ImportFilesModel
    {
        public IFormFile File { get; set; }
    }
}
