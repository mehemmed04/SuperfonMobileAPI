using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserCardCodePermissionExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.UserCardCodePermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserCardCodePermission> queryable, int userCardCodePermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserCardCodePermission> dbSet)
                return dbSet.Find(userCardCodePermissionId);

            return queryable.FirstOrDefault(q => q.UserCardCodePermissionId == userCardCodePermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserCardCodePermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserCardCodePermission> queryable, int userCardCodePermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserCardCodePermission> dbSet)
                return dbSet.FindAsync(userCardCodePermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserCardCodePermissionId == userCardCodePermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserCardCodePermission>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.UserCardCodePermission> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.UserCardCodePermission> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
