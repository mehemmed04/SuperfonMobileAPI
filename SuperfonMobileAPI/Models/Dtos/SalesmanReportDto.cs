namespace SuperfonMobileAPI.Models.Dtos
{
    public class SalesmanReportDto
    {
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double PlannedSale { get; set; }
        public double ActualSale { get; set; }
        public double PercentageValue { get; set; }
        public double PlannedSaleOther { get; set; }
        public double ActualSaleOther { get; set; }
        public double PercentageValueOther { get; set; }
    }
}