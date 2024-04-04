using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperfonWorks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonWorks.Data.Entities;
using SuperfonMobileAPI.Services;
using SuperfonMobileAPI.Models.Entities;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using SuperfonMobileAPI.Shared;

namespace SuperfonMobileAPI.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        readonly SuperfonWorksContext sfContext = null;
        readonly TigerDataService tigerData = null;
        readonly AppDataService dataService = null;
        readonly SecurityService security = null;
        readonly CustomJWTValidator customJWTValidator = null;
        public UsersController(SuperfonWorksContext _sfContext, TigerDataService tigerData, AppDataService dataService, SecurityService security, CustomJWTValidator _customJWTValidator)
        {
            sfContext = _sfContext;
            this.tigerData = tigerData;
            this.dataService = dataService;
            this.security = security;
            customJWTValidator = _customJWTValidator;
        }
        [Authorize(Roles = "R-501")]
        public async Task<IActionResult> Index(bool isActive = true)
        {
            ViewData["isActiveChecked"] = isActive;
            var list =await sfContext.Users.Where(x=>x.IsActive == isActive).ToListAsync();
            var data = JsonConvert.DeserializeObject<List<UserListRowDTO>>(JsonConvert.SerializeObject(list));
            var groups = await sfContext.UserGroups.ToListAsync();
            data.ForEach(x=>x.GroupName = groups.FirstOrDefault(t=>t.UserGroupId == x.UserGroupId)?.UserGroupName);
            return View("index",data);
        }

        public async Task<IActionResult> Modify()
        {
            ViewData["UserGroups"] = await sfContext.UserGroups.ToListAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Modify(int id)
        {
            ViewData["UserGroups"] = await sfContext.UserGroups.ToListAsync();
            var user = await sfContext.Users.FindAsync(id);
            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            return View(data);
        } 
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await sfContext.Users
                .Include(x => x.UserBankAccountPermissions)
                .Include(x => x.UserBranchPermissions)
                .Include(x => x.UserCardCodePermissions)
                .Include(x => x.UserMenuPermissions)
                .Include(x => x.UserSafeboxPermissions)
                .Include(x => x.UserWarehousePermissions)


                .SingleOrDefaultAsync(u=> u.UserId == id);
            if (user == null) return NotFound();
            sfContext.UserBankAccountPermissions.RemoveRange(user.UserBankAccountPermissions);
            sfContext.UserBranchPermissions.RemoveRange(user.UserBranchPermissions);
            sfContext.UserCardCodePermissions.RemoveRange(user.UserCardCodePermissions);
            sfContext.UserMenuPermissions.RemoveRange(user.UserMenuPermissions);
            sfContext.UserSafeboxPermissions.RemoveRange(user.UserSafeboxPermissions);
            sfContext.UserWarehousePermissions.RemoveRange(user.UserWarehousePermissions);
            sfContext.Users.Remove(user);
            await sfContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Modify(UserListRowDTO dto)
        {
            string passHash = string.Empty;
            //var passHash = security.ComputeSha256Hash(loginData.Password);
            if (!string.IsNullOrEmpty(dto.RandomPassword))
            {
                passHash = security.ComputeSha256Hash(dto.RandomPassword);
            }
            
            if (dto.UserId == 0)
            {
                var data = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(dto));
                data.PassHash = "2ac9a6746aca543af8dff39894cfe8173afba21eb01c6fae33d52947222855ef";
                data.IsActive = true;
                if (passHash.Length > 0) data.PassHash = passHash;
                sfContext.Users.Add(data);
            }
            else
            {
                User user = sfContext.Users.Find(dto.UserId);
                user.DisplayName = dto.DisplayName;
                user.Email = dto.Email;
                user.Phone = dto.Phone;
                user.IsActive = dto.IsActive;
                user.UserPID = dto.UserPID;
                user.UserGroupId = dto.UserGroupId;
                if (passHash.Length > 0) user.PassHash = passHash;
                sfContext.Users.Update(user);
            }
            await sfContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> WarehousePermission(int id)
        {
            var user = await sfContext.Users.Include(x=>x.UserWarehousePermissions).SingleOrDefaultAsync(x=>x.UserId == id);
            var warehouses = await tigerData.GetWarehouses();
            var permittedWhs = user.UserWarehousePermissions.Select(x => x.WarehouseNumber).ToList();
            user.UserWarehousePermissions = null;

            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            data.Warehouses = warehouses.Where(x => permittedWhs.Contains(x.WarehouseNumber)).ToList();
            return View("WarehousePermission", data);
        }

        [HttpGet]
        public async Task<IActionResult> BranchPermission(int id)
        {
            var user = await sfContext.Users.Include(x => x.UserBranchPermissions).SingleOrDefaultAsync(x => x.UserId == id);
            var branches = await tigerData.GetBranches();
            var permittedBranches = user.UserBranchPermissions.Select(x => x.BranchNumber).ToList();
            user.UserBranchPermissions = null;

            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            data.Branches = branches.Where(x => permittedBranches.Contains(x.BranchNumber)).ToList();
            return View("BranchPermission", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehousePermission(UserListRowDTO dto)
        {
            sfContext.UserWarehousePermissions.Add(new UserWarehousePermission { UserId = dto.UserId, WarehouseNumber = dto.WarehouseNumberAdd, WarehousePermissionTypeId = 1 });
            await sfContext.SaveChangesAsync();
            return await WarehousePermission(dto.UserId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWarehousePermission(int userId, int warehouseNumber)
        {
            var whp = await sfContext.UserWarehousePermissions.SingleOrDefaultAsync(x => x.UserId == userId && x.WarehouseNumber == warehouseNumber);
            sfContext.UserWarehousePermissions.Remove(whp);
            await sfContext.SaveChangesAsync();
            return await WarehousePermission(userId);
        }

        [HttpPost]
        public async Task<IActionResult> AddBranchPermission(UserListRowDTO dto)
        {
            sfContext.UserBranchPermissions.Add(new UserBranchPermission { UserId = dto.UserId, BranchNumber = dto.BranchNumberAdd });
            await sfContext.SaveChangesAsync();
            return await BranchPermission(dto.UserId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBranchPermission(int userId, int branchNumber)
        {
            var whp = await sfContext.UserBranchPermissions.SingleOrDefaultAsync(x => x.UserId == userId && x.BranchNumber == branchNumber);
            sfContext.UserBranchPermissions.Remove(whp);
            await sfContext.SaveChangesAsync();
            return await BranchPermission(userId);
        }

        [HttpGet]
        public async Task<IActionResult> MenuPermission(int id)
        {
            var user = await sfContext.Users.Include(u=>u.UserGroup).FirstOrDefaultAsync(x=>x.UserId == id);
            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            data.MenuPermissions = await dataService.GetUserMenuPermissionsForAdminPanel(id);
            ViewData["AllMenuPermissions"] = from d in (await sfContext.MenuPermissions.Where(x => x.IsActive).ToListAsync()) 
                                      select new MenuPermissionReadDTO { MenuPermissionId = d.MenuPermissionId, PermissionName = d.PermissionName };
            ViewData["UserGroupName"] = user.UserGroup.UserGroupName;
            if(user.UserGroupId != null)
                data.MenuPermissionsFromGroup = await dataService.GetUserGroupMenuPermissionsForAdminPanel(user.UserGroupId.Value);
            return View("MenuPermission", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuPermission(UserListRowDTO dto)
        {
            sfContext.UserMenuPermissions.Add(new UserMenuPermission { UserId = dto.UserId, MenuPermissionId = dto.MenuPermissionIdAdd });
            await sfContext.SaveChangesAsync();
            return await MenuPermission(dto.UserId);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroupMenuPermission(GroupMenuPermissionDTO dto)
        {
            sfContext.UserMenuPermissions.Add(new UserMenuPermission { GroupId = dto.GroupId, MenuPermissionId = dto.MenuPermissionIdAdd });
            await sfContext.SaveChangesAsync();
            return await GroupMenuPermission(dto.GroupId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenuPermission(int userId, int menuPermissionId)
        {
            var mn = await sfContext.UserMenuPermissions.SingleOrDefaultAsync(x => x.UserId == userId && x.MenuPermissionId == menuPermissionId);
            sfContext.UserMenuPermissions.Remove(mn);
            await sfContext.SaveChangesAsync();
            return await MenuPermission(userId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroupMenuPermission(int groupId, int menuPermissionId)
        {
            var mn = await sfContext.UserMenuPermissions.SingleOrDefaultAsync(x => x.GroupId == groupId && x.MenuPermissionId == menuPermissionId);
            sfContext.UserMenuPermissions.Remove(mn);
            await sfContext.SaveChangesAsync();
            return await GroupMenuPermission(groupId);
        }



        [HttpGet]
        public async Task<IActionResult> CardCodePermission(int id)
        {
            var user = await sfContext.Users.Include(x => x.UserCardCodePermissions).SingleOrDefaultAsync(x => x.UserId == id);
            user.UserWarehousePermissions = null;

            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            data.Cards = new List<TigerCard>();
            foreach (var c in user.UserCardCodePermissions)
            {
                data.Cards.Add(await tigerData.GetCardByCode(c.CardCode));
            }
            return View("CardCodePermission", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCardCodePermission(UserListRowDTO dto)
        {
            var cardData = await tigerData.GetCardByCode(dto.CardCodePermissionAdd);
            if (cardData == null)
                return View("Error", new Models.ErrorViewModel { ErrorMessage = $"Cari kodu tapılmadı {dto.CardCodePermissionAdd}", RequestId = Guid.NewGuid().ToString() });
            sfContext.UserCardCodePermissions.Add(new UserCardCodePermission { UserId = dto.UserId, CardCode = dto.CardCodePermissionAdd, CardPermissionTypeId = 1 });
            await sfContext.SaveChangesAsync();
            return await CardCodePermission(dto.UserId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCardCodePermission(int userId, string cardCode)
        {
            var whp = await sfContext.UserCardCodePermissions.SingleOrDefaultAsync(x => x.UserId == userId && x.CardCode == cardCode);
            sfContext.UserCardCodePermissions.Remove(whp);
            await sfContext.SaveChangesAsync();
            return await CardCodePermission(userId);
        }



        [HttpGet]
        public async Task<IActionResult> SafeboxPermission(int id)
        {
            var user = await sfContext.Users.Include(x => x.UserSafeboxPermissions).SingleOrDefaultAsync(x => x.UserId == id);
            user.UserWarehousePermissions = null;

            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            data.Safeboxes = new List<TigerSafebox>();
            var sfboxes = await tigerData.GetSafeboxes();
            foreach (var c in user.UserSafeboxPermissions)
            {
                var sfbox = sfboxes.FirstOrDefault(x => x.SafeboxCode == c.SafeboxCode);
                if(sfbox != null)
                    data.Safeboxes.Add(sfbox);
            }
            return View("SafeboxPermission", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddSafeboxPermission(UserListRowDTO dto)
        {
            var safeboxData = (await tigerData.GetSafeboxes()).FirstOrDefault(x=>x.SafeboxCode == dto.SafeboxPermissionAdd);
            if (safeboxData == null)
                return View("Error", new Models.ErrorViewModel { ErrorMessage = $"Kassa tapılmadı {dto.SafeboxPermissionAdd}", RequestId = Guid.NewGuid().ToString() });
            sfContext.UserSafeboxPermissions.Add(new UserSafeboxPermission { UserId = dto.UserId, SafeboxCode = dto.SafeboxPermissionAdd });
            await sfContext.SaveChangesAsync();
            return await SafeboxPermission(dto.UserId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSafeboxPermission(int userId, string safeboxCode)
        {
            var whp = await sfContext.UserSafeboxPermissions.SingleOrDefaultAsync(x => x.UserId == userId && x.SafeboxCode == safeboxCode);
            sfContext.UserSafeboxPermissions.Remove(whp);
            await sfContext.SaveChangesAsync();
            return await SafeboxPermission(userId);
        }





        
        [HttpGet]
        public async Task<IActionResult> BankAccPermission(int id)
        {
            var user = await sfContext.Users.Include(x => x.UserBankAccountPermissions).SingleOrDefaultAsync(x => x.UserId == id);
            user.UserWarehousePermissions = null;

            var data = JsonConvert.DeserializeObject<UserListRowDTO>(JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            data.BankAccounts = new List<TigerBankAccount>();
            var accs = await tigerData.GetBankAccounts();
            foreach (var c in user.UserBankAccountPermissions)
            {
                var rt = accs.FirstOrDefault(x => x.AccountCode == c.BankAccountCode);
                if (rt != null)
                    data.BankAccounts.Add(rt);
            }
            return View("BankAccPermission", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddBankAccPermission(UserListRowDTO dto)
        {
            var BankAccData = (await tigerData.GetBankAccounts()).FirstOrDefault(x => x.AccountCode == dto.BankAccPermissionAdd);
            if (BankAccData == null)
                return View("Error", new Models.ErrorViewModel { ErrorMessage = $"Hesab nömrəsi tapılmadı {dto.BankAccPermissionAdd}", RequestId = Guid.NewGuid().ToString() });
            sfContext.UserBankAccountPermissions.Add(new UserBankAccountPermission { UserId = dto.UserId, BankAccountCode = dto.BankAccPermissionAdd });
            await sfContext.SaveChangesAsync();
            return await BankAccPermission(dto.UserId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBankAccPermission(int userId, string BankAccCode)
        {
            var whp = await sfContext.UserBankAccountPermissions.SingleOrDefaultAsync(x => x.UserId == userId && x.BankAccountCode == BankAccCode);
            sfContext.UserBankAccountPermissions.Remove(whp);
            await sfContext.SaveChangesAsync();
            return await BankAccPermission(userId);
        }


        [HttpGet]
        public string GenerateRandomPassword()
        {
            int length = 8; 
            const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" + "0123456789";
            var characterArray = alphanumericCharacters.Distinct().ToArray();
            var bytes = new byte[length * 8];
            RandomNumberGenerator.Create().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }

        [HttpGet]
        [Authorize(Roles = "R-501")]
        public async Task<IActionResult> UserGroups()
        {
            ViewData["UserGroups"] = await sfContext.UserGroups.ToListAsync();
            return View("UserGroups");
        }

        [HttpGet]
        public async Task<IActionResult> GroupMenuPermission(int id)
        {
            GroupMenuPermissionDTO dto = new GroupMenuPermissionDTO();
            var group = await sfContext.UserGroups.FindAsync(id);
            ViewData["AllMenuPermissions"] = from d in (await sfContext.MenuPermissions.Where(x => x.IsActive).ToListAsync())
                                             select new MenuPermissionReadDTO { MenuPermissionId = d.MenuPermissionId, PermissionName = d.PermissionName };
            dto.MenuPermissions = await dataService.GetUserGroupMenuPermissionsForAdminPanel(id);
            dto.GroupId = id;
            dto.GroupName = group.UserGroupName;
            return View("GroupMenuPermission", dto);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO dto,[FromQuery] string returnUrl)
        {

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";
            try
            {
                ViewData["error"] = "İstifadəçi adı və ya şifər yanlışdır";
                var user = await sfContext.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == dto.Username.ToLower());
                //user = await context.Users.GetByUsernameAsync(loginData.Username);
                if (user == null) return View("login");
                var passHash = security.ComputeSha256Hash(dto.Password);
                if (user.PassHash != passHash) return View("login");
                if (!user.IsActive) return View("login");
                var permissions = await dataService.GetUserMenuPermissions(user.UserId);
                var claims = new Claim[]
                   {
                       new Claim("UserId",user.UserId.ToString())
                   }.ToList();
                claims.AddRange(GetUserClaims(permissions));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                ViewData["error"] = string.Empty;
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("login");
            }
        }

        private IEnumerable<Claim> GetUserClaims(IEnumerable<MenuPermissionReadDTO> menuPermissions)
        {
            List<Claim> claims = new List<Claim>();
            claims.AddRange(menuPermissions.Select(item =>
                new Claim(ClaimTypes.Role, "R-" + item.MenuPermissionId.ToString())));
            return claims;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("login");
        }

        [HttpPost]
        public async Task<IActionResult> InvalidateAllUsers()
        {
            customJWTValidator.ClearAll();
            return Redirect("/users");
        }
    }
}
