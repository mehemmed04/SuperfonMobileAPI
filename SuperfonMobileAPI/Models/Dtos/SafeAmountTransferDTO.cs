using SuperfonMobileAPI.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class SafeAmountTransferDTO
    {
        [Range(minimum:1,maximum:2,ErrorMessage = Constants.TransferTypeNotFound)]
        public byte TransferType { get; set; }

        public string SourceSafeboxCode { get; set; }
        [Required]
        public string DestinationCode { get; set; }
        [Range(minimum : 0.1,maximum: 1000000, ErrorMessage = Constants.WrongAmountEntered)]
        public double Amount { get; set; }
        public string Note { get; set; }
        public string UserDisplayName { get; set; }
    }
}
