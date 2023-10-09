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
        public bool IsMainTable { get; set; }
    }
}
