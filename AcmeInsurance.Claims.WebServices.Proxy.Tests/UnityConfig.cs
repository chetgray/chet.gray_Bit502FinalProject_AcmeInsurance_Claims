using System;

using AcmeInsurance.Claims.Models;

using Unity;

namespace AcmeInsurance.Claims.WebServices.Proxy.Tests
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> _container =
            new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = new UnityContainer();
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
        }
    }
}
