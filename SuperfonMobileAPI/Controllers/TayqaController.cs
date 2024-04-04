using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TayqaController : ControllerBase
    {
        TigerDataService tigerData = null;
        public TayqaController(TigerDataService _tigerData)
        {
            tigerData = _tigerData;
        }
        [HttpPost("check-refund")]
        public async Task<IActionResult> CheckRefund(TayqaRefundCheckingDTO dto)
        {
            foreach (var line in dto.Lines)
            {
                var checkResult = await tigerData.CheckRefundFromCustomer(dto.ClientId, line.ItemId);
                if (checkResult < line.Amount && checkResult > 0)
                {
                    line.Amount = checkResult;
                }
                else if (checkResult < 0)
                {
                    line.Amount = 0;
                }
            }

            return Ok( new TayqaRefundResultDTO { Lines = dto.Lines });
        }
    }
}
