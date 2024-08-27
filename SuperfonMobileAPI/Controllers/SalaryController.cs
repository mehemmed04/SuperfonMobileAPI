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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        TigerDataService tigerData = null;
        AppDataService appDataService = null;
        SuperfonWorksContext sfContext = null;
        private readonly ILogger<SalaryController> _logger;

        public SalaryController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context, ILogger<SalaryController> logger)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            sfContext = _context;
            _logger = logger;
        }

        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }



        [HttpPost("advance-request")]
        public async Task<IActionResult> InsertAdvanceRequest(SalaryAdvanceRequestDTO dto)
        {
            var user = await sfContext.Users.FindAsync(userId);
           // var safeboxes = await sfContext.UserSafeboxPermissions.ByUserId(userId).ToListAsync();
           // if (safeboxes.Count == 0) return NotFound(Constants.UserConnectedSafeboxNotFound);
            try
            {
                SalaryAdvanceRequest salaryAdvanceRequest = new()
                {
                    UserId = userId,
                    RequestDate = DateTime.Now,
                    RequestAmount = dto.RequestAmount,
                    RequestDescription = dto.RequestDescription, PartitionCount = dto.PartitionCount
                };
                sfContext.SalaryAdvanceRequests.Add(salaryAdvanceRequest);
                await sfContext.SaveChangesAsync();
                await tigerData.InsertSalaryAdvanceRequest(salaryAdvanceRequest, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
            }
        }

        [HttpGet("advance-request/list")]
        public async Task<ActionResult<IEnumerable<SalaryAdvanceRequestView>>> GetExpenseDeclarations()
        {
            try
            {
                var list = await sfContext.SalaryAdvanceRequestViews.Where(x => x.UserId == userId && x.RequestDate > DateTime.Today.AddDays(-30)).ToListAsync();
                list = list.OrderByDescending(x => x.RequestDate).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        [HttpGet("payroll/list")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get()
        {
            try
            {
                var user = await sfContext.Users.FindAsync(userId);
                var list = await tigerData.GetSalaryPayrolls(user.UserPID);
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        [HttpGet("payroll/qr/{id}")]
        public async Task<IActionResult> GetPayrollQR(int id)
        {
            _logger.LogInformation("GetPayrollQR called by ID: {id}", id);

            var user = await sfContext.Users.FindAsync(userId);


            if (user == null)
            {
                _logger.LogInformation("User not found by ID: {id}", id);
                return NotFound("User not found");
            }

            string opType = "CashOut";
            string salesmanCode = "101";
            string moduleSign = "F";

            _logger.LogInformation("Fetching salary payroll data for ID: {Id}", id);
            var data = await tigerData.GetSalaryPayrollById(id);

            _logger.LogInformation("Fetching personnel data for UserPID: {UserPID}", user.UserPID);
            var personnelData = await tigerData.GetEFlowPersonnel(user.UserPID);

            _logger.LogInformation("Fetching card data for CardCode: {CardCode}", personnelData.MAAS_CARI_KODU);
            var cardTigerData = await tigerData.GetCardByCode(personnelData.MAAS_CARI_KODU);

            string docno = data.Year.ToString() + data.Month.ToString() + moduleSign + data.Id.ToString();
            string txtQRCode = $"{cardTigerData?.CardId} - {cardTigerData?.CardCode} - {salesmanCode} - {data.SalaryTotal} - {opType} - {docno} - {data.PersonName} \n";

            _logger.LogInformation("Generating QR code for Document Number: {DocNo}", docno);

            QRCodeGenerator _qrCode = new();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(8);
            using MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var byteData = stream.ToArray();
            Dictionary<string, string> result = new Dictionary<string, string>();
            result["QRData"] = $"data:image/png;base64,{Convert.ToBase64String(byteData)}";
            result["textData"] = txtQRCode;

            _logger.LogInformation("QR code generated successfully");

            return Ok(result);
        }
    }
}
