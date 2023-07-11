using AcmeInsurance.Claims.Models;

namespace AcmeInsurance.Claims.Business
{
    public interface IClaimBl
    {
        IClaimModel Add(IClaimModel model);
        IProviderModel GetProviderByCode(string code);
        IProviderModel GetProviderById(int id);
    }
}
