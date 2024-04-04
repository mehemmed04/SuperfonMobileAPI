using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Shared;
using SuperfonWorks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalesmenController : ControllerBase
    {
        readonly TigerDataService _tigerData = null;
        readonly SuperfonWorksContext _sfContext = null;
        public SalesmenController(TigerDataService tigerData, SuperfonWorksContext context)
        {
            _tigerData = tigerData;
            _sfContext = context;
        }

        [HttpGet("report/{year:int}/{month:int}")]
        public async Task<IEnumerable<SalesmanReportDto>> GetReport(int year, int month)
        {
            int userId = this.GetUserId();
            string branchNumbers = (await _sfContext.UserBranchPermissions
                     .Where(x => x.UserId == userId).ToListAsync())
                        .Aggregate();
            
            return (await _tigerData.GetSalesmenReport(year, month, branchNumbers));
        }

        [HttpGet("report-detail/{year:int}/{month:int}/{salesmanCode}")]
        public async Task<SalesmanReportDetailDto> GetReportDetail(int year, int month, string salesmanCode)
        {
            int userId = this.GetUserId();
            string branchNumbers = (await _sfContext.UserBranchPermissions.Where(x => x.UserId == userId).ToListAsync()).Aggregate();

            var reportDetail = (await _tigerData.GetSalesmanReportDetail(year, month, salesmanCode, branchNumbers));
            dynamic firstRow = reportDetail.First();
            SalesmanReportDetailDto result = new()
            {
                Year = firstRow.Year,
                Month = firstRow.Month,
                SalesmanName = firstRow.SalesmanName
            };

            result.ToatlActualSale = reportDetail.Sum(s => (decimal)s.ActualSale);
            result.Detail = reportDetail.Select(s => new SalesmanSaleDetailDto() { ActualSale = s.ActualSale, BranchName = s.BranchName }).ToList();
            return result;
        }
    }
}
