using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class SafeAmountTransferExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.SafeAmountTransfer GetByKey(this IQueryable<SuperfonWorks.Data.Entities.SafeAmountTransfer> queryable, int safeAmountTransferId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.SafeAmountTransfer> dbSet)
                return dbSet.Find(safeAmountTransferId);

            return queryable.FirstOrDefault(q => q.SafeAmountTransferId == safeAmountTransferId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.SafeAmountTransfer> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.SafeAmountTransfer> queryable, int safeAmountTransferId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.SafeAmountTransfer> dbSet)
                return dbSet.FindAsync(safeAmountTransferId);

            var task = queryable.FirstOrDefaultAsync(q => q.SafeAmountTransferId == safeAmountTransferId);
            return new ValueTask<SuperfonWorks.Data.Entities.SafeAmountTransfer>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.SafeAmountTransfer> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.SafeAmountTransfer> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
