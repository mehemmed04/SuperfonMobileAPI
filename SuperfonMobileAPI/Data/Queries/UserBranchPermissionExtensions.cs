using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserBranchPermissionExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.UserBranchPermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserBranchPermission> queryable, int userBranchPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserBranchPermission> dbSet)
                return dbSet.Find(userBranchPermissionId);

            return queryable.FirstOrDefault(q => q.UserBranchPermissionId == userBranchPermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserBranchPermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserBranchPermission> queryable, int userBranchPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserBranchPermission> dbSet)
                return dbSet.FindAsync(userBranchPermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserBranchPermissionId == userBranchPermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserBranchPermission>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.UserBranchPermission> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.UserBranchPermission> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
