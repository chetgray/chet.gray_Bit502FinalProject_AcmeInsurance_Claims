using System.Collections.Generic;

using AcmeInsurance.Claims.Data.DataAccess;
using AcmeInsurance.Claims.Data.Objects;

using Unity;

namespace AcmeInsurance.Claims.Data
{
    public class ClaimRepository : IClaimRepository
    {
        [Dependency]
        public IDal Dal { get; set; }

        public IClaimDto Add(IClaimDto dto)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "spA_Claim_Insert",
                new Dictionary<string, object>
                {
                    { "@patientName", dto.PatientName },
                    { "@providerId", dto.ProviderId },
                    { "@amount", dto.Amount },
                    { "@hasPreApproval", dto.HasPreApproval },
                    { "@claimStatusId", dto.ClaimStatusId },
                }
            );

            IClaimDto addedDto = ConvertToDto(record);

            return addedDto;
        }

        public IProviderDto GetProviderByCode(string code)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "spA_Provider_SelectByCode",
                new Dictionary<string, object> { { "@code", code } }
            );

            IProviderDto dto = ConvertProviderToDto(record);

            return dto;
        }

        public IProviderDto GetProviderById(int id)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "spA_Provider_SelectById",
                new Dictionary<string, object> { { "@id", id } }
            );

            IProviderDto dto = ConvertProviderToDto(record);

            return dto;
        }

        public IList<IClaimDto> ListByClaimStatus(int claimStatusId)
        {
            IEnumerable<object[]> records = Dal.GetRecordListFromStoredProcedure(
                "spA_Claim_SelectByClaimStatus",
                new Dictionary<string, object> { { "@claimStatusId", claimStatusId } }
            );
            IList<IClaimDto> dtos = new List<IClaimDto>();
            foreach (object[] record in records)
            {
                IClaimDto dto = ConvertToDto(record);
                dtos.Add(dto);
            }

            return dtos;
        }

        private IProviderDto ConvertProviderToDto(object[] record)
        {
            if (record is null)
            {
                return null;
            }

            IProviderDto dto = UnityConfig.Container.Resolve<IProviderDto>();
            dto.Id = (int)record[0];
            dto.Code = (string)record[1];
            dto.Name = (string)record[2];
            dto.IsInNetwork = (bool)record[3];
            dto.IsPreferred = (bool)record[4];

            return dto;
        }

        private IClaimDto ConvertToDto(object[] record)
        {
            if (record is null)
            {
                return null;
            }

            IClaimDto dto = UnityConfig.Container.Resolve<IClaimDto>();
            dto.Id = (int)record[0];
            dto.PatientName = (string)record[1];
            dto.ProviderId = (int)record[2];
            dto.Amount = (decimal)record[3];
            dto.HasPreApproval = (bool)record[4];
            dto.ClaimStatusId = (int)record[5];

            return dto;
        }
    }
}
