using Microsoft.AspNetCore.Http;

namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class ImportFilesModel
    {
        public IFormFile File { get; set; }
    }
}
