using PRN232.Lab3.CosmeticsStore.Repository.Entities;
using PRN232.Lab3.CosmeticsStore.Repository.Interfaces;
using PRN232.Lab3.CosmeticsStore.Service.Interface;
using PRN232.Lab3.CosmeticsStore.Service.Payload.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Implement
{
    public class CosmeticInformationService: ICosmeticInformationService
	{
        private readonly IUnitOfWork _unitOfWork;
		public CosmeticInformationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<CosmeticInformation> Add(CosmeticInformationRequest request)
		{
			CosmeticInformation cosmetic = new CosmeticInformation()
			{
				CosmeticId = request.CosmeticId,
				CategoryId = request.CategoryId,
				CosmeticName = request.CosmeticName,
				CosmeticSize = request.CosmeticSize,
				DollarPrice = request.DollarPrice,
				ExpirationDate = request.ExpirationDate,
				SkinType = request.SkinType
			};
			await _unitOfWork.GetRepository<CosmeticInformation>().InsertAsync(cosmetic);
			_unitOfWork.CommitAsync();
			return cosmetic;
		}

		public Task<CosmeticInformation> Delete(string id)
		{
			var cosmetic = GetById(id).Result;
			if (cosmetic == null)
			{
				throw new Exception("Cosmetic not found");
			}
			_unitOfWork.GetRepository<CosmeticInformation>().DeleteAsync(cosmetic);
			_unitOfWork.CommitAsync();
			return Task.FromResult(cosmetic);
		}

		public Task<List<CosmeticCategory>> GetAllCategories()
		{
			var categories = _unitOfWork.GetRepository<CosmeticCategory>().GetListAsync();

			return Task.FromResult(categories.Result.ToList());
		}

		public Task<List<CosmeticInformation>> GetAllCosmetics()
		{
			var cosmetics = _unitOfWork.GetRepository<CosmeticInformation>().GetListAsync();
			return Task.FromResult(cosmetics.Result.ToList());
		}

		public async Task<CosmeticInformation> GetById(string id)
		{
			var cosmetic = await _unitOfWork.GetRepository<CosmeticInformation>().SingleOrDefaultAsync(predicate: x => x.CosmeticId == id);
			if (cosmetic == null)
			{
				throw new Exception("Cosmetic not found");
			}
			return cosmetic;
		}


		public Task<CosmeticInformation> Update(CosmeticInformationRequest request)
		{
			var cosmetic = GetById(request.CosmeticId).Result;
			if (cosmetic == null)
			{
				throw new Exception("Cosmetic not found");
			}
			cosmetic.CosmeticId = request.CosmeticId;
			cosmetic.CategoryId = request.CategoryId;
			cosmetic.CosmeticName = request.CosmeticName;
			cosmetic.CosmeticSize = request.CosmeticSize;
			cosmetic.DollarPrice = request.DollarPrice;
			cosmetic.ExpirationDate = request.ExpirationDate;
			cosmetic.SkinType = request.SkinType;
			_unitOfWork.GetRepository<CosmeticInformation>().UpdateAsync(cosmetic);
			_unitOfWork.CommitAsync();
			return Task.FromResult(cosmetic);
		}
	}
}
