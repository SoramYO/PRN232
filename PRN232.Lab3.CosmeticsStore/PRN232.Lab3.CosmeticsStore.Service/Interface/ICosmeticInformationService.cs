using PRN232.Lab3.CosmeticsStore.Repository.Entities;
using PRN232.Lab3.CosmeticsStore.Service.Payload.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Interface
{
    public interface ICosmeticInformationService
    {
		Task<List<CosmeticInformation>> GetAllCosmetics();
		Task<CosmeticInformation> GetById(string id);
		Task<CosmeticInformation> Add(CosmeticInformationRequest request);
		Task<CosmeticInformation> Update(CosmeticInformationRequest request);
		Task<CosmeticInformation> Delete(string id);
		Task<List<CosmeticCategory>> GetAllCategories();

	}
}
