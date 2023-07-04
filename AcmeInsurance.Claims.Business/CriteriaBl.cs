using System.Collections.Generic;

using AcmeInsurance.Claims.Data;
using AcmeInsurance.Claims.Data.Objects;
using AcmeInsurance.Claims.Models;

using Unity;

namespace AcmeInsurance.Claims.Business
{
    /// <inheritdoc cref="ICriteriaBl" />
    public class CriteriaBl : ICriteriaBl
    {
        [Dependency]
        public ICriteriaRepository Repository { get; set; }

        public ICriteriaModel Add(ICriteriaModel model)
        {
            ICriteriaDto dto = ConvertToDto(model);
            ICriteriaDto addedDto = Repository.Add(dto);
            ICriteriaModel addedModel = ConvertToModel(addedDto);

            return addedModel;
        }

        public ICriteriaModel GetById(int id)
        {
            ICriteriaDto dto = Repository.GetById(id);
            if (dto is null)
            {
                return null;
            }

            ICriteriaModel model = ConvertToModel(dto);

            return model;
        }

        public IList<ICriteriaModel> ListAll()
        {
            IEnumerable<ICriteriaDto> dtos = Repository.ListAll();
            IList<ICriteriaModel> models = ConvertToModelList(dtos);

            return models;
        }

        public bool RemoveById(int id)
        {
            bool wasRemoved = Repository.RemoveById(id);

            return wasRemoved;
        }

        private static ICriteriaModel ConvertToModel(ICriteriaDto dto)
        {
            ICriteriaModel model = UnityConfig.Container.Resolve<ICriteriaModel>();
            model.Id = dto.Id;
            model.DenialMinimumAmount = dto.DenialMinimumAmount;
            model.RequiresProviderIsInNetwork = dto.RequiresProviderIsInNetwork;
            model.RequiresProviderIsPreferred = dto.RequiresProviderIsPreferred;
            model.RequiresClaimHasPreApproval = dto.RequiresClaimHasPreApproval;

            return model;
        }

        private ICriteriaDto ConvertToDto(ICriteriaModel criteria)
        {
            ICriteriaDto dto = UnityConfig.Container.Resolve<ICriteriaDto>();
            dto.Id = criteria.Id;
            dto.DenialMinimumAmount = criteria.DenialMinimumAmount;
            dto.RequiresProviderIsInNetwork = criteria.RequiresProviderIsInNetwork;
            dto.RequiresProviderIsPreferred = criteria.RequiresProviderIsPreferred;
            dto.RequiresClaimHasPreApproval = criteria.RequiresClaimHasPreApproval;

            return dto;
        }

        private IList<ICriteriaModel> ConvertToModelList(IEnumerable<ICriteriaDto> dtos)
        {
            IList<ICriteriaModel> models = new List<ICriteriaModel>();
            foreach (ICriteriaDto dto in dtos)
            {
                models.Add(ConvertToModel(dto));
            }

            return models;
        }
    }
}
