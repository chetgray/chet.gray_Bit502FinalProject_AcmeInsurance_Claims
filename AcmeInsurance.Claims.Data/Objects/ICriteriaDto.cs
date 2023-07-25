namespace AcmeInsurance.Claims.Data.Objects
{
    public interface ICriteriaDto
    {
        int Id { get; set; }

        decimal DenialMinimumAmount { get; set; }
        bool RequiresProviderIsInNetwork { get; set; }
        bool RequiresProviderIsPreferred { get; set; }
        bool RequiresClaimHasPreApproval { get; set; }
    }
}
