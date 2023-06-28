using System.Collections.Generic;
using System.Web.Mvc;

using AcmeInsurance.Claims.Business;
using AcmeInsurance.Claims.Models;
using AcmeInsurance.Claims.Web.CriteriaManager.ViewModels;

using Unity;

namespace AcmeInsurance.Claims.Web.CriteriaManager.Controllers
{
    public class CriteriaController : Controller
    {
        // GET: Criteria
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Criteria/List
        public ActionResult List()
        {
            ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
            IEnumerable<ICriteriaModel> criteriaList = bl.ListAll();

            IEnumerable<ICriteriaDetailsViewModel> criteriaDetailsList =
                ConvertToDetailsViewModelList(criteriaList);

            ViewBag.Title = "All Criteria";

            return View(criteriaDetailsList);
        }

        private ICriteriaDetailsViewModel ConvertToDetailsViewModel(ICriteriaModel model)
        {
            ICriteriaDetailsViewModel viewModel =
                UnityConfig.Container.Resolve<ICriteriaDetailsViewModel>();
            viewModel.Id = model.Id;
            viewModel.DenialMinimumAmount = model.DenialMinimumAmount;
            viewModel.RequiresProviderIsInNetwork = model.RequiresProviderIsInNetwork;
            viewModel.RequiresProviderIsPreferred = model.RequiresProviderIsPreferred;
            viewModel.RequiresClaimHasPreApproval = model.RequiresClaimHasPreApproval;

            return viewModel;
        }

        private IList<ICriteriaDetailsViewModel> ConvertToDetailsViewModelList(
            IEnumerable<ICriteriaModel> models
        )
        {
            IList<ICriteriaDetailsViewModel> viewModels = new List<ICriteriaDetailsViewModel>();
            foreach (ICriteriaModel model in models)
            {
                viewModels.Add(ConvertToDetailsViewModel(model));
            }

            return viewModels;
        }
    }
}
