namespace AcmeInsurance.Claims.Data.Objects
{
    public class ProviderDto : IProviderDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsInNetwork { get; set; }
        public bool IsPreferred { get; set; }
    }
}
