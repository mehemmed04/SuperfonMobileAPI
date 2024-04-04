using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserMenuPermissionExtensions
    {
        #region Generated Extensions
        public static IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> ByGroupId(this IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> queryable, int? groupId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.GroupId == groupId || (groupId == null && q.GroupId == null)));
        }

        public static IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> ByMenuPermissionId(this IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> queryable, int menuPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.MenuPermissionId == menuPermissionId);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> queryable, int? userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
        }

        public static SuperfonWorks.Data.Entities.UserMenuPermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> queryable, int userMenuPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserMenuPermission> dbSet)
                return dbSet.Find(userMenuPermissionId);

            return queryable.FirstOrDefault(q => q.UserMenuPermissionId == userMenuPermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserMenuPermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserMenuPermission> queryable, int userMenuPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserMenuPermission> dbSet)
                return dbSet.FindAsync(userMenuPermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserMenuPermissionId == userMenuPermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserMenuPermission>(task);
        }

        #endregion

    }
}
