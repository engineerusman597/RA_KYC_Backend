﻿using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Domain.Entities
{
    public class BSAAssessmentBasis : BaseEntity
    {
        [Required]
        public string RiskCategoryCode { get; set; }
        public string RiskCategoryName { get; set; }
        public string LowRiskQuestion { get; set; }
        public string ModerateRiskQuestion { get; set; }
        public string HighRiskQuestion { get; set; }
        public string RiskCategoryNumber { get; set; }
        public string RowInFFIECAppendix { get; set; }
    }
}
