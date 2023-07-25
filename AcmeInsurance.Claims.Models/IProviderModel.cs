namespace AcmeInsurance.Claims.Models
{
    public interface IProviderModel
    {
        int Id { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        bool IsInNetwork { get; set; }
        bool IsPreferred { get; set; }
    }
}
