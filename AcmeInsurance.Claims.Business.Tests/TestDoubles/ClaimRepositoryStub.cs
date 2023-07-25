using System.Collections.Generic;

using AcmeInsurance.Claims.Data;
using AcmeInsurance.Claims.Data.Objects;

namespace AcmeInsurance.Claims.Business.Tests.TestDoubles
{
    public class ClaimRepositoryStub : IClaimRepository
    {
        public IClaimDto ClaimDto { get; set; }
        public IList<IClaimDto> ClaimDtoList { get; set; }
        public IProviderDto ProviderDto { get; set; }

        public IClaimDto Add(IClaimDto dto)
        {
            return ClaimDto;
        }

        public IClaimDto GetById(int id)
        {
            return ClaimDto;
        }

        public IProviderDto GetProviderByCode(string code)
        {
            return ProviderDto;
        }

        public IProviderDto GetProviderById(int id)
        {
            return ProviderDto;
        }

        public IList<IClaimDto> ListByClaimStatus(int claimStatusId)
        {
            return ClaimDtoList;
        }

        public IClaimDto UpdateClaimStatus(int id, int claimStatusId)
        {
            return ClaimDto;
        }
    }
}
