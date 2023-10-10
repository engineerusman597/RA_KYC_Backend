using RA_KYC_BE.Application.Dtos.BSARiskMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class BSAssessmentCheckedDto<T>
    {
        public T Options { get; set; }
        public List<GetBSARiskMatrixDto> BSARiskMatrix { get; set; }
        public bool IsMainTable { get; set; }
    }
}
