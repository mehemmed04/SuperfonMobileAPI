using System.Collections.Generic;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class BranchPlanReportDto
    {
        public short BranchNumber { get; set; }
        public string BranchName { get; set; }
        public double PlannedSale { get; set; }
        public double ActualSale { get; set; }
        public double PercentageValue { get; set; }
        public List<CategorySalesTotalDto> CategorySalesTotals { get; set; }
    }
}
