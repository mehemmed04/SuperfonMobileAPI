using Microsoft.AspNetCore.Authentication.JwtBearer;
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

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CashoutController : ControllerBase
    {
        readonly TigerDataService tigerData = null;
        readonly AppDataService appDataService = null;
        readonly SuperfonWorksContext sfContext = null;
        public CashoutController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            sfContext = _context;
        }

        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }



        [HttpPost()]
        public async Task<IActionResult> InsertPayback(CashoutDTO dto)
        {
            var user = await sfContext.Users.Include(x=>x.UserCardCodePermissions).FirstOrDefaultAsync(u=> u.UserId == userId);
            var cardData = await tigerData.GetCardByCode(user.UserCardCodePermissions.FirstOrDefault()?.CardCode);
            if(cardData == null || !cardData.CardName.Contains("USTA",StringComparison.OrdinalIgnoreCase))
                cardData = await tigerData.GetCard(dto.CardId);
            int inserted = await tigerData.InsertCashout(dto,user, cardData);
            if (inserted > 0)
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPaybackRequests()
        {
            var user = await sfContext.Users.FindAsync(userId);
            var data = await tigerData.GetCashouts(user.UserPID);
            return Ok(data);
        }

        [HttpGet("cardDatas/list/filtration")]
        public async Task<IActionResult> GetCardDatas(string? filteringItem)
        {
            IEnumerable<TigerCard> datas = new List<TigerCard>();

            if (filteringItem == null)
            {
                return BadRequest(datas);
            }

            var user = await sfContext.Users.Include(x => x.UserCardCodePermissions).FirstOrDefaultAsync(u => u.UserId == userId);

            datas = await tigerData.GetCardsFiltration(filteringItem);

            return Ok(datas);
        }

        [HttpGet("payback/qr/{id}")]
        public async Task<IActionResult> GetPaybackQR(int id)
        {
            //var user = await sfContext.Users.FindAsync(userId);

            string opType = "CashOut";
            string salesmanCode = "101";
            string moduleSign = "C";
            var data = await tigerData.GetCashout(id);
            var cardTigerData = await tigerData.GetCardByCode(data.CardCode);
            string docno = data.RequestDate.Year.ToString() + data.RequestDate.Month.ToString() + moduleSign + data.Id.ToString();
            string txtQRCode = $"{cardTigerData?.CardId} - {cardTigerData?.CardCode} - {salesmanCode} - {data.Amount} - {opType} - {docno} - {data.PersonName} \n";

            QRCodeGenerator _qrCode = new();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(8);
            using MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var byteData = stream.ToArray();
            Dictionary<string, string> result = new()
            {
                ["QRData"] = $"data:image/png;base64,{Convert.ToBase64String(byteData)}",
                ["textData"] = txtQRCode
            };
            return Ok(result);


        }
    }

}
