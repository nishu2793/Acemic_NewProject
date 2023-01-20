namespace NewProject.API.Requests.Payment
{
    public class Payment_PercentageRequest
    {
        public int Id { get; set; }
        public double? Percentage { get; set; }
        public string? Name { get; set; }
        public string? AccountId { get; set; }
    }

}
