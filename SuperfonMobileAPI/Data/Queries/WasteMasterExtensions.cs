using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class WasteMasterExtensions
    {
        #region Generated Extensions
        public static IQueryable<SuperfonWorks.Data.Entities.WasteMaster> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.WasteMaster> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        public static SuperfonWorks.Data.Entities.WasteMaster GetByKey(this IQueryable<SuperfonWorks.Data.Entities.WasteMaster> queryable, int wasteMasterId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.WasteMaster> dbSet)
                return dbSet.Find(wasteMasterId);

            return queryable.FirstOrDefault(q => q.WasteMasterId == wasteMasterId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.WasteMaster> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.WasteMaster> queryable, int wasteMasterId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.WasteMaster> dbSet)
                return dbSet.FindAsync(wasteMasterId);

            var task = queryable.FirstOrDefaultAsync(q => q.WasteMasterId == wasteMasterId);
            return new ValueTask<SuperfonWorks.Data.Entities.WasteMaster>(task);
        }

        #endregion

    }
}
