using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserExtensions
    {
        #region Generated Extensions
        public static IQueryable<SuperfonWorks.Data.Entities.User> ByUserGroupId(this IQueryable<SuperfonWorks.Data.Entities.User> queryable, int? userGroupId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.UserGroupId == userGroupId || (userGroupId == null && q.UserGroupId == null)));
        }

        public static SuperfonWorks.Data.Entities.User GetByKey(this IQueryable<SuperfonWorks.Data.Entities.User> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.User> dbSet)
                return dbSet.Find(userId);

            return queryable.FirstOrDefault(q => q.UserId == userId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.User> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.User> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.User> dbSet)
                return dbSet.FindAsync(userId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserId == userId);
            return new ValueTask<SuperfonWorks.Data.Entities.User>(task);
        }

        public static SuperfonWorks.Data.Entities.User GetByUsername(this IQueryable<SuperfonWorks.Data.Entities.User> queryable, string username)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.FirstOrDefault(q => q.Username == username);
        }

        public static Task<SuperfonWorks.Data.Entities.User> GetByUsernameAsync(this IQueryable<SuperfonWorks.Data.Entities.User> queryable, string username)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.FirstOrDefaultAsync(q => q.Username == username);
        }

        #endregion

    }
}
