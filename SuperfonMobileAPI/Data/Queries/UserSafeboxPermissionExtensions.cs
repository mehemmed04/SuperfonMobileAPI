using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserSafeboxPermissionExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.UserSafeboxPermission GetBySafeboxCodeUserId(this IQueryable<SuperfonWorks.Data.Entities.UserSafeboxPermission> queryable, string safeboxCode, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.FirstOrDefault(q => q.SafeboxCode == safeboxCode
                && q.UserId == userId);
        }

        public static Task<SuperfonWorks.Data.Entities.UserSafeboxPermission> GetBySafeboxCodeUserIdAsync(this IQueryable<SuperfonWorks.Data.Entities.UserSafeboxPermission> queryable, string safeboxCode, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.FirstOrDefaultAsync(q => q.SafeboxCode == safeboxCode
                && q.UserId == userId);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.UserSafeboxPermission> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.UserSafeboxPermission> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        public static SuperfonWorks.Data.Entities.UserSafeboxPermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserSafeboxPermission> queryable, int userSafeboxPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserSafeboxPermission> dbSet)
                return dbSet.Find(userSafeboxPermissionId);

            return queryable.FirstOrDefault(q => q.UserSafeboxPermissionId == userSafeboxPermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserSafeboxPermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserSafeboxPermission> queryable, int userSafeboxPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserSafeboxPermission> dbSet)
                return dbSet.FindAsync(userSafeboxPermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserSafeboxPermissionId == userSafeboxPermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserSafeboxPermission>(task);
        }

        #endregion

    }
}
