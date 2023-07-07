namespace AcmeInsurance.Claims.Models
{
    public interface IClaimModel
    {
        int Id { get; set; }
        string PatientName { get; set; }
        IProviderModel Provider { get; set; }
        decimal Amount { get; set; }
        bool HasPreApproval { get; set; }
        ClaimStatus ClaimStatus { get; set; }
    }
}
