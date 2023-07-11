namespace AcmeInsurance.Claims.Data.Objects
{
    public interface IProviderDto
    {
        string Code { get; set; }
        int Id { get; set; }
        bool IsInNetwork { get; set; }
        bool IsPreferred { get; set; }
        string Name { get; set; }
    }
}
