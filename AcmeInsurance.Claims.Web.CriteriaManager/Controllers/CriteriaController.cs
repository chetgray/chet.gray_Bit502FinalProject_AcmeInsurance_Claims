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
        // GET: Criteria/Create
        public ActionResult Create()
        {
            ICriteriaCreateViewModel criteriaCreate =
                UnityConfig.Container.Resolve<ICriteriaCreateViewModel>();

            return View(criteriaCreate);
        }

        // POST: Criteria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            ICriteriaCreateViewModel criteriaCreate =
                UnityConfig.Container.Resolve<ICriteriaCreateViewModel>();
            UpdateModel(criteriaCreate, form);
            ICriteriaModel criteria = ConvertToModel(criteriaCreate);

            try
            {
                ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
                ICriteriaModel createdCriteria = bl.Add(criteria);

                return RedirectToAction("Details", new { id = createdCriteria.Id });
            }
            catch
            {
                return View(criteriaCreate);
            }
        }

        // GET: Criteria/Delete/{id}
        public ActionResult Delete(int id)
        {
            ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
            ICriteriaModel criteria = bl.GetById(id);
            ICriteriaDetailsViewModel criteriaDetails = ConvertToDetailsViewModel(criteria);

            return View(criteriaDetails);
        }

        // POST: Criteria/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            if (form["action"] != "delete")
            {
                return RedirectToAction("Details", new { id });
            }

            ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
            if (!bl.RemoveById(id))
            {
                return RedirectToAction("Delete", new { id });
            }

            return RedirectToAction("List");
        }

        // GET: Criteria/Details/{id}
        public ActionResult Details(int id)
        {
            ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
            ICriteriaModel criteria = bl.GetById(id);
            ICriteriaDetailsViewModel criteriaDetails = ConvertToDetailsViewModel(criteria);

            return View(criteriaDetails);
        }

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

        private ICriteriaModel ConvertToModel(ICriteriaCreateViewModel viewModel)
        {
            ICriteriaModel model = UnityConfig.Container.Resolve<ICriteriaModel>();
            model.DenialMinimumAmount = (decimal)viewModel.DenialMinimumAmount;
            model.RequiresProviderIsInNetwork = viewModel.RequiresProviderIsInNetwork;
            model.RequiresProviderIsPreferred = viewModel.RequiresProviderIsPreferred;
            model.RequiresClaimHasPreApproval = viewModel.RequiresClaimHasPreApproval;

            return model;
        }
    }
}
