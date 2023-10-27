using Aspose.Words;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using RA_KYC_BE.Application.Dtos;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Dtos.OFAC;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Utils;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class OFACController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;

        public OFACController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpPost("SaveRiskCategoriesWithClientAndResults")]
        public async Task<IActionResult> SaveRiskCategoriesWithClientAndResults([FromBody] OFACAssessmentCheckedDto<List<OFACAssessmentBasisWithClientDto>> model)
        {
            try
            {
                var mitigatingControls = new List<OFACControlsWithClient>();
                var ofacAssessmentBasisWithClient = new List<OFACAssessmentBasisWithClient>();
                var riskMatrices = new List<OFACRiskMatrix>();
                foreach (var ofaItems in model.Options)
                {
                    if (ofaItems.IsChecked)
                    {
                        riskMatrices.Add(new OFACRiskMatrix()
                        {
                            ClientId = ofaItems.ClientId,
                            Code = ofaItems.RiskCategoryCode,
                            Category = ofaItems.RiskCategoryName,
                            InherentRisk = ofaItems.InherentRisk + "\t" + ofaItems.InherentRiskScore,
                            MitigatingControls = ofaItems.MitigatingControl + "\t" + ofaItems.MitigatingControlScore,
                            ResidualRisk = ofaItems.ResidualRisk + "\t" + ofaItems.ResidualRiskScore
                        });
                    }
                    ofacAssessmentBasisWithClient.Add(_mapper.Map<OFACAssessmentBasisWithClient>(ofaItems));
                    mitigatingControls.AddRange(_mapper.Map<List<OFACControlsWithClient>>(ofaItems.MitigatingControls));
                }
                await _unitOfWork.OFACs.SaveRiskCategoriesWithClientAndResults(ofacAssessmentBasisWithClient, mitigatingControls, riskMatrices, model.IsMainTable);
                return Ok(await _unitOfWork.Complete());
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddOFACAssessmentBasisDto ofacAssessmentBasisDto)
        {
            var riskCategories = _mapper.Map<OFACAssessmentBasis>(ofacAssessmentBasisDto);
            riskCategories.CreatedBy = UserId;
            riskCategories.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.OFACs.Add(riskCategories);
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
            var riskCategories = await _unitOfWork.OFACs.GetById(Id);
            return Ok(_mapper.Map<OFACDto>(riskCategories));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var riskCategories = await _unitOfWork.OFACs.GetAll();
            var mitigatingControls = await _unitOfWork.OFACControls.GetAll();
            var riskCategoriesDtos = _mapper.Map<List<OFACDto>>(riskCategories);
            foreach (var riskCategory in riskCategoriesDtos)
            {
                foreach (var childrenRiskCategory in mitigatingControls)
                {
                    if (riskCategory.RiskCategoryCode == childrenRiskCategory.Code)
                    {
                        riskCategory.MitigatingControls.Add(new OFACControlsDto()
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
            return Ok(new OFACAssessmentCheckedDto<List<OFACDto>>()
            {
                Options = riskCategoriesDtos,
                IsMainTable = true
            });
        }

        [HttpGet("GetAllByClientId/{ClientId}")]
        public async Task<IActionResult> GetAllByClientId(int ClientId)
        {
            var riskCategories = await _unitOfWork.OFACs.GetAllOFACRABasisByClientId(ClientId);
            var mitigatingControls = await _unitOfWork.OFACControls.GetAllOFACControlsByClientId(ClientId);
            var riskCategoriesDtos = _mapper.Map<List<OFACAssessmentBasisWithClientDto>>(riskCategories);
            foreach (var riskCategory in riskCategoriesDtos)
            {
                foreach (var childrenRiskCategory in mitigatingControls)
                {
                    if (riskCategory.RiskCategoryCode == childrenRiskCategory.Code)
                    {
                        riskCategory.MitigatingControls.Add(new OFACControlsWithClientDto()
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
            return Ok(new OFACAssessmentCheckedDto<List<OFACAssessmentBasisWithClientDto>>()
            {
                Options = riskCategoriesDtos,
                IsMainTable = false
            });
        }

        [HttpGet("PrintOFACReport/{ClientId}")]
        public async Task<IActionResult> GenerateDocument(int ClientId)
        {
            try
            {
                string wwwPath = _environment.WebRootPath;
                var docsfolder = Path.Combine(wwwPath, "Docs");
                if (!Directory.Exists(docsfolder))
                {
                    Directory.CreateDirectory(docsfolder);
                }
                string templatePath = Path.Combine(docsfolder, "2920 Wall OFAC-RA.docx");
                var ofacTemplate = new Document(templatePath);
                var ofacRiskMatrices = await _unitOfWork.OFACs.GetMatricesByClientId(ClientId);
                var client = ofacRiskMatrices[0].Client;
                var clientInfo = new
                {
                    CLIENTINFO = client != null ? client.ClientName : "",
                    PrintDate = DateTimeOffset.UtcNow.ToString("MM/dd/yyyy")
                };

                var data = new List<OFACRiskMatrixDTO>();
                ofacRiskMatrices.ForEach(r => data.Add(
                    new OFACRiskMatrixDTO()
                    {
                        Code = r.Code,
                        Category = r.Category,
                        InherentRisk = r.InherentRisk,
                        MitigatingControls = r.MitigatingControls,
                        ResidualRisk = r.ResidualRisk,
                    }));


                ofacTemplate.MailMerge.Execute(new string[] { "CLIENTINFO", "PrintDate" },
                                  new object[] { clientInfo.CLIENTINFO, clientInfo.PrintDate });
                // Execute the mail merge
                var d = Utils.ToDataTable(data);
                ofacTemplate.MailMerge.ExecuteWithRegions(d);

                var stream = new MemoryStream();
                ofacTemplate.Save(stream, SaveFormat.Pdf);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "2920WallOFACA-RA.pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetMatricesByClientId/{ClientId}")]
        public async Task<IActionResult> GetMatricesByClientId(int ClientId) => Ok(await _unitOfWork.OFACs.GetMatricesByClientId(ClientId));

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] OFACDto riskCategoriesDto)
        {
            var riskCategories = await _unitOfWork.OFACs.GetById(riskCategoriesDto.Id);
            riskCategories.RiskCategoryName = riskCategoriesDto.RiskCategoryName;
            riskCategories.IsActive = riskCategoriesDto.IsActive;
            riskCategories.UpdatedBy = UserId;
            riskCategories.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var riskCategories = await _unitOfWork.OFACs.GetById(Id);
            await _unitOfWork.OFACs.Remove(riskCategories);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
