using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class MenuPermissionExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.MenuPermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.MenuPermission> queryable, int menuPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.MenuPermission> dbSet)
                return dbSet.Find(menuPermissionId);

            return queryable.FirstOrDefault(q => q.MenuPermissionId == menuPermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.MenuPermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.MenuPermission> queryable, int menuPermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.MenuPermission> dbSet)
                return dbSet.FindAsync(menuPermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.MenuPermissionId == menuPermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.MenuPermission>(task);
        }

        #endregion

    }
}
