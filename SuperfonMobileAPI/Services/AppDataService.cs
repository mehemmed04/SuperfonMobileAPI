using Microsoft.EntityFrameworkCore;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonWorks.Data;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Services
{
    public class AppDataService
    {
        SuperfonWorksContext dbContext = null;
        public AppDataService(SuperfonWorksContext _context)
        {
            dbContext = _context;
        }
        public async ValueTask<int> InsertExpense(ExpenseDTO expense)
        {
            await Task.Delay(1500);
            return await ValueTask.FromResult<int>(1);
        }

        public async Task<IEnumerable<MenuPermissionReadDTO>> GetUserMenuPermissions(int mUserId)
        {
            var user = await dbContext.Users.FindAsync(mUserId);
            var permissions = (await GetUserMenuPermissionsForAdminPanel(mUserId)).ToList();
            if (user.UserGroupId != null)
            {
                var groupPermissions =await GetUserGroupMenuPermissionsForAdminPanel(user.UserGroupId.Value);
                permissions.AddRange(groupPermissions);
            }
            return permissions;
        }

        public async Task<IEnumerable<MenuPermissionReadDTO>> GetUserMenuPermissionsForAdminPanel(int mUserId)
        {
            var userMenuPermissions = await dbContext.UserMenuPermissions.Include(x => x.MenuPermission).Where(x => x.UserId == mUserId).ToListAsync();
            var res = userMenuPermissions.Select(x => x.MenuPermission);
            if (mUserId == 1) res = await dbContext.MenuPermissions.Where(x => x.IsActive).ToListAsync();
            return ConvertToMPR(res);
        }

        public async Task<IEnumerable<MenuPermissionReadDTO>> GetUserGroupMenuPermissionsForAdminPanel(int groupId)
        {
            var userMenuPermissions = await dbContext.UserMenuPermissions.Include(x => x.MenuPermission).Where(x => x.GroupId == groupId).ToListAsync();
            var res = userMenuPermissions.Select(x => x.MenuPermission);
            return ConvertToMPR(res);
        }

        private static IEnumerable<MenuPermissionReadDTO> ConvertToMPR(IEnumerable<MenuPermission> res)
        {
            return (from e in res
                    select new MenuPermissionReadDTO
                    {
                        IconName = e.IconName,
                        IsActive = e.IsActive,
                        KeyWord = e.KeyWord,
                        Link = e.Link,
                        MenuPermissionId = e.MenuPermissionId,
                        MenuPermissionTypeId = e.MenuPermissionTypeId,
                        ParentId = e.ParentId,
                        PermissionName = e.PermissionName
                    });
        }
    }
}
