using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SalesController : ControllerBase
    {
        SalesBackgroundService _salesBackgroundService = null;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "SalesData";

        public SalesController(SalesBackgroundService salesBackgroundService, IMemoryCache memoryCache)
        {
            _salesBackgroundService = salesBackgroundService;
            _memoryCache = memoryCache;
        }

        [HttpGet("GetSales")]
        public async Task<IActionResult> GetAllSales(int month)
        {
            if (_memoryCache.TryGetValue(CacheKey, out IEnumerable<TigerSales> salesData))
            {
                salesData = salesData.Where(s => s.NumberOfMonth == month).ToList();
                //return Ok(salesData);
                var response = new List<SaleInfo>();
                foreach (var sale in salesData)
                {
                    bool isExists = false;
                    foreach (var r in response)
                    {
                        if (sale.NR == r.NR)
                        {
                            r.Details.Add(new SaleDetail()
                            {
                                Goal = sale.Goal,
                                PlanType = sale.PlanType,
                                SaleAmount = sale.SaleAmount,
                                Result = sale.Result
                            });
                            isExists = true;
                        }
                        else isExists = false;
                    }

                    if (!isExists)
                        response.Add(new SaleInfo()
                        {
                            NR = sale.NR,
                            SaleLocation = sale.SaleLocation,
                            Details = new List<SaleDetail>()
                            {
                                new SaleDetail()
                                {
                                    Goal = sale.Goal,
                                    PlanType = sale.PlanType,
                                    SaleAmount = sale.SaleAmount,
                                    Result = sale.Result
                                }
                            }
                    });
                }
                
                return Ok(response);
            }

            return BadRequest("There is not any sale");
        }
        public class SaleDetail
        {
            public string PlanType { get; set; }
            public float SaleAmount { get; set; }
            public decimal Goal { get; set; }
            public string Result { get; set; }
        }

        public class SaleInfo
        {
            public int NR { get; set; }
            public string SaleLocation { get; set; }

            public List<SaleDetail> Details { get; set; } = new List<SaleDetail>();
        }

    }
}
