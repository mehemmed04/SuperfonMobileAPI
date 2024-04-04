using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class RepairmanRequestExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.RepairmanRequest GetByKey(this IQueryable<SuperfonWorks.Data.Entities.RepairmanRequest> queryable, int repairmanRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.RepairmanRequest> dbSet)
                return dbSet.Find(repairmanRequestId);

            return queryable.FirstOrDefault(q => q.RepairmanRequestId == repairmanRequestId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.RepairmanRequest> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.RepairmanRequest> queryable, int repairmanRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.RepairmanRequest> dbSet)
                return dbSet.FindAsync(repairmanRequestId);

            var task = queryable.FirstOrDefaultAsync(q => q.RepairmanRequestId == repairmanRequestId);
            return new ValueTask<SuperfonWorks.Data.Entities.RepairmanRequest>(task);
        }

        #endregion

    }
}
