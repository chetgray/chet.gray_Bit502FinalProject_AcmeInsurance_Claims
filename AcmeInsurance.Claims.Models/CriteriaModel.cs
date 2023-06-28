namespace AcmeInsurance.Claims.Models
{
    public class CriteriaModel : ICriteriaModel
    {
        public int Id { get; set; }

        public decimal DenialMinimumAmount { get; set; }
        public bool RequiresProviderIsInNetwork { get; set; }
        public bool RequiresProviderIsPreferred { get; set; }
        public bool RequiresClaimHasPreApproval { get; set; }
    }
}
