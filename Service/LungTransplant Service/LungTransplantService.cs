using Domain;
using Domain.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Consts;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using DataAccess_EF;

namespace Service.LungTransplant_Service
{
    public class LungTransplantService : ILungTransplantService
    {
        private IUnitOfWork _unitOfWork;
        public LungTransplantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> AddRequestAsync(LungTransplantVM model, string userId)
        {
            var getRequestsByNationalId = await _unitOfWork.TbLungTransplant.GetWhereAsync(a => a.NationalId == model.NationalId && a.Status != Status.Rejected && a.Status != Status.Accepted);
            if (getRequestsByNationalId?.Any() == true)
                return OperationResult.Error("The Natopnal ID is already in processing");

            // Convert file from IFormFile to byte[]
            using var nationalImageStream = new MemoryStream();
            await model.NationalImage.CopyToAsync(nationalImageStream);

            using var chestRayImageStream = new MemoryStream();
            await model.ChestRayImage.CopyToAsync(chestRayImageStream);

            try
            {
                TbLungTransplant dt = new()
                {
                    FullName = model.FullName,
                    Age = model.Age,
                    Phone = model.Phone,
                    City = model.City,
                    NationalId = model.NationalId,
                    Address = model.Address,
                    UserId = userId,
                    NationalImage = nationalImageStream.ToArray(),
                    ChestRayImage = chestRayImageStream.ToArray(),
                    Status = Status.Pending
                };
                await _unitOfWork.TbLungTransplant.AddAsync(dt);
                await _unitOfWork.Complete();

                return OperationResult.Succeeded(dt.Id.ToString());
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

    }
}
