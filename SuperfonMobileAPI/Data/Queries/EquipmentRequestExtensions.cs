using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class EquipmentRequestExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.EquipmentRequest GetByKey(this IQueryable<SuperfonWorks.Data.Entities.EquipmentRequest> queryable, int equipmentRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.EquipmentRequest> dbSet)
                return dbSet.Find(equipmentRequestId);

            return queryable.FirstOrDefault(q => q.EquipmentRequestId == equipmentRequestId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.EquipmentRequest> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.EquipmentRequest> queryable, int equipmentRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.EquipmentRequest> dbSet)
                return dbSet.FindAsync(equipmentRequestId);

            var task = queryable.FirstOrDefaultAsync(q => q.EquipmentRequestId == equipmentRequestId);
            return new ValueTask<SuperfonWorks.Data.Entities.EquipmentRequest>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.EquipmentRequest> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.EquipmentRequest> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
