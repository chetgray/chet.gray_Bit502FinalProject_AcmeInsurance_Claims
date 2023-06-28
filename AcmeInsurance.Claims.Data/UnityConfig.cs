using System;

using AcmeInsurance.Claims.Data.Objects;

using Unity;

namespace AcmeInsurance.Claims.Data
{
    internal static class UnityConfig
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
            container.RegisterType<ICriteriaDto, CriteriaDto>();
        }
    }
}
