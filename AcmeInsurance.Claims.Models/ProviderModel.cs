namespace AcmeInsurance.Claims.Models
{
    public class ProviderModel : IProviderModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsInNetwork { get; set; }
        public bool IsPreferred { get; set; }
    }
}
