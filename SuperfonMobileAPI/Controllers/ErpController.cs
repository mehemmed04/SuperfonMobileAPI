using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Shared;
using SuperfonWorks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SuperfonWorks.Data.Entities;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ErpController : ControllerBase
    {
        TigerDataService tigerData = null;
        AppDataService appDataService = null;
        SuperfonWorksContext context = null;
        public ErpController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            context = _context;
        }
        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("ok, works");
        }

        [HttpGet("expense-cards")]
        public async Task<IEnumerable<ServiceCard>> GetServiceCards()
        {
            return await tigerData.GetServiceCards();
        }

        [HttpGet("banks")]
        public async Task<IEnumerable<TigerBank>> GetBanks()
        {
            var accs = await tigerData.GetBankAccounts();
            var codes = await context.UserBankAccountPermissions.Where(x => x.UserId == userId).Select(t => t.BankAccountCode).ToListAsync();
            var codesFiltered = accs.Where(x => codes.Contains(x.AccountCode));
            var result = from m in codesFiltered select new TigerBank { BankCode = m.AccountCode, BankName = m.AccountName, BranchName = string.Empty };
            return result;
        }

        [HttpGet("safeboxes")]
        public async Task<IEnumerable<TigerSafebox>> GetSafeboxes()
        {
            return await tigerData.GetSafeboxes();
        }
        [HttpGet("card/search")]
        public async Task<IEnumerable<TigerCard>> Getcard( [FromQuery] string expression)
        {
            return await tigerData.GetCards(expression);
        }

        [HttpGet("product/search")]
        public async Task<TigerProduct> GetProduct([FromQuery] string barcode, [FromQuery] bool branchDetails)
        {
            if (!branchDetails)
            {
                return await tigerData.GetProduct(barcode);
            }
            else
            {
                var userBranches = await context.UserBranchPermissions.Where(x => x.UserId == userId).ToListAsync();
                return await tigerData.GetProductIncludingDetails(barcode,userBranches.Select(x=>x.BranchNumber).ToArray());
            }
        }

        [HttpGet("stfiche/search")]
        public async Task<IEnumerable<TigerSTFiche>> GetStockFiches([FromQuery] TigerStockFicheFilter filter)
        {
            return await tigerData.GetStockFiches(filter);
        }

        [HttpPut("stfiche/control/{id}")]
        public async Task<IActionResult> DoControlStockFiche(int id)
        {
            var user = await context.Users.FindAsync(userId);
            await tigerData.ControlStFiche(id,user.DisplayName);
            return Ok();
        }
        [HttpGet("warehouses")]
        public async Task<IEnumerable<TigerWarehouse>> GetWarehouses()
        {
            return await tigerData.GetWarehouses();
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost("product/verification/{productCode}")]
        public async Task<IActionResult> AddProductVerification(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode)) return BadRequest();
            var existing = await context.ProductVerifications.FirstOrDefaultAsync(x=>x.ProductCode.Equals(productCode));
            if (existing != null) return StatusCode(409, "Məhsul mövcuddur");
            User user = await context.Users.FindAsync(userId);
            ProductVerification verification = new()
            {
                ProductCode = productCode,
                CreatedDate = DateTime.Now,
                CreatedBy = user.DisplayName
            };
            context.ProductVerifications.Add(verification);
            await context.SaveChangesAsync();
            return Ok();
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut("product/verification/{productCode}")]
        public async Task<IActionResult> DoProductVerification(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode)) return BadRequest();
            var existing = await context.ProductVerifications.FirstOrDefaultAsync(x=>x.ProductCode.Equals(productCode));
            if (existing == null) return StatusCode(404, "Məhsul tapılmadı");
            User user = await context.Users.FindAsync(userId);
            existing.VerifiedDate = DateTime.Now;
            existing.VerifiedBy = user.DisplayName;
            existing.IsVerified = true;
            existing.ModifiedBy = user.DisplayName;
            existing.ModifiedDate = DateTime.Now;
            await context.SaveChangesAsync();
            return Ok();
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("product/verification/{productCode}")]
        public async Task<IActionResult> UndoProductVerification(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode)) return BadRequest();
            var existing = await context.ProductVerifications.FirstOrDefaultAsync(x => x.ProductCode.Equals(productCode));
            if (existing == null) return StatusCode(404, "Məhsul tapılmadı");
            User user = await context.Users.FindAsync(userId);
            existing.VerifiedDate = null;
            existing.VerifiedBy = null;
            existing.IsVerified = false;
            existing.ModifiedBy = user.DisplayName;
            existing.ModifiedDate = DateTime.Now;
            await context.SaveChangesAsync();
            return Ok();
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpGet("product/verification/{productCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductVerification(string productCode)
        {
            if (Request.Headers["x-api-key"] != "s!?LiF>k5o~#5gGj0h~d") return Unauthorized();
            if (string.IsNullOrWhiteSpace(productCode)) return BadRequest();
            var existing = await context.ProductVerifications.FirstOrDefaultAsync(x => x.ProductCode.Equals(productCode));
            if (existing == null) return StatusCode(404, "Məhsul tapılmadı");
            var res = Newtonsoft.Json.JsonConvert.SerializeObject(existing);
            return Ok(res);
        }

        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //[HttpPost("product/verification-status/{productCode}")]
        //public async Task<IActionResult> ActivateProductVerification(string productCode)
        //{
        //    return await ProductVerficationChangeStatus(productCode,true);
        //}


        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //[HttpDelete("product/verification-status/{productCode}")]
        //public async Task<IActionResult> DeActivateProductVerification(string productCode)
        //{
        //    return await ProductVerficationChangeStatus(productCode, false);
        //}

        //private async Task<IActionResult> ProductVerficationChangeStatus(string productCode, bool isActive)
        //{
        //    if (string.IsNullOrWhiteSpace(productCode)) return BadRequest();
        //    var existing = await context.ProductVerifications.FirstOrDefaultAsync(x => x.ProductCode.Equals(productCode));
        //    if (existing == null) return StatusCode(404, "Məhsul tapılmadı");
        //    User user = await context.Users.FindAsync(userId);
        //    existing.IsActive = isActive;
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}

        [Authorize(AuthenticationSchemes = Constants.BasicAuthenticationScheme)]
        [HttpGet("stock-state")]
        public async Task<IEnumerable<ProductStockStateDto>> GetStockState()
        {
            var products = await tigerData.GetProductStockByBranches();
            IEnumerable<ProductStockStateDto> result = products.Select(p => new ProductStockStateDto()
            {
                sku = p.Sku,
                hasStock = (p.SpeCode == "JOYROOM" || p.SpeCode == "EUROACS") ? (p.Price > 4.9) : (p.Price > 14.9),
                price = p.Price,
                skuName = p.SkuName
            });
            return result;
        }
    }
}
