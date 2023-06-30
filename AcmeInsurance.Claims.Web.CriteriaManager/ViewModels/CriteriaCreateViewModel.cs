namespace AcmeInsurance.Claims.Web.CriteriaManager.ViewModels
{
    public class CriteriaCreateViewModel : ICriteriaCreateViewModel
    {
        public decimal? DenialMinimumAmount { get; set; } = null;
        public bool RequiresProviderIsInNetwork { get; set; } = true;
        public bool RequiresProviderIsPreferred { get; set; } = true;
        public bool RequiresClaimHasPreApproval { get; set; } = true;
    }
}
