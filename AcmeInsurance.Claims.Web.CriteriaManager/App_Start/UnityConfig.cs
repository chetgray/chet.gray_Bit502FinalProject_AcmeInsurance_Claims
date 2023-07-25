using System;

using AcmeInsurance.Claims.Business;
using AcmeInsurance.Claims.Models;
using AcmeInsurance.Claims.Web.CriteriaManager.ViewModels;

using Unity;

namespace AcmeInsurance.Claims.Web.CriteriaManager
{
    internal static class UnityConfig
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
            container.RegisterType<ICriteriaCreateViewModel, CriteriaCreateViewModel>();
            container.RegisterType<ICriteriaDetailsViewModel, CriteriaDetailsViewModel>();
            container.RegisterType<ICriteriaModel, CriteriaModel>();

            container.RegisterSingleton<ICriteriaBl, CriteriaBl>();
        }
    }
}
