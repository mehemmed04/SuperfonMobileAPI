using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Shared;
using SuperfonWorks.Data;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        SecurityService security = null;
        SuperfonWorksContext context = null;
        TigerDataService tigerData = null;
        AppDataService dataService = null;
        CustomJWTValidator customJWTValidator = null;
        public AuthController(SecurityService _security, SuperfonWorksContext _context, TigerDataService _tigerData, AppDataService _dataService, CustomJWTValidator _customJWTValidator)
        {
            security = _security;
            context = _context;
            tigerData = _tigerData;
            dataService = _dataService;
            customJWTValidator = _customJWTValidator;
        }
        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            LoginResultDTO loginResult = new()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                UserId = user.UserId,
                Username = user.Username
            };
            loginResult.MenuPermissions = await dataService.GetUserMenuPermissions(userId);
            return Ok(loginResult);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginDTO loginData)
        {
            var user = await context.Users.SingleOrDefaultAsync(x=>x.Username.ToLower() == loginData.Username.ToLower());
            //user = await context.Users.GetByUsernameAsync(loginData.Username);
            if (user == null) return Unauthorized(Constants.UsernameOrPasswordInvalid);
            var passHash = security.ComputeSha256Hash(loginData.Password);
            if (user.PassHash != passHash) return Unauthorized(Constants.UsernameOrPasswordInvalid);
            if (!user.IsActive) return Unauthorized(Constants.UsernameInactive);


            await Task.Delay(500);
            LoginResultDTO loginResult = new()
            {
                DisplayName = user.DisplayName, 
                Email = user.Email, 
                Phone = user.Phone, 
                UserId = user.UserId,
                Username = user.Username,
                Token = security.GenerateToken(user.UserId)
            };
            customJWTValidator.AddToken(loginResult.Token);
            loginResult.MenuPermissions = await dataService.GetUserMenuPermissions(user.UserId);
            return Ok(loginResult);
        }

        [HttpGet("branches")]
        public async Task<IEnumerable<TigerBranch>> GetUserBranchPermissions()
        {
            string userId = User.FindFirstValue("UserId");
            var userBranchPermissions = await context.UserBranchPermissions.Where(x => x.UserId == Convert.ToInt32(userId)).ToListAsync();
            var userBranchPermissionIds = userBranchPermissions.Select(t => t.BranchNumber);
            var tigerBranches = await tigerData.GetBranches();

            return tigerBranches.Where(x => userBranchPermissionIds.Contains(x.BranchNumber));
        }

       

        [HttpPost("change-password")]
        public async Task<ActionResult<LoginResultDTO>> ChangePassword([FromBody] ChangePassDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NewPassword))
                return BadRequest();
            var user = await context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            if (!user.IsActive) 
                return Unauthorized(Constants.UsernameInactive);
            var passHashNew = security.ComputeSha256Hash(dto.NewPassword);
            if (user.PassHash.Equals(passHashNew))
                return BadRequest();
            user.PassHash = passHashNew;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            await Task.Delay(500);
            LoginResultDTO loginResult = new()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                UserId = user.UserId,
                Username = user.Username,
                Token = security.GenerateToken(user.UserId)
            };
            loginResult.MenuPermissions = await dataService.GetUserMenuPermissions(user.UserId);
            return Ok(loginResult);
        }

    }


}
