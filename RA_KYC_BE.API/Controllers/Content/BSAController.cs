﻿using Aspose.Words;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Utils;
using RA_KYC_BE.Domain.Entities;
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
                var bsaTemplate = new Document(templatePath);
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


                bsaTemplate.MailMerge.Execute(new string[] { "CLIENTINFO", "PrintDate" },
                                  new object[] { clientInfo.CLIENTINFO, clientInfo.PrintDate });
                // Execute the mail merge
                var d = Utils.ToDataTable(data);
                bsaTemplate.MailMerge.ExecuteWithRegions(d);

                var stream = new MemoryStream();
                bsaTemplate.Save(stream, SaveFormat.Pdf);
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
