using System.Collections.Generic;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class CreateTransferDTO
    {
        public int SourceWarehouseNumber { get; set; }
        public int DestWarehouseNumber { get; set; }
        public List<CompleteTransferModLineDTO> Lines { get; set; }
    }
}
