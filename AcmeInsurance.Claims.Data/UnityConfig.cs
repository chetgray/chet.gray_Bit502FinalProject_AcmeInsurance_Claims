using System;
using System.Configuration;

using AcmeInsurance.Claims.Data.DataAccess;
using AcmeInsurance.Claims.Data.Objects;

using Unity;
using Unity.Injection;

namespace AcmeInsurance.Claims.Data
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
            string connectionString = ConfigurationManager.ConnectionStrings[
                "AcmeInsurance.Claims.Database"
            ].ConnectionString;

            container.RegisterType<IProviderDto, ProviderDto>();
            container.RegisterType<IClaimDto, ClaimDto>();
            container.RegisterType<ICriteriaDto, CriteriaDto>();

            container.RegisterSingleton<IDal, Dal>(new InjectionConstructor(connectionString));
        }
    }
}
