using System.Collections.Generic;

using AcmeInsurance.Claims.Data;
using AcmeInsurance.Claims.Data.Objects;
using AcmeInsurance.Claims.Models;

using Unity;

namespace AcmeInsurance.Claims.Business
{
    public class ClaimBl : IClaimBl
    {
        [Dependency]
        public IClaimRepository Repository { get; set; }

        public IClaimModel Add(IClaimModel model)
        {
            IClaimDto dto = ConvertToDto(model);
            IClaimDto addedDto = Repository.Add(dto);
            IClaimModel addedModel = ConvertToModel(addedDto);

            return addedModel;
        }

        public IProviderModel GetProviderByCode(string code)
        {
            IProviderDto dto = Repository.GetProviderByCode(code);
            IProviderModel model = ConvertProviderToModel(dto);

            return model;
        }

        public IProviderModel GetProviderById(int id)
        {
            IProviderDto dto = Repository.GetProviderById(id);
            IProviderModel model = ConvertProviderToModel(dto);

            return model;
        }

        public IList<IClaimModel> ListByClaimStatus(ClaimStatus claimStatus)
        {
            IEnumerable<IClaimDto> dtos = Repository.ListByClaimStatus((int)claimStatus);
            IList<IClaimModel> models = ConvertToModelList(dtos);

            return models;
        }

        private IProviderModel ConvertProviderToModel(IProviderDto dto)
        {
            if (dto is null)
            {
                return null;
            }

            IProviderModel model = UnityConfig.Container.Resolve<IProviderModel>();
            model.Id = dto.Id;
            model.Code = dto.Code;
            model.Name = dto.Name;
            model.IsInNetwork = dto.IsInNetwork;
            model.IsPreferred = dto.IsPreferred;

            return model;
        }

        private IClaimDto ConvertToDto(IClaimModel model)
        {
            if (model is null)
            {
                return null;
            }

            IClaimDto dto = UnityConfig.Container.Resolve<IClaimDto>();
            dto.Id = model.Id;
            dto.PatientName = model.PatientName;
            dto.ProviderId = model.Provider.Id;
            dto.Amount = model.Amount;
            dto.HasPreApproval = model.HasPreApproval;
            dto.ClaimStatusId = (int)model.ClaimStatus;

            return dto;
        }

        private IClaimModel ConvertToModel(IClaimDto dto)
        {
            if (dto is null)
            {
                return null;
            }

            IClaimModel model = UnityConfig.Container.Resolve<IClaimModel>();
            model.Id = dto.Id;
            model.PatientName = dto.PatientName;
            model.Provider = GetProviderById(dto.ProviderId);
            model.Amount = dto.Amount;
            model.HasPreApproval = dto.HasPreApproval;
            model.ClaimStatus = (ClaimStatus)dto.ClaimStatusId;

            return model;
        }

        private IList<IClaimModel> ConvertToModelList(IEnumerable<IClaimDto> dtos)
        {
            IList<IClaimModel> models = new List<IClaimModel>();
            foreach (IClaimDto dto in dtos)
            {
                IClaimModel model = ConvertToModel(dto);
                models.Add(model);
            }

            return models;
        }
    }
}
