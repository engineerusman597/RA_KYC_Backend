using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSAController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BSAController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            var mitigatingControls = await _unitOfWork.BSAs.GetAllBSAControls();
            var riskCategoriesDtos = _mapper.Map<List<BSADto>>(riskCategories);
            foreach (var riskCategory in riskCategoriesDtos)
            {
                foreach (var childrenRiskCategory in mitigatingControls)
                {
                    if(riskCategory.RiskCategoryCode == childrenRiskCategory.ParentCode)
                    {
                        riskCategory.ChildrenCategories.Add(new BSAControlsDto()
                        {
                            Id = childrenRiskCategory.Id,
                            Weak = "Weak (1)",
                            Adequate = "Adequate (2)",
                            Strong = "Strong (3)",
                            AdequateQuestion = childrenRiskCategory.Adequate2,
                            StrongQuestion = childrenRiskCategory.Strong3,
                            WeakQuestion = childrenRiskCategory.Weak1,
                            Code = childrenRiskCategory.ControlCode,
                            ParentCode = childrenRiskCategory.ParentCode,
                            Name = childrenRiskCategory.Category,
                            Score = Convert.ToDouble(childrenRiskCategory.Score),
                        });
                    }
                }
            }
            return Ok(riskCategoriesDtos);
        }

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
