using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class CompleteTransferDTO
    {
        public List<CompleteTransferModLineDTO> AddedLines { get; set; }
        public List<CompleteTransferModLineDTO> ModifiedLines { get; set; }
    }

    public class CompleteTransferModLineDTO
    {
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
    }

}
