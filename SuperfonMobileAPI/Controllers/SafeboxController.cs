using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Shared;
using SuperfonWorks.Data;
using SuperfonWorks.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SafeboxController : ControllerBase
    {
        TigerDataService tigerData = null;
        AppDataService appDataService = null;
        SuperfonWorksContext context = null;
        public SafeboxController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            context = _context;
        }
        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpGet("current/total")]
        public async Task<ActionResult<double>> GetCurrentUserConnectedSafeboxTotal([FromQuery] DateTime? date)
        {
            var userDetails = await context.Users.FindAsync(userId);
            var personnelData = await tigerData.GetEFlowPersonnel(userDetails.UserPID);
            var safeboxes = await context.UserSafeboxPermissions.Where(x => x.UserId == userId).ToListAsync();
            if (safeboxes.Count == 0) return NotFound(Constants.UserConnectedSafeboxNotFound);
            if (date == null) date = DateTime.Today;
            return await tigerData.GetSafeboxTotal(safeboxes.First().SafeboxCode, date.Value);
        }


        [HttpPost("transfer")]
        public async Task<IActionResult> InsertSafeAmountTransfer([FromBody] SafeAmountTransferDTO safeAmount)
        {
           
            var userDetails = await context.Users.FindAsync(userId);
            //var personnelData = await tigerData.GetEFlowPersonnel(userDetails.UserPID);
            var safeboxes = await context.UserSafeboxPermissions.Where(x => x.UserId == userId).ToListAsync();
            if (safeboxes.Count == 0) return NotFound(Constants.UserConnectedSafeboxNotFound);

            SafeAmountTransfer transfer = new ()
            {
                Amount = safeAmount.Amount,
                DestinationCode = safeAmount.DestinationCode,
                Note = safeAmount.Note,
                SourceSafeboxCode = safeboxes.FirstOrDefault()?.SafeboxCode,
                TransferType = safeAmount.TransferType,
                UserId = userId,
                DateCreated = DateTime.Now
            };
            context.SafeAmountTransfers.Add(transfer);
            await context.SaveChangesAsync();

            //safeAmount.SourceSafeboxCode = personnelData.KASA_KODU;
            safeAmount.UserDisplayName = userDetails.DisplayName;
            int inserted = await tigerData.InsertSafeAmountTransfer(transfer, userDetails.DisplayName);
            if (inserted > 0)
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
        }


        [HttpGet("transfer/list")]
        public async Task<ActionResult<IEnumerable<SafeAmountTransferView>>> GetSafeAmountTransfersView()
        {
            DateTime date = DateTime.Today.AddDays(-7);
            var data = await context.SafeAmountTransferViews.Where(x => x.UserId == userId && x.DateCreated >= date).ToListAsync();
            return Ok(data);
        }
    }
}
