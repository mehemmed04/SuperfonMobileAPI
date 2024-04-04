using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserBankAccountPermissionExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.UserBankAccountPermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserBankAccountPermission> queryable, int userBankAccountPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserBankAccountPermission> dbSet)
                return dbSet.Find(userBankAccountPermissionId);

            return queryable.FirstOrDefault(q => q.UserBankAccountPermissionId == userBankAccountPermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserBankAccountPermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserBankAccountPermission> queryable, int userBankAccountPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserBankAccountPermission> dbSet)
                return dbSet.FindAsync(userBankAccountPermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserBankAccountPermissionId == userBankAccountPermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserBankAccountPermission>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.UserBankAccountPermission> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.UserBankAccountPermission> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
