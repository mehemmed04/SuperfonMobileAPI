using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonWorks.Data;
using System.Collections;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetAllSales()
        {
            if (_memoryCache.TryGetValue(CacheKey, out IEnumerable<TigerSales> salesData))
            {
                return Ok(salesData);
            }

            return BadRequest("There is not any sale");
        }
    }
}
