using System;

using AcmeInsurance.Claims.Business;

using Unity;

namespace AcmeInsurance.Claims.Services.DeciderService
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> _container =
            new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container =
                    Business.UnityConfig.Container.CreateChildContainer();
                RegisterTypes(container);

                return container;
            });

        public static IUnityContainer Container
        {
            get => _container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IClaimBl, ClaimBl>();
            container.RegisterType<ICriteriaBl, CriteriaBl>();
        }
    }
}
