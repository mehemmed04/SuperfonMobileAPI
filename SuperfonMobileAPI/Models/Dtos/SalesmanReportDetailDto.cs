using System.Collections.Generic;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class SalesmanReportDetailDto
    {
        public string SalesmanName { get; set; }
        public decimal ToatlActualSale { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<SalesmanSaleDetailDto> Detail { get; set; }
    }
}
