using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Advice_Service
{
    public class AdviceService : IAdviceService
    {
        private IUnitOfWork _unitOfWork;
        public AdviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<OperationResult> AddAsync(AddAdviceVM model, IFormFile file)
        {
            // Convert Photo from IFormFile to byte[]
            using var dataStream = new MemoryStream();
            await file.CopyToAsync(dataStream);

            try
            {
                TbAdvice dt = new()
                {
                    Image = dataStream.ToArray(),
                    Title = model.Title,
                    Content = model.Content,
                    DoctorId = model.DoctorId,
                    DiseaseId = model.DiseaseId,
                    DiseaseTypeId = model.DiseaseTypeId,
                    AppUserId = model.UserId
                };

                await _unitOfWork.TbAdvices.AddAsync(dt);
                return OperationResult.Succeeded("Add New Advice Successfully!");

            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public async Task<OperationResult> UpdateAsync(AddAdviceVM model, IFormFile file)
        {
            // Convert Photo from IFormFile to byte[]
            using var dataStream = new MemoryStream();
            await file.CopyToAsync(dataStream);

            try
            {
                var advice = await _unitOfWork.TbAdvices.GetFirstOrDefaultAsync(x => x.Id == model.Id);
                advice.Title = model.Title;
                advice.Content = model.Content;
                advice.Image = dataStream.ToArray();
                advice.DoctorId = model.DoctorId;
                advice.DiseaseId = model.DiseaseId;
                advice.DiseaseTypeId = model.DiseaseTypeId;
                advice.AppUserId = model.UserId;

                _unitOfWork.TbAdvices.Update(advice);
                return OperationResult.Succeeded("Update Advice Successfully!");
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }
    }
}
