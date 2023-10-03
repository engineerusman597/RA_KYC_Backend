using ExcelDataReader;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.GenericRepositories;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class BSARepository : GenericRepository<BSAAssessmentBasis>, IBSARepository
    {
        private readonly AppDbContext _context;

        public BSARepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BSAControls>> GetAllBSAControls() => await _context.BSAControls.ToListAsync();

        public async Task ImportMitigatingControlsFiles(ImportFilesModel importRiskCategoriesModel)
        {
            try
            {
                if (importRiskCategoriesModel.File == null || importRiskCategoriesModel.File.Length == 0) { }
                else
                {
                    string fileExtension = Path.GetExtension(importRiskCategoriesModel.File.FileName);
                    if (fileExtension != ".xls" && fileExtension != ".xlsx") { }
                    else
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        using var stream = new MemoryStream();
                        importRiskCategoriesModel.File.CopyTo(stream);
                        bool IsHeaderLoopItrate = false;
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            int count = reader.FieldCount;
                            if (importRiskCategoriesModel.File.FileName == "BSA-AML Control.xlsx")
                            {
                                List<BSAControls> mitigatingControls = new List<BSAControls>();
                                while (reader.Read())
                                {
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        var code = Convert.ToString(reader.GetValue(0)).Trim();
                                        var controlCode = Convert.ToString(reader.GetValue(1)).Trim();
                                        var category = Convert.ToString(reader.GetValue(2)).Trim();
                                        var strong3 = Convert.ToString(reader.GetValue(3)).Trim();
                                        var adequate2 = Convert.ToString(reader.GetValue(4)).Trim();
                                        var weak1 = Convert.ToString(reader.GetValue(5)).Trim();
                                        var score = Convert.ToString(reader.GetValue(6)).Trim();
                                        var comments = Convert.ToString(reader.GetValue(7)).Trim();
                                        var documents = Convert.ToString(reader.GetValue(8)).Trim();

                                        if (code == "Code" && controlCode == "Control Code" &&
                                            category == "Category" && strong3 == "Strong (3)" &&
                                            adequate2 == "Adequate (2)" && weak1 == "Weak (1)" &&
                                            score == "Score" && comments == "Comments:" && documents == "Documents")
                                        {
                                            IsHeaderLoopItrate = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(reader.GetValue(0))))
                                        {
                                            mitigatingControls.Add(new BSAControls
                                            {
                                                ParentCode = Convert.ToString(reader.GetValue(0)).Trim(),
                                                ControlCode = Convert.ToString(reader.GetValue(1)).Trim(),
                                                Category = Convert.ToString(reader.GetValue(2)).Trim(),
                                                Strong3 = Convert.ToString(reader.GetValue(3)),
                                                Adequate2 = (Convert.ToString(reader.GetValue(4)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(4)).Trim(),
                                                Weak1 = (Convert.ToString(reader.GetValue(5)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(5)).Trim(),
                                                Score = Convert.ToDecimal(Convert.ToString(reader.GetValue(6)).Trim() == "_" ? 0.00 : Convert.ToString(reader.GetValue(6)).Trim()),
                                                Comments = Convert.ToString(reader.GetValue(7)),
                                                Documents = Convert.ToString(reader.GetValue(8)),
                                                CreatedOn = DateTimeOffset.UtcNow
                                            });
                                        }
                                    }
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        break;
                                    }
                                }

                                _context.BSAControls.AddRange(mitigatingControls);
                                _context.SaveChanges();
                            }
                            else if (importRiskCategoriesModel.File.FileName == "BSA-AML Assessment Basis.xlsx")
                            {
                                List<BSAAssessmentBasis> riskCategories = new List<BSAAssessmentBasis>();
                                while (reader.Read())
                                {
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        var code = Convert.ToString(reader.GetValue(0)).Trim();
                                        var riskCategory = Convert.ToString(reader.GetValue(1)).Trim();
                                        var lowRisk = Convert.ToString(reader.GetValue(2)).Trim();
                                        var moderateRisk = Convert.ToString(reader.GetValue(3)).Trim();
                                        var highRisk = Convert.ToString(reader.GetValue(4)).Trim();
                                        var RiskCategoryNumber = Convert.ToString(reader.GetValue(5)).Trim();
                                        var RowInFFIECAppendix = Convert.ToString(reader.GetValue(6)).Trim();

                                        if (code == "Code" && riskCategory == "Risk Category" && lowRisk == "Low Risk"
                                            && moderateRisk == "Moderate Risk" && highRisk == "High Risk"
                                            && RiskCategoryNumber == "Risk Category #" && RowInFFIECAppendix== "Row in FFIEC Appendix J")
                                        {
                                            IsHeaderLoopItrate = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(reader.GetValue(0))))
                                        {
                                            riskCategories.Add(new BSAAssessmentBasis
                                            {
                                                RiskCategoryCode = Convert.ToString(reader.GetValue(0)).Trim(),
                                                RiskCategoryName = Convert.ToString(reader.GetValue(1)).Trim(),
                                                LowRiskQuestion = Convert.ToString(reader.GetValue(2)).Trim(),
                                                ModerateRiskQuestion = Convert.ToString(reader.GetValue(3)),
                                                HighRiskQuestion = (Convert.ToString(reader.GetValue(4)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(4)).Trim(),
                                                RiskCategoryNumber = (Convert.ToString(reader.GetValue(5)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(5)).Trim(),
                                                RowInFFIECAppendix = (Convert.ToString(reader.GetValue(6)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(6)).Trim(),
                                                CreatedOn = DateTimeOffset.UtcNow
                                            });
                                        }
                                    }
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        break;
                                    }
                                }

                                _context.BSAAssessmentBasis.AddRange(riskCategories);
                                _context.SaveChanges();
                            }
                            else if (importRiskCategoriesModel.File.FileName == "OFAC Assessment Basis.xlsx")
                            {
                                List<OFACAssessmentBasis> oFACAssessmentBasis = new();
                                while (reader.Read())
                                {
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        var code = Convert.ToString(reader.GetValue(0)).Trim();
                                        var riskCategory = Convert.ToString(reader.GetValue(1)).Trim();
                                        var lowRisk = Convert.ToString(reader.GetValue(2)).Trim();
                                        var moderateRisk = Convert.ToString(reader.GetValue(3)).Trim();
                                        var highRisk = Convert.ToString(reader.GetValue(4)).Trim();

                                        if (code == "Code" && riskCategory == "Risk Category" && lowRisk == "Low Risk"
                                            && moderateRisk == "Moderate Risk" && highRisk == "High Risk")
                                        {
                                            IsHeaderLoopItrate = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(reader.GetValue(0))))
                                        {
                                            oFACAssessmentBasis.Add(new OFACAssessmentBasis
                                            {
                                                RiskCategoryCode = Convert.ToString(reader.GetValue(0)).Trim(),
                                                RiskCategoryName = Convert.ToString(reader.GetValue(1)).Trim(),
                                                LowRiskQuestion = Convert.ToString(reader.GetValue(2)).Trim(),
                                                ModerateRiskQuestion = Convert.ToString(reader.GetValue(3)),
                                                HighRiskQuestion = (Convert.ToString(reader.GetValue(4)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(4)).Trim(),
                                                CreatedOn = DateTimeOffset.UtcNow
                                            });
                                        }
                                    }
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        break;
                                    }
                                }

                                await _context.OFACAssessmentBasis.AddRangeAsync(oFACAssessmentBasis);
                                await _context.SaveChangesAsync();
                            }
                            else if (importRiskCategoriesModel.File.FileName == "OFAC Control.xlsx")
                            {
                                List<OFACControl> oFACControls = new List<OFACControl>();
                                while (reader.Read())
                                {
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        var code = Convert.ToString(reader.GetValue(0)).Trim();
                                        var controlCode = Convert.ToString(reader.GetValue(1)).Trim();
                                        var category = Convert.ToString(reader.GetValue(2)).Trim();
                                        var strong3 = Convert.ToString(reader.GetValue(3)).Trim();
                                        var adequate2 = Convert.ToString(reader.GetValue(4)).Trim();
                                        var weak1 = Convert.ToString(reader.GetValue(5)).Trim();
                                        var score = Convert.ToString(reader.GetValue(6)).Trim();
                                        var comments = Convert.ToString(reader.GetValue(7)).Trim();
                                        var documents = Convert.ToString(reader.GetValue(8)).Trim();

                                        if (code == "Code" && controlCode == "Control Code" &&
                                            category == "Category" && strong3 == "Strong (3)" &&
                                            adequate2 == "Adequate (2)" && weak1 == "Weak (1)" &&
                                            score == "Score" && comments == "Comments:" && documents == "Documents")
                                        {
                                            IsHeaderLoopItrate = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(reader.GetValue(0))))
                                        {
                                            oFACControls.Add(new OFACControl
                                            {
                                                ParentCode = Convert.ToString(reader.GetValue(0)).Trim(),
                                                ControlCode = Convert.ToString(reader.GetValue(1)).Trim(),
                                                Category = Convert.ToString(reader.GetValue(2)).Trim(),
                                                Strong3 = Convert.ToString(reader.GetValue(3)),
                                                Adequate2 = (Convert.ToString(reader.GetValue(4)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(4)).Trim(),
                                                Weak1 = (Convert.ToString(reader.GetValue(5)).Trim() == "N/A") ? string.Empty : Convert.ToString(reader.GetValue(5)).Trim(),
                                                Score = Convert.ToDecimal(Convert.ToString(reader.GetValue(6)).Trim() == "_" ? 0.00 : Convert.ToString(reader.GetValue(6)).Trim()),
                                                Comments = Convert.ToString(reader.GetValue(7)),
                                                Documents = Convert.ToString(reader.GetValue(8)),
                                                CreatedOn = DateTimeOffset.UtcNow
                                            });
                                        }
                                    }
                                    if (IsHeaderLoopItrate == false)
                                    {
                                        break;
                                    }
                                }

                                await _context.OFACControl.AddRangeAsync(oFACControls);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
