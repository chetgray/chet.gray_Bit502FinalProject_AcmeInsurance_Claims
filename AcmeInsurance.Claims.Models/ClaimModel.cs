using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AcmeInsurance.Claims.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ClaimStatus
    {
        [EnumMember(Value = "pending")]
        Pending = 1,

        [EnumMember(Value = "approved")]
        Approved = 2,

        [EnumMember(Value = "denied")]
        Denied = 3
    }

    public class ClaimModel : IClaimModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public IProviderModel Provider { get; set; }
        public decimal Amount { get; set; }
        public bool HasPreApproval { get; set; }
        public ClaimStatus ClaimStatus { get; set; }
    }
}
