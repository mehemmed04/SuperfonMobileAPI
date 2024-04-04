using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class EquipmentController : ControllerBase
    {
        TigerDataService tigerData = null;
        AppDataService appDataService = null;
        SuperfonWorksContext sfContext = null;
        public EquipmentController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            sfContext = _context;
        }

        int userId { get { return this.GetUserId(); } }



        [HttpPost("request")]
        public async Task<IActionResult> InsertEquipmentRequest(EquipmentRequestDTO dto)
        {
            try
            {
                var user = await sfContext.Users.FindAsync(userId);

                EquipmentRequest equipmentRequest = new()
                {
                    UserId = userId,
                    RequestDate = DateTime.Now,
                    Quantity = dto.Quantity,
                    RequestDescription = dto.Description, Note = dto.Note
                };
                sfContext.EquipmentRequests.Add(equipmentRequest);
                await sfContext.SaveChangesAsync();
                await tigerData.InsertEquipmentRequest(equipmentRequest, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
            }
        }

        [HttpGet("request/list")]
        public async Task<IActionResult> GetEquipmentRequests()
        {
            var list = await sfContext.EquipmentRequestViews.Where(x => x.UserId == userId && x.RequestDate > DateTime.Today.AddDays(-30)).ToListAsync();
            list = list.OrderByDescending(x => x.RequestDate).ToList();
            return Ok(list);
        }
    }
}
