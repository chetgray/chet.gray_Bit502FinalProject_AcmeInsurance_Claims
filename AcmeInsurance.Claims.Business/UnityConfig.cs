using System;

using AcmeInsurance.Claims.Data;
using AcmeInsurance.Claims.Models;

using Unity;

namespace AcmeInsurance.Claims.Business
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> _container =
            new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = Data.UnityConfig.Container.CreateChildContainer();
                RegisterTypes(container);

                return container;
            });

        public static IUnityContainer Container
        {
            get => _container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IProviderModel, ProviderModel>();
            container.RegisterType<IClaimModel, ClaimModel>();
            container.RegisterType<ICriteriaModel, CriteriaModel>();

            container.RegisterSingleton<IClaimRepository, ClaimRepository>();
            container.RegisterSingleton<ICriteriaRepository, CriteriaRepository>();
        }
    }
}
