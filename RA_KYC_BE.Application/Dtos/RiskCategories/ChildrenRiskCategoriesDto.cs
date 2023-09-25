namespace RA_KYC_BE.Application.Dtos.RiskCategories
{
    public class ChildrenRiskCategoriesDto
    {
        public int Id { get; set; }
        public string ParentCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Strong { get; set; }
        public string StrongQuestion { get; set; }
        public string Adequate { get; set; }
        public string AdequateQuestion { get; set; }
        public string Weak { get; set; }
        public string WeakQuestion { get; set; }
        public double Score { get; set; }
    }
}
