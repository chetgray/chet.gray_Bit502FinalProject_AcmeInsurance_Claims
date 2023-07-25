using AcmeInsurance.Claims.Data.Objects;

namespace AcmeInsurance.Claims.Data
{
    public interface IClaimRepository
    {
        IClaimDto Add(IClaimDto dto);
        IProviderDto GetProviderByCode(string code);
        IProviderDto GetProviderById(int id);
    }
}
