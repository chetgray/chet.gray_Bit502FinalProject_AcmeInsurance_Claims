namespace AcmeInsurance.Claims.Data.Objects
{
    public interface IClaimDto
    {
        int Id { get; set; }
        string PatientName { get; set; }
        int ProviderId { get; set; }
        decimal Amount { get; set; }
        bool HasPreApproval { get; set; }
        int ClaimStatusId { get; set; }
    }
}
