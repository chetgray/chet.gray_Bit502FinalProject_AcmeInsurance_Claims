using System.ComponentModel.DataAnnotations;
using System.Web.Http;

using AcmeInsurance.Claims.Business;
using AcmeInsurance.Claims.Models;

using Unity;

namespace AcmeInsurance.Claims.WebServices.Api.Controllers
{
    public class ClaimsController : ApiController
    {
        private readonly IClaimBl _bl = UnityConfig.Container.Resolve<IClaimBl>();

        // POST: api/Claims
        [HttpPost]
        public int Post(PostRequest request)
        {
            IClaimModel claim = ConvertToModel(request);
            IClaimModel addedClaim = _bl.Add(claim);

            return addedClaim.Id;
        }

        private IClaimModel ConvertToModel(PostRequest request)
        {
            IClaimModel claim = UnityConfig.Container.Resolve<IClaimModel>();

            claim.Amount = request.Amount;
            claim.HasPreApproval = request.HasPreApproval;
            claim.PatientName = request.PatientName;
            if (request.ProviderId.HasValue)
            {
                claim.Provider =
                    _bl.GetProviderById(request.ProviderId.Value)
                    ?? throw new ValidationException("Provider ID not found.");
            }
            else if (!string.IsNullOrEmpty(request.ProviderCode))
            {
                claim.Provider =
                    _bl.GetProviderByCode(request.ProviderCode)
                    ?? throw new ValidationException("Provider Code not found.");
            }
            else
            {
                throw new ValidationException("ProviderId or ProviderCode must be specified.");
            }

            return claim;
        }

        public class PostRequest
        {
            [Required]
            public decimal Amount { get; set; }

            [Required]
            public bool HasPreApproval { get; set; }

            [Required]
            public string PatientName { get; set; }
            public string ProviderCode { get; set; }
            public int? ProviderId { get; set; }
        }
    }
}
