using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        TigerDataService tigerData = null;
        public SalesController(TigerDataService _tigerData, AppDataService _appDataService)
        {
            tigerData = _tigerData;
        }

        [HttpGet("GetSales")]
        public async Task<IEnumerable<TigerSales>> GetAllSales()
        {
            return await tigerData.GetSales();
        }
    }
}
