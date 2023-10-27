namespace RA_KYC_BE.Application.Dtos.OFAC
{
    public class OFACAssessmentCheckedDto<T>
    {
        public T Options { get; set; }
        //public List<GetBSARiskMatrixDto> BSARiskMatrix { get; set; }
        public bool IsMainTable { get; set; }
    }
}
