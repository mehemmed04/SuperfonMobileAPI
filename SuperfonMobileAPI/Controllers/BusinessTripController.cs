using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SuperfonMobileAPI.Domain.Models;
using SuperfonMobileAPI.Domain.Validation;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Shared;
using SuperfonWorks.Data;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Data.Queries;
using SuperfonWorks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BusinessTripController : ControllerBase
    {
        TigerDataService tigerData = null;
        AppDataService appDataService = null;
        SuperfonWorksContext sfContext = null;
        public BusinessTripController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            sfContext = _context;
        }

        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }


        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] BusinessTripDetailDTO dto)
        {
            var user = await sfContext.Users.FindAsync(userId);
            dto.Note = ExtensionMethods.ConvertLineBreaks(dto.Note);
            try
            {
                await tigerData.InsertBusinessTripRequestDetail(dto, user.UserPID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
            }
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetRequests()
        {
            var user = await sfContext.Users.FindAsync(userId);
            var list = (await tigerData.GetBusinessTrips(user.UserPID)).ToList();
            list.ForEach(x=> x.Note = ExtensionMethods.ReverseLineBreaks(x.Note));
            return Ok(list);
        }

        [HttpPost("declaration/{requestId}")]
        public async Task<IActionResult> InsertDeclaration(int requestId, [FromBody] IEnumerable<BusinessTripDeclarationDetailDTO> details)
        {
            try
            {

                var list = details.ToList();
                list.ForEach(x=>x.Note = ExtensionMethods.ConvertLineBreaks(x.Note));
                var user = await sfContext.Users.FindAsync(userId);
                await tigerData.InsertBusinessTripDeclaration(requestId, details, user);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
            }
        }

        [HttpGet("declaration/list")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetExpenseDeclarations()
        {
            var user = await sfContext.Users.FindAsync(userId);
            var list = (await tigerData.GetBusinessTripDeclarations(user.UserPID)).ToList();
            list.ForEach(x => x.Note = ExtensionMethods.ReverseLineBreaks(x.Note));
            return Ok(list);
        }

        [HttpGet("declaration/businessTripNumbersList")]
        public async Task<ActionResult<IEnumerable<string>>> GetExpenseBusinessTripNumbers()
        {
            var user = await sfContext.Users.FindAsync(userId);
            var list = (await tigerData.GetBusinessTripNumbers(user.UserPID)).ToList();
            return Ok(list);
        }


        [HttpPost("declaration/detail")]
        public async Task<IActionResult> GetExpenseDeclarationDetails([FromBody] BusinessTripDetailsRequest request)
        {
            var user = await sfContext.Users.FindAsync(userId);
            var result = (await tigerData.GetBusinessTripDeclarationDetails(user.UserPID, request));

            if (result != null)
                return Ok(result);

            return BadRequest("You entered wrong information");
        }
    }
}
