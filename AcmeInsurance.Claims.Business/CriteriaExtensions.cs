using AcmeInsurance.Claims.Models;

namespace AcmeInsurance.Claims.Business
{
    public static class CriteriaExtensions
    {
        public static bool AreMetBy(this ICriteriaModel criteria, IClaimModel claim)
        {
            bool areCriteriaMet =
                (claim.Amount < criteria.DenialMinimumAmount)
                && (claim.Provider.IsInNetwork == criteria.RequiresProviderIsInNetwork)
                && (claim.Provider.IsPreferred == criteria.RequiresProviderIsPreferred)
                && (claim.HasPreApproval == criteria.RequiresClaimHasPreApproval);

            return areCriteriaMet;
        }
    }
}
