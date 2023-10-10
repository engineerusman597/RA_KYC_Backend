using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Application.Dtos.BSARiskMatrix
{
    public class GetBSARiskMatrixDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string RowInFFIECAppendix { get; set; }
        public string CategoryNumber { get; set; }
        public string Category { get; set; }
        public string InherentRisk { get; set; }
        public string MitigatingControls { get; set; }
        public string ResidualRisk { get; set; }
        public int ClientId { get; set; }
    }
}
