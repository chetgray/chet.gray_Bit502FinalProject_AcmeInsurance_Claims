using System.ComponentModel.DataAnnotations;

namespace AcmeInsurance.Claims.Web.CriteriaManager.ViewModels
{
    public interface ICriteriaDetailsViewModel
    {
        [Display(Name = "ID")]
        int Id { get; set; }

        [Required]
        [Display(Name = "Minimum amount to deny")]
        decimal DenialMinimumAmount { get; set; }

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
