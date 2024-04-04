using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperfonMobileAPI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Pages
{
    public class ProductPricePrintModel : PageModel
    {
        readonly TigerDataService tigerDataService;
        public ProductPricePrintModel(TigerDataService tigerDataService)
        {
            this.tigerDataService = tigerDataService;
        }
        public async Task OnGetAsync()
        {
            var product = await tigerDataService.GetProductIncludingDetails(Barcode, new int[] { Convert.ToInt32(BranchNumber) });
            Code = product.ProductCode;
            Name = product.ProductName;
            Price = product.ProductBranchDetails.FirstOrDefault(x => x.BranchNumber == Convert.ToInt32(BranchNumber))?.Price.ToString("N2") + " AZN";
        }

        [FromQuery(Name = "branch")]
        public string BranchNumber { get; set; }

        [FromQuery(Name = "barcode")]
        public string Barcode { get; set; } 

        public string Code { get; set; } 
        public string Name { get; set; }

        public string Price { get; set; } = "0.00";
    }
}
