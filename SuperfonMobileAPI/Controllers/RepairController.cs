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
namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RepairController : ControllerBase
    {
        TigerDataService tigerData = null;
        SuperfonWorksContext sfContext = null;
        public RepairController(TigerDataService _tigerData, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            sfContext = _context;
        }

        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpPost("repairman-request")]
        public async Task<IActionResult> InsertRequest(RepairmanRequestDTO dto)
        {
            RepairmanRequest repairmanRequest = new RepairmanRequest {Ficheno=DateTime.Now.Year.ToString() + "000000", Note = dto.Note, RepairFee = dto.RepairFee, RequestDate = DateTime.Now, SparePartPrice = dto.SparePartPrice, UserId = userId };
            sfContext.RepairmanRequests.Add(repairmanRequest);
            int inserted = await sfContext.SaveChangesAsync();
            long fchNo = Convert.ToInt64(repairmanRequest.Ficheno);
            fchNo++;
            repairmanRequest.Ficheno = fchNo.ToString();
            sfContext.RepairmanRequests.Update(repairmanRequest);
            await sfContext.SaveChangesAsync();
            if (inserted > 0)
                return Ok(repairmanRequest);
            return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
        }

        [HttpGet("repairman-request/list")]
        public async Task<IActionResult> GetRepairmanRequests()
        {
            var data = await sfContext.RepairmanRequests.Where(x => x.UserId == userId && x.RequestDate > DateTime.Today.AddDays(-30)).ToListAsync();
            return Ok(data);
        }


        [HttpGet("repairman-request/qr/{requestId}")]
        public async Task<IActionResult> GetRequestQR(int requestId, string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                deviceId = "00000000-54b3-e7c7-0000-000046bffd97";
            string opType = "CashIn";
            string salesmanCode = "101";
            string moduleSign = "U";
            var data = await sfContext.RepairmanRequests.FindAsync(requestId);

            if (data == null)
                return BadRequest(false);

            var user = await sfContext.Users.FindAsync(data.UserId);
            var cardData = await sfContext.UserCardCodePermissions.Where(x => x.UserId == user.UserId && x.CardPermissionTypeId == 1).FirstOrDefaultAsync();

            if (cardData == null)
                return BadRequest("There is no card data for this user");

            //var personnelData = await tigerData.GetEFlowPersonnel(user.UserPID);
            var cardTigerData = await tigerData.GetCardByCode(cardData.CardCode); //personnelData.ISAVANS_CARI_KODU
            string docno = data.RequestDate.ToString("yyMMdd") + moduleSign + data.RepairmanRequestId.ToString();
            var total = data.SparePartPrice + data.RepairFee;
            string txtQRCode = $"{cardTigerData?.CardId} - {cardTigerData?.CardCode} - {salesmanCode} - {total} - {opType} - {docno} - {data.Note} \n";

            // string txtQRCode = $"{cardData?.CardCode} - {slmCode} - {(data.SparePartPrice + data.RepairFee)} - {opType} - {data.RepairmanRequestId} - {data.Note}  *{data.RepairFee}";
            //  string txtQRCode = $"{cardTigerData?.CardId} - {cardTigerData?.CardCode} - {data.RepairmanRequestId} - {(data.SparePartPrice + data.RepairFee)} - {opType} - {deviceId}";
            QRCodeGenerator _qrCode = new();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(8);
            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var byteData = stream.ToArray();
                Dictionary<string, string> result = new Dictionary<string, string>();
                result["QRData"] = $"data:image/png;base64,{Convert.ToBase64String(byteData)}";
                result["textData"] = txtQRCode;
                return Ok(result);
            }
        }
    }
}
