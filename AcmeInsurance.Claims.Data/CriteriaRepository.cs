﻿using System.Collections.Generic;
using System.Data.SqlClient;

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

        public IList<ICriteriaDto> AddRange(IEnumerable<ICriteriaDto> dtos)
        {
            IList<ICriteriaDto> addedDtos = new List<ICriteriaDto>();
            foreach (ICriteriaDto dto in dtos)
            {
                ICriteriaDto addedDto = Add(dto);
                addedDtos.Add(addedDto);
            }

            return addedDtos;
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

        public bool RemoveById(int id)
        {
            try
            {
                Dal.ExecuteStoredProcedure(
                    "spA_Criteria_DeleteById",
                    new Dictionary<string, object> { { "@id", id } }
                );
            }
            catch (SqlException)
            {
                return false;
            }

            return true;
        }

        public ICriteriaDto Update(ICriteriaDto dto)
        {
            object[] record = Dal.GetRecordFromStoredProcedure(
                "spA_Criteria_UpdateById",
                new Dictionary<string, object>
                {
                    { "@id", dto.Id },
                    { "@denialMinimumAmount", dto.DenialMinimumAmount },
                    { "@requiresProviderIsInNetwork", dto.RequiresProviderIsInNetwork },
                    { "@requiresProviderIsPreferred", dto.RequiresProviderIsPreferred },
                    { "@requiresClaimHasPreApproval", dto.RequiresClaimHasPreApproval },
                }
            );
            ICriteriaDto updatedDto = ConvertToDto(record);

            return updatedDto;
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
