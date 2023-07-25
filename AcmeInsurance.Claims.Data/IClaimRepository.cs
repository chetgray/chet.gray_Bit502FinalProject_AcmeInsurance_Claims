using System.Collections.Generic;

using AcmeInsurance.Claims.Data.Objects;

namespace AcmeInsurance.Claims.Data
{
    public interface IClaimRepository
    {
        IClaimDto Add(IClaimDto dto);
        IClaimDto GetById(int id);
        IProviderDto GetProviderByCode(string code);
        IProviderDto GetProviderById(int id);
        IList<IClaimDto> ListByClaimStatus(int claimStatusId);
        IClaimDto UpdateClaimStatus(int id, int claimStatusId);
    }
}
