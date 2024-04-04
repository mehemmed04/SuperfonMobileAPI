using System;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class CashoutDTO
    {
        public int CardId { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
    }
}
