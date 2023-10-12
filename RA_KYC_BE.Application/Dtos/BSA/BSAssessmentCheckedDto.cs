using RA_KYC_BE.Application.Dtos.BSARiskMatrix;

namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class BSAssessmentCheckedDto<T>
    {
        public T Options { get; set; }
        //public List<GetBSARiskMatrixDto> BSARiskMatrix { get; set; }
        public bool IsMainTable { get; set; }
    }
}
