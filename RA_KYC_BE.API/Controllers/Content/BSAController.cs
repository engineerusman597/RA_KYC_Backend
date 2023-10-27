
using AutoMapper;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RA_KYC_BE.Application.Dtos;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Utils;
using RA_KYC_BE.Domain.Entities;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Data;
using System.Reflection;
using Xceed.Words.NET;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSAController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;

        public BSAController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpPost("SaveRiskCategoriesWithClientAndResults")]
        public async Task<IActionResult> SaveRiskCategoriesWithClientAndResults([FromBody] BSAssessmentCheckedDto<List<BSAAssessmentBasisWithClientDto>> model)
        {
            try
            {
                var mitigatingControls = new List<BSAControlsWithClient>();
                var bsaAssessmentBasisWithClient = new List<BSAAssessmentBasisWithClient>();
                var riskMatrices = new List<BSARiskMatrix>();
                foreach (var bsaItems in model.Options)
                {
                    if (bsaItems.IsChecked)
                    {
                        riskMatrices.Add(new BSARiskMatrix()
                        {
                            ClientId = bsaItems.ClientId,
                            Code = bsaItems.RiskCategoryCode,
                            RowInFFIECAppendix = bsaItems.RowInFFIECAppendix,
                            CategoryNumber = bsaItems.RiskCategoryNumber,
                            Category = bsaItems.RiskCategoryName,
                            InherentRisk = bsaItems.InherentRisk + "\t" + bsaItems.InherentRiskScore,
                            MitigatingControls = bsaItems.MitigatingControl + "\t" + bsaItems.MitigatingControlScore,
                            ResidualRisk = bsaItems.ResidualRisk + "\t" + bsaItems.ResidualRiskScore
                        });
                    }
                    bsaAssessmentBasisWithClient.Add(_mapper.Map<BSAAssessmentBasisWithClient>(bsaItems));
                    mitigatingControls.AddRange(_mapper.Map<List<BSAControlsWithClient>>(bsaItems.MitigatingControls));
                }
                await _unitOfWork.BSAs.SaveRiskCategoriesWithClientAndResults(bsaAssessmentBasisWithClient, mitigatingControls, riskMatrices, model.IsMainTable);
                return Ok(await _unitOfWork.Complete());
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBSAAssessmentBasisDto riskCategoriesDto)
        {
            var riskCategories = _mapper.Map<BSAAssessmentBasis>(riskCategoriesDto);
            riskCategories.CreatedBy = UserId;
            riskCategories.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.BSAs.Add(riskCategories);
            return Ok(await _unitOfWork.Complete());
        }

        [HttpPost("Import")]
        public async Task<IActionResult> ImportFiles([FromForm] ImportFilesModel importRiskCategoriesModel)
        {
            await _unitOfWork.BSAs.ImportMitigatingControlsFiles(importRiskCategoriesModel);
            return Ok(await _unitOfWork.Complete());
        }

        /// <summary>
        /// Get Customer Risk Factors by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var riskCategories = await _unitOfWork.BSAs.GetById(Id);
            return Ok(_mapper.Map<BSADto>(riskCategories));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var riskCategories = await _unitOfWork.BSAs.GetAll();
            var mitigatingControls = await _unitOfWork.BSAControls.GetAll();
            var riskCategoriesDtos = _mapper.Map<List<BSADto>>(riskCategories);
            foreach (var riskCategory in riskCategoriesDtos)
            {
                foreach (var childrenRiskCategory in mitigatingControls)
                {
                    if (riskCategory.RiskCategoryCode == childrenRiskCategory.Code)
                    {
                        riskCategory.MitigatingControls.Add(new BSAControlsDto()
                        {
                            Id = childrenRiskCategory.Id,
                            WeakQuestion = childrenRiskCategory.WeakQuestion,
                            AdequateQuestion = childrenRiskCategory.AdequateQuestion,
                            StrongQuestion = childrenRiskCategory.StrongQuestion,
                            Code = childrenRiskCategory.Code,
                            ControlCode = childrenRiskCategory.ControlCode,
                            Category = childrenRiskCategory.Category,
                            Score = Convert.ToDouble(childrenRiskCategory.Score),
                            Comments = childrenRiskCategory.Comments,
                            Documents = childrenRiskCategory.Documents,
                        });
                    }
                }
            }
            return Ok(new BSAssessmentCheckedDto<List<BSADto>>()
            {
                Options = riskCategoriesDtos,
                IsMainTable = true
            });
        }

        [HttpGet("GetAllByClientId/{ClientId}")]
        public async Task<IActionResult> GetAllByClientId(int ClientId)
        {
            var riskCategories = await _unitOfWork.BSAs.GetAllBSARABasisByClientId(ClientId);
            var mitigatingControls = await _unitOfWork.BSAControls.GetAllBSAControlsByClientId(ClientId);
            var riskCategoriesDtos = _mapper.Map<List<BSAAssessmentBasisWithClientDto>>(riskCategories);
            foreach (var riskCategory in riskCategoriesDtos)
            {
                foreach (var childrenRiskCategory in mitigatingControls)
                {
                    if (riskCategory.RiskCategoryCode == childrenRiskCategory.Code)
                    {
                        riskCategory.MitigatingControls.Add(new BSAControlsWithClientDto()
                        {
                            Id = childrenRiskCategory.Id,
                            ClientId = childrenRiskCategory.ClientId,
                            WeakQuestion = childrenRiskCategory.WeakQuestion,
                            AdequateQuestion = childrenRiskCategory.AdequateQuestion,
                            StrongQuestion = childrenRiskCategory.StrongQuestion,
                            Code = childrenRiskCategory.Code,
                            ControlCode = childrenRiskCategory.ControlCode,
                            Category = childrenRiskCategory.Category,
                            Score = childrenRiskCategory.Score,
                            Comments = childrenRiskCategory.Comments,
                            Documents = childrenRiskCategory.Documents
                        });
                    }
                }
            }
            return Ok(new BSAssessmentCheckedDto<List<BSAAssessmentBasisWithClientDto>>()
            {
                Options = riskCategoriesDtos,
                IsMainTable = false
            });
        }

        [HttpGet("PrintBSAReport/{ClientId}")]
        public async Task<IActionResult> GenerateDocument(int ClientId)
        {
            try
            {
                string wwwPath = _environment.WebRootPath;
                var docsfolder = Path.Combine(wwwPath, "Docs");
                if(!Directory.Exists(docsfolder)) 
                {
                    Directory.CreateDirectory(docsfolder);
                }
                string templatePath = Path.Combine(docsfolder, "2920 Wall BSA-RA.docx");
                Document doc = new Document();
                doc.LoadFromFile(templatePath);
                var bsaRiskMatrices = await _unitOfWork.BSAs.GetMatricesByClientId(ClientId);
                var client = bsaRiskMatrices[0].Client;
                var clientInfo = new
                {
                    CLIENTINFO = client != null ? client.ClientName : "",
                    PrintDate = DateTimeOffset.UtcNow.ToString("MM/dd/yyyy")
                };

                var data = new List<BSARiskMatrixDTO>();
                bsaRiskMatrices.ForEach(r => data.Add(
                    new BSARiskMatrixDTO()
                    {
                        Code = r.Code,
                        Category = r.Category,
                        CategoryNumber = r.CategoryNumber,
                        InherentRisk = r.InherentRisk,
                        MitigatingControls = r.MitigatingControls,
                        ResidualRisk = r.ResidualRisk,
                        RowInFFIECAppendix = r.RowInFFIECAppendix,
                    }));
                doc.Replace("«CLIENTINFO»", clientInfo.CLIENTINFO, true, true);
                doc.Replace("«PRINTDATE»", clientInfo.PrintDate, true, true);
                //bsaTemplate.MailMerge.Execute(new string[] { "CLIENTINFO", "PrintDate" },
                //                  new object[] { clientInfo.CLIENTINFO, clientInfo.PrintDate });
                // Find the table in the document

                // Create a section and add a table to it
                //Section section = doc.AddSection();
                //Table table = section.AddTable(true);
                //table.ResetCells(9, 5); // Adjust rows and columns as needed

                //// Set the table data (example data)
                //string[] headers = { "Date", "Description", "Country", "On Hands", "On Order" };
                ////string[][] datatable = data;

                //// Populate the table
                //TableRow headerRow = table.Rows[0];
                //for (int i = 0; i < headers.Length; i++)
                //{
                //    Paragraph paragraph = headerRow.Cells[i].AddParagraph();
                //    paragraph.AppendText(headers[i]);
                //}

                //for (int row = 0; row < data.Count; row++)
                //{
                //    TableRow dataRow = table.Rows[row + 1];
                //    for (int col = 0; col < 7; col++)
                //    {
                //        Paragraph paragraph = dataRow.Cells[col].AddParagraph();
                //        paragraph.AppendText(data[row][col]);
                //    }
                //}

                //// Find and replace the variable in the document with the table
                //string variableToReplace = "YourVariable"; // Replace with your actual variable
                //doc.Replace(variableToReplace, table, true, true);
                //Table table = (Table)doc.Sections[0].Tables[0];
                //// Iterate over the data and insert it into the table
                //// Find the row with placeholders (assumed to be in the first row of the table)
                //TableRow placeholderRow = table.Rows[0];

                //// Clone the placeholders row for each record
                //for (int i = 1; i < data.Count; i++)
                //{
                //    TableRow newRow = placeholderRow.Clone();
                //    table.Rows.Insert(i, newRow);
                //}

                //// Replace the placeholders with dynamic data
                //for (int i = 0; i < data.Count; i++)
                //{
                //    TableRow currentRow = table.Rows[i];
                //    currentRow.Cells[0].Paragraphs[0].AppendText(data[i].Code);
                //    currentRow.Cells[1].AddParagraph().AppendText(data[i].RowInFFIECAppendix);
                //    currentRow.Cells[2].AddParagraph().AppendText(data[i].CategoryNumber.ToString());
                //    currentRow.Cells[3].AddParagraph().AppendText(data[i].Category);
                //    currentRow.Cells[4].AddParagraph().AppendText(data[i].InherentRisk);
                //    currentRow.Cells[5].AddParagraph().AppendText(data[i].MitigatingControls);
                //    currentRow.Cells[6].AddParagraph().AppendText(data[i].ResidualRisk);
                //}

                //// Remove the original placeholders row
                //table.Rows.Remove(placeholderRow);
                var stream = new MemoryStream();
                //bsaTemplate.Save(stream, SaveFormat.Pdf);
                doc.SaveToFile(templatePath+DateTime.Now.ToString("dd-MM-yyyy"), FileFormat.Doc);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "2920WallBSA-RA.pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetMatricesByClientId/{ClientId}")]
        public async Task<IActionResult> GetMatricesByClientId(int ClientId) => Ok(await _unitOfWork.BSAs.GetMatricesByClientId(ClientId));

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] BSADto riskCategoriesDto)
        {
            var riskCategories = await _unitOfWork.BSAs.GetById(riskCategoriesDto.Id);
            riskCategories.RiskCategoryName = riskCategoriesDto.RiskCategoryName;
            riskCategories.IsActive = riskCategoriesDto.IsActive;
            riskCategories.UpdatedBy = UserId;
            riskCategories.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var riskCategories = await _unitOfWork.BSAs.GetById(Id);
            await _unitOfWork.BSAs.Remove(riskCategories);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
