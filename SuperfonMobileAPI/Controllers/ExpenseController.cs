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
using System.Runtime.CompilerServices;
using Dapper;
using SuperfonMobileAPI.Domain.Models;
namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpenseController : ControllerBase
    {
        TigerDataService tigerData = null;
        AppDataService appDataService = null;
        SuperfonWorksContext sfContext = null;
        public ExpenseController(TigerDataService _tigerData, AppDataService _appDataService, SuperfonWorksContext _context)
        {
            tigerData = _tigerData;
            appDataService = _appDataService;
            sfContext = _context;
        }

        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        //ExpenseAdvanceRequestCreateModel
        [HttpPost]
        public async Task<IActionResult> InsertExpense(ExpenseDTO expense)
        {
            int inserted = await appDataService.InsertExpense(expense);
            if (inserted > 0)
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
        }

        [HttpPost("advance-request")]
        public async Task<IActionResult> InsertAdvanceRequest(ExpenseAdvanceRequestDTO dto)
        {
            var user = await sfContext.Users.FindAsync(userId);
            // var safeboxes = await sfContext.UserSafeboxPermissions.ByUserId(userId).ToListAsync();
            // if (safeboxes.Count == 0) return NotFound(Constants.UserConnectedSafeboxNotFound);

            try
            {
                ExpenseAdvanceRequest expenseAdvanceRequest = new()
                {
                    UserId = userId,
                    RequestDate = DateTime.Now,
                    RequestAmount = dto.RequestAmount,
                    RequestDescription = dto.RequestDescription,
                    RequestType = dto.RequestType
                };
                sfContext.ExpenseAdvanceRequests.Add(expenseAdvanceRequest);
                await sfContext.SaveChangesAsync();
                if (expenseAdvanceRequest.RequestType == 0)
                {
                    await tigerData.InsertExpenseAdvanceRequest(expenseAdvanceRequest, user);
                }
                else
                {
                    await tigerData.InsertExpenseForceRequest(expenseAdvanceRequest, user);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
            }


            //var userDetails = await context.Users.FindAsync(userId);
            //var safeboxes = await context.UserSafeboxPermissions.Where(x => x.UserId == userId).ToListAsync();
            //if (safeboxes.Count == 0) return NotFound(Constants.UserConnectedSafeboxNotFound);

            //SafeAmountTransfer transfer = new()
            //{
            //    Amount = safeAmount.Amount,
            //    DestinationCode = safeAmount.DestinationCode,
            //    Note = safeAmount.Note,
            //    SourceSafeboxCode = safeboxes.First().SafeboxCode,
            //    TransferType = safeAmount.TransferType,
            //    UserId = userId,
            //    DateCreated = DateTime.Now
            //};
            //context.SafeAmountTransfers.Add(transfer);
            //await context.SaveChangesAsync();

            //safeAmount.SourceSafeboxCode = safeboxes.First().SafeboxCode;
            //safeAmount.UserDisplayName = userDetails.DisplayName;
            //int inserted = await tigerData.InsertSafeAmountTransfer(transfer.SafeAmountTransferId, safeAmount);
            //if (inserted > 0)
            //    return Ok();
            //return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");

        }


        [HttpGet("advance-request/list")]
        public async Task<ActionResult<IEnumerable<ExpenseAdvanceRequestView>>> GetAdvanceRequests([FromQuery] bool? isDeclared, [FromQuery] byte requestType)
        {
            var query = sfContext.ExpenseAdvanceRequestViews.Where(x => x.UserId == userId && x.RequestDate > DateTime.Today.AddYears(-1) && x.RequestType == requestType);
            if (isDeclared.HasValue)
                query = query.Where(x => x.IsDeclared == isDeclared.Value);
            var list = await query.ToListAsync();
            list = list.OrderByDescending(x => x.RequestDate).ToList();
            return Ok(list);
        }

        [HttpPost("advance-declaration")]
        public async Task<IActionResult> InsertExpenseDeclaration(ExpenseDeclarationDTO dto)
        {
            try
            {
                var user = await sfContext.Users.FindAsync(userId);

                //  var 
                //    var safeboxes = await sfContext.UserSafeboxPermissions.Where(x => x.UserId == userId).ToListAsync();
                //   if (safeboxes.Count == 0) return NotFound(Constants.UserConnectedSafeboxNotFound);

                ExpenseDeclaration expenseDeclaration = new ExpenseDeclaration
                {
                    DeclarationDate = DateTime.Now,
                    DeclarationNote = dto.DeclarationNote,
                    UserId = userId
                };

                dto.Details.ForEach(x =>
                {
                    expenseDeclaration.ExpenseDeclarationDetails.Add(new ExpenseDeclarationDetail
                    { ExpenseDescription = x.ExpenseDescription, ExpenseAmount = x.ExpenseAmount, Date = x.Date });
                });

                var expenseAdvanceRequests = await sfContext.ExpenseAdvanceRequests.Where(x => dto.ExpenseAdvanceRequestIds.Contains(x.ExpenseAdvanceRequestId) && x.ExpenseDeclarationId == null).ToListAsync();
                if (expenseAdvanceRequests.Count != dto.ExpenseAdvanceRequestIds.Length)
                {
                    return BadRequest("Yalnız hesabata alınmamış avanslar seçilə bilər");
                }
                if (expenseAdvanceRequests.Select(x => x.RequestType).Distinct().Count() != 1)
                {
                    return BadRequest("Yalnız eyni avanslar növləri seçilə bilər");
                }
                var requestType = expenseAdvanceRequests.First().RequestType;

                using IDbContextTransaction dbTransaction = sfContext.Database.BeginTransaction();

                sfContext.ExpenseDeclarations.Add(expenseDeclaration);
                await sfContext.SaveChangesAsync();
                int newId = expenseDeclaration.ExpenseDeclarationId;

                expenseAdvanceRequests.ForEach(x => x.ExpenseDeclarationId = newId);
                await sfContext.SaveChangesAsync();

                if (requestType == 0)
                {
                    await tigerData.InsertExpenseAdvanceDeclaration(expenseDeclaration, user, dto.ExpenseAdvanceRequestIds);
                }
                if (requestType == 1)
                {
                    await tigerData.InsertExpenseAdvanceForceDeclaration(expenseAdvanceRequests.First(), expenseDeclaration.ExpenseDeclarationDetails, user);
                }

                await dbTransaction.CommitAsync();
                //await tigerData.InsertExpenseDeclaration(expenseDeclaration, user);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Əməliyyat zamanı səhvlik baş verdi");
            }
        }

        [HttpGet("advance-declaration/list")]
        public async Task<IActionResult> GetExpenseDeclarations()
        {
            var list = await sfContext.ExpenseDeclarationViews.Where(x => x.UserId == userId && x.DeclarationDate > DateTime.Today.AddDays(-30)).ToListAsync();

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExpenseDeclarationResultDTO>>(Newtonsoft.Json.JsonConvert.SerializeObject(list));


            foreach (var item in result)
            {
                var det = await sfContext.ExpenseDeclarationDetails.Where(x => x.ExpenseDeclarationId == item.ExpenseDeclarationId).ToListAsync();
                item.Details = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExpenseDeclarationResultDTO.ExpenseDeclarationResultDetailDTO>>(Newtonsoft.Json.JsonConvert.SerializeObject(det));
            }

            result = result.OrderByDescending(x => x.DeclarationDate).ToList();
            return Ok(result);
        }

        [HttpGet("advance-declaration")]
        public async Task<IActionResult> GetExpenseInformation(int declerationId)
        {
            var expense = await sfContext.ExpenseDeclarationViews
                                 .FirstOrDefaultAsync(x => x.ExpenseDeclarationId.Equals(declerationId));

            if (expense == null)
                return BadRequest("There is no expense");

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpenseDeclarationResultDTO>(Newtonsoft.Json.JsonConvert.SerializeObject(expense));

            var det = await sfContext.ExpenseDeclarationDetails
                             .Where(x => x.ExpenseDeclarationId == result.ExpenseDeclarationId).ToListAsync();

            if (det == null)
                return BadRequest("There is no information");


            var declarationDetailIds = det.Select(x => x.ExpenseDeclarationDetailId);

            List<ExpenseDeclarationInformationViewModel> model = new List<ExpenseDeclarationInformationViewModel>();

            foreach (var declarationDetailId in declarationDetailIds)
            {
                var response = await tigerData.GetExpenseDeclarationInformation(declarationDetailId);
                model.Add(response);
            }

            return Ok(model);
        }


        [HttpGet("request/qr/{requestId}")]
        public async Task<IActionResult> GetRequestQR(int requestId, string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                deviceId = "00000000-54b3-e7c7-0000-000046bffd97";
            string opType = "CashOut";
            string salesmanCode = "101";
            string moduleSign = "A";
            var data = await sfContext.ExpenseAdvanceRequests.FindAsync(requestId);
            var user = await sfContext.Users.FindAsync(data.UserId);
            var userEFlowData = await tigerData.GetEFlowPersonnel(user.UserPID);
            // var cardData = await sfContext.UserCardCodePermissions.Where(x => x.UserId == user.UserId && x.CardPermissionTypeId == 1).FirstOrDefaultAsync();
            var cardTigerData = await tigerData.GetCardByCode(userEFlowData.ISAVANS_CARI_KODU);
            string docno = data.RequestDate.ToString("yyMMdd") + moduleSign + data.ExpenseAdvanceRequestId.ToString();
            string txtQRCode = $"{cardTigerData?.CardId} - {cardTigerData?.CardCode} - {salesmanCode} - {data.RequestAmount} - {opType} - {docno} - {data.RequestDescription} \n";

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

        [HttpGet("advance-declaration/details")]
        public async Task<IActionResult> GetExpenseDetails(int expenseAdvanceRequestId)
        {
            var expenseAdvanceRequests = await sfContext.ExpenseAdvanceRequests.FirstOrDefaultAsync(x => x.ExpenseAdvanceRequestId == expenseAdvanceRequestId);

            if (expenseAdvanceRequests == null)
                return BadRequest("Not found");

            var result = new
            {
                RequestAmount = expenseAdvanceRequests.RequestAmount,
                Description = expenseAdvanceRequests.RequestDescription
            };

            return Ok(result);
        }
    }
}
