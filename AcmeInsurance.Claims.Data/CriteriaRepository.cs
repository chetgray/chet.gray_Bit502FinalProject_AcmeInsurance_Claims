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

        public ICriteriaDto Add(ICriteriaDto dto)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "spA_Criteria_Insert",
                new Dictionary<string, object>
                {
                    { "@denialMinimumAmount", dto.DenialMinimumAmount },
                    { "@requiresProviderIsInNetwork", dto.RequiresProviderIsInNetwork },
                    { "@requiresProviderIsPreferred", dto.RequiresProviderIsPreferred },
                    { "@requiresClaimHasPreApproval", dto.RequiresClaimHasPreApproval },
                }
            );
            ICriteriaDto addedDto = ConvertToDto(record);

            return addedDto;
        }

        public ICriteriaDto GetById(int id)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "[spA_Criteria_SelectById]",
                new Dictionary<string, object> { { "@id", id } }
            );
            ICriteriaDto dto = ConvertToDto(record);

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
                ICriteriaDto dto = ConvertToDto(record);
                dtos.Add(dto);
            }

            return dtos;
        }

        private static ICriteriaDto ConvertToDto(object[] record)
        {
            ICriteriaDto addedDto = UnityConfig.Container.Resolve<CriteriaDto>();
            addedDto.Id = (int)record[0];
            addedDto.DenialMinimumAmount = (decimal)record[1];
            addedDto.RequiresProviderIsInNetwork = (bool)record[2];
            addedDto.RequiresProviderIsPreferred = (bool)record[3];
            addedDto.RequiresClaimHasPreApproval = (bool)record[4];

            return addedDto;
        }
    }
}
