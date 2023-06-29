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
