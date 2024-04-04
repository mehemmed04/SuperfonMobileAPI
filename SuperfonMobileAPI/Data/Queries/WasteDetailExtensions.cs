using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class WasteDetailExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.WasteDetail GetByKey(this IQueryable<SuperfonWorks.Data.Entities.WasteDetail> queryable, int wasteDetailId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.WasteDetail> dbSet)
                return dbSet.Find(wasteDetailId);

            return queryable.FirstOrDefault(q => q.WasteDetailId == wasteDetailId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.WasteDetail> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.WasteDetail> queryable, int wasteDetailId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.WasteDetail> dbSet)
                return dbSet.FindAsync(wasteDetailId);

            var task = queryable.FirstOrDefaultAsync(q => q.WasteDetailId == wasteDetailId);
            return new ValueTask<SuperfonWorks.Data.Entities.WasteDetail>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.WasteDetail> ByWasteMasterId(this IQueryable<SuperfonWorks.Data.Entities.WasteDetail> queryable, int wasteMasterId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.WasteMasterId == wasteMasterId);
        }

        #endregion

    }
}
