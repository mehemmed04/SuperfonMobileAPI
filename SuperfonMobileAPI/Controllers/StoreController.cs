using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QRCoder;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Shared;
using SuperfonWorks.Data;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Data.Queries;
using SuperfonWorks.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StoreController : ControllerBase
    {
        TigerDataService tigerData = null;
        SuperfonWorksContext sfContext = null;
        public StoreController(TigerDataService _tigerData, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            sfContext = _context;
        }

        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpGet("waiting-transfers")]
        public async Task<IEnumerable<TigerSTFiche>> GetUncompleteTransferFiches()
        {
            var whs = await (from p in sfContext.UserWarehousePermissions where p.UserId == userId && p.WarehousePermissionTypeId == 1 select p.WarehouseNumber).ToArrayAsync();
            var data = await tigerData.GetUncompleteTransferFiches(whs);
            return data;
        } 
        
        [HttpGet("quick-transfers")]
        public async Task<IEnumerable<TigerSTFiche>> GetQuickTransferFiches()
        {
            var whs = await (from p in sfContext.UserWarehousePermissions where p.UserId == userId && p.WarehousePermissionTypeId == 1 select p.WarehouseNumber).ToArrayAsync();
            var data = await tigerData.GetQuickTransferFiches(whs);
            return data;
        }


        [HttpPut("quick-transfer-approve/{stficheId}")]
        public async Task<IActionResult> ApproveQuickTransferFiche(int stficheId)
        {
            await tigerData.ApproveQuickTransferFiche(stficheId);
            return Ok();
        }

        [HttpGet("transfer-lines/{stficheId}")]
        public async Task<IEnumerable> GetTransferFicheLines(int stficheId, [FromQuery] string sortBy = "line")
        {
            var data = await tigerData.GeTransferFicheLines(stficheId);
            if (sortBy == "name")
                data = data.OrderBy(x => x.ItemName);
            return data;
        }

        [HttpPost("transfer-complete/{stficheId}")]
        public async Task<IActionResult> CompleteTransfer(int stficheId, [FromBody] CompleteTransferDTO dto)
        {
            //if(stficheId == 0) stficheId = dto.StficheId;
            var user = await sfContext.Users.FindAsync(userId);
            var persData = await tigerData.GetEFlowPersonnel(user.UserPID);
            if (persData == null) return BadRequest("EFlow personel bağlantısı tapılmadı");
            await tigerData.insertTransferAccResult(persData.NO,persData.PERSONEL_ADI, stficheId, dto);
            return Ok();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferDTO dto)
        {
            //var user = await sfContext.Users.FindAsync(userId);
            //var persData = await tigerData.GetEFlowPersonnel(user.UserPID);
            //if (persData == null) return BadRequest("EFlow personel bağlantısı tapılmadı");
            //await tigerData.insertTransferAccResult(persData.NO, persData.PERSONEL_ADI, stficheId, dto);
            return Ok();
        }


        //[AllowAnonymous]
        [HttpGet("report/{year:int}/{month:int}")]
        public async Task<IEnumerable<BranchPlanReportDto>> GetPlanReport(int year, int month)
        {
            int userId = this.GetUserId();
            List<int> branchNumbers = (await sfContext.UserBranchPermissions
                     .Where(x => x.UserId == userId).Select(x=>x.BranchNumber).ToListAsync());

            var list = await tigerData.GetBranchPlanReport(year, month);
            var slsCateg = await tigerData.GetBranchCategorySalesCustom(year, month);
            foreach (var item in list)
            {
                var ctgs = slsCateg.Where(x => x.BRANCH == item.BranchNumber);
                item.CategorySalesTotals = ctgs.Select(x => new CategorySalesTotalDto { CategoryName = x.KATEGORI_BP, SalesTotal = Math.Round(x.Total,2) }).ToList();
            }
            return list.Where(x=> branchNumbers.Contains(x.BranchNumber));
        }

        [HttpGet("report/details/{year:int}/{month:int}/{branchNumber:int}")]
        public async Task<IEnumerable<dynamic>> GetSalesmanCategorySalesByBranch(int year, int month, int branchNumber)
        {
            try
            {
                var list = await tigerData.GetSalesmanCategorySalesByBranch(year, month, branchNumber);
                var grouppedBySlsman = list.ToLookup(x => (x.SalesmanCode, x.SalesmanName));
                return grouppedBySlsman.Select(x => new { x.Key.SalesmanCode, x.Key.SalesmanName, Total = x.Select(f => f.Total).Cast<double>().Sum(), Details = x.Select(t => new { CategoryName = TigerDataService.GetCategoryName(t.KATEGORI), t.Quantity, t.Total }) });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
