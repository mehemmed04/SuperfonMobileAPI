using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Entities
{
    public class TigerProduct
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public List<TigerProductBranchInfo> ProductBranchDetails { get; set; }   
    }

    public class TigerProductBranchInfo : TigerBranch
    {
        public double Price { get; set; }
        public double Stock { get; set; }
        public bool IsPermitted { get; set; }
    }
}
