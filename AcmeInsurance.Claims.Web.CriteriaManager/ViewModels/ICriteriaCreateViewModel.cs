using System.ComponentModel.DataAnnotations;

namespace AcmeInsurance.Claims.Web.CriteriaManager.ViewModels
{
    public interface ICriteriaCreateViewModel
    {
        [Required]
        [Display(Name = "Minimum amount to deny")]
        decimal? DenialMinimumAmount { get; set; }

        [Required]
        [Display(Name = "Require in-network provider")]
        bool RequiresProviderIsInNetwork { get; set; }

        [Required]
        [Display(Name = "Require preferred provider")]
        bool RequiresProviderIsPreferred { get; set; }

        [Required]
        [Display(Name = "Require pre-approval")]
        bool RequiresClaimHasPreApproval { get; set; }
    }
}
