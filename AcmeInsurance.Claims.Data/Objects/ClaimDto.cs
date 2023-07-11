namespace AcmeInsurance.Claims.Data.Objects
{
    public class ClaimDto : IClaimDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int ProviderId { get; set; }
        public decimal Amount { get; set; }
        public bool HasPreApproval { get; set; }
        public int ClaimStatusId { get; set; }
    }
}
