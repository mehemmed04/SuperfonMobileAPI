using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonWorks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        readonly TigerDataService tigerDataService;
        readonly SuperfonWorksContext context = null;
        public ProductsController(TigerDataService tigerDataService, SuperfonWorksContext context)
        {
            this.tigerDataService = tigerDataService;
            this.context = context;
        }
        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var branchNumbers = (await context.UserBranchPermissions.Where(x => x.UserId == userId).ToListAsync()).Select(x => x.BranchNumber);
            var branches = (await tigerDataService.GetBranches()).Where(x => branchNumbers.Contains(x.BranchNumber));
            ViewBag.branches = branches;
            return View( new TigerProductListDTO { Products = new List<TigerProduct>() });
        }

        [HttpPost]
        public async Task<IActionResult> Index(TigerProductListDTO dto)
        {
            var branchNumbers = (await context.UserBranchPermissions.Where(x => x.UserId == userId).ToListAsync()).Select(x => x.BranchNumber);
            var branches = (await tigerDataService.GetBranches()).Where(x => branchNumbers.Contains(x.BranchNumber));
            ViewBag.branches = branches;
            dto.Products = (await tigerDataService.SearchProducts(dto.Text, 1, 100)).Products;
            return View(dto);
        }

        [HttpGet]
        public async Task<double> Price(string barcode, int branch)
        {
            var details = await tigerDataService.GetProductIncludingDetails(barcode, new int[] { branch });
            return Convert.ToDouble(details.ProductBranchDetails.FirstOrDefault(x=>x.BranchNumber == branch)?.Price);
        }

    }
}
