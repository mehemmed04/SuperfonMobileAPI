using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserGroupExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.UserGroup GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserGroup> queryable, int userGroupId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserGroup> dbSet)
                return dbSet.Find(userGroupId);

            return queryable.FirstOrDefault(q => q.UserGroupId == userGroupId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserGroup> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserGroup> queryable, int userGroupId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserGroup> dbSet)
                return dbSet.FindAsync(userGroupId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserGroupId == userGroupId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserGroup>(task);
        }

        #endregion

    }
}
