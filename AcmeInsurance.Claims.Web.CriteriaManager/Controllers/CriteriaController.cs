using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AcmeInsurance.Claims.Business;
using AcmeInsurance.Claims.Models;
using AcmeInsurance.Claims.Web.CriteriaManager.ViewModels;

using Unity;

namespace AcmeInsurance.Claims.Web.CriteriaManager.Controllers
{
    public class CriteriaController : Controller
    {
        // GET: Criteria/BulkCreate
        public ActionResult BulkCreate()
        {
            return View();
        }

        // POST: Criteria/BulkCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BulkCreate(FormCollection form)
        {
            string[] lines = form["criteria"]
                .Trim()
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            IList<ICriteriaModel> criteriaList = new List<ICriteriaModel>();
            foreach (string line in lines)
            {
                string[] values = line.Split(',').Select(v => v.Trim()).ToArray();
                ICriteriaModel criteria = UnityConfig.Container.Resolve<ICriteriaModel>();
                criteria.DenialMinimumAmount = decimal.Parse(values[0].TrimStart('$'));
                try
                {
                    criteria.RequiresProviderIsInNetwork = bool.Parse(values[1]);
                }
                catch (FormatException)
                {
                    switch (values[1].ToLower())
                    {
                        case "out of network":
                            criteria.RequiresProviderIsInNetwork = false;
                            break;
                        case "in network":
                        default:
                            criteria.RequiresProviderIsInNetwork = true;
                            break;
                    }
                }

                criteria.RequiresProviderIsPreferred = bool.Parse(values[2]);
                criteria.RequiresClaimHasPreApproval = bool.Parse(values[3]);
                criteriaList.Add(criteria);
            }

            // add criteria
            ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
            bl.AddRange(criteriaList);

            return RedirectToAction("List");
        }

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

        // GET: Criteria/Edit/{id}
        public ActionResult Edit(int id)
        {
            ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
            ICriteriaModel criteria = bl.GetById(id);
            ICriteriaDetailsViewModel criteriaDetails = ConvertToDetailsViewModel(criteria);

            return View(criteriaDetails);
        }

        // POST: Criteria/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            ICriteriaDetailsViewModel criteriaDetails =
                UnityConfig.Container.Resolve<ICriteriaDetailsViewModel>();
            UpdateModel(criteriaDetails, form);
            ICriteriaModel criteria = ConvertToModel(criteriaDetails);

            try
            {
                ICriteriaBl bl = UnityConfig.Container.Resolve<ICriteriaBl>();
                ICriteriaModel updatedCriteria = bl.Update(criteria);

                return RedirectToAction("Details", new { id = updatedCriteria.Id });
            }
            catch
            {
                return View(criteriaDetails);
            }
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

        private ICriteriaModel ConvertToModel(ICriteriaDetailsViewModel criteriaDetails)
        {
            ICriteriaModel criteria = UnityConfig.Container.Resolve<ICriteriaModel>();
            criteria.Id = criteriaDetails.Id;
            criteria.DenialMinimumAmount = criteriaDetails.DenialMinimumAmount;
            criteria.RequiresProviderIsInNetwork = criteriaDetails.RequiresProviderIsInNetwork;
            criteria.RequiresProviderIsPreferred = criteriaDetails.RequiresProviderIsPreferred;
            criteria.RequiresClaimHasPreApproval = criteriaDetails.RequiresClaimHasPreApproval;

            return criteria;
        }

        private ICriteriaModel ConvertToModel(ICriteriaCreateViewModel criteriaCreate)
        {
            ICriteriaModel criteria = UnityConfig.Container.Resolve<ICriteriaModel>();
            criteria.DenialMinimumAmount = (decimal)criteriaCreate.DenialMinimumAmount;
            criteria.RequiresProviderIsInNetwork = criteriaCreate.RequiresProviderIsInNetwork;
            criteria.RequiresProviderIsPreferred = criteriaCreate.RequiresProviderIsPreferred;
            criteria.RequiresClaimHasPreApproval = criteriaCreate.RequiresClaimHasPreApproval;

            return criteria;
        }
    }
}
