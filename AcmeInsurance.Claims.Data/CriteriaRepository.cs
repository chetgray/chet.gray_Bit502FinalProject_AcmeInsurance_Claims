using System.Collections.Generic;

using AcmeInsurance.Claims.Data.DataAccess;
using AcmeInsurance.Claims.Data.Objects;

using Unity;

namespace AcmeInsurance.Claims.Data
{
    /// <inheritdoc cref="ICriteriaRepository" />
    public class CriteriaRepository : ICriteriaRepository
    {
        [Dependency]
        public IDal Dal { get; set; }

        public ICriteriaDto GetById(int id)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "[spA_Criteria_SelectById]",
                new Dictionary<string, object> { { "@id", id } }
            );
            ICriteriaDto dto = UnityConfig.Container.Resolve<CriteriaDto>();
            dto.Id = (int)record[0];
            dto.DenialMinimumAmount = (decimal)record[1];
            dto.RequiresProviderIsInNetwork = (bool)record[2];
            dto.RequiresProviderIsPreferred = (bool)record[3];
            dto.RequiresClaimHasPreApproval = (bool)record[4];

            return dto;
        }

        public IList<ICriteriaDto> ListAll()
        {
            IEnumerable<object[]> records = Dal.GetRecordListFromStoredProcedure(
                "spA_Criteria_SelectAll",
                new Dictionary<string, object>()
            );
            IList<ICriteriaDto> dtos = new List<ICriteriaDto>();
            foreach (object[] record in records)
            {
                ICriteriaDto dto = UnityConfig.Container.Resolve<ICriteriaDto>();
                dto.Id = (int)record[0];
                dto.DenialMinimumAmount = (decimal)record[1];
                dto.RequiresProviderIsInNetwork = (bool)record[2];
                dto.RequiresProviderIsPreferred = (bool)record[3];
                dto.RequiresClaimHasPreApproval = (bool)record[4];
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
