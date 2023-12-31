﻿using System.Collections.Generic;

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

        public IList<ICriteriaModel> AddRange(IEnumerable<ICriteriaModel> models)
        {
            IEnumerable<ICriteriaDto> dtos = ConvertToDtoList(models);
            IEnumerable<ICriteriaDto> addedDtos = Repository.AddRange(dtos);
            IList<ICriteriaModel> addedModels = ConvertToModelList(addedDtos);

            return addedModels;
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

        public ICriteriaModel Update(ICriteriaModel model)
        {
            ICriteriaDto dto = ConvertToDto(model);
            dto = Repository.Update(dto);
            ICriteriaModel updatedModel = ConvertToModel(dto);

            return updatedModel;
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

        private IEnumerable<ICriteriaDto> ConvertToDtoList(IEnumerable<ICriteriaModel> models)
        {
            IList<ICriteriaDto> dtos = new List<ICriteriaDto>();
            foreach (ICriteriaModel model in models)
            {
                dtos.Add(ConvertToDto(model));
            }

            return dtos;
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
