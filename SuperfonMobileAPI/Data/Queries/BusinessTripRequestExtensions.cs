using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class BusinessTripRequestExtensions
    {
        #region Generated Extensions
        public static IQueryable<SuperfonWorks.Data.Entities.BusinessTripRequest> ByBusinessTripDeclarationId(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripRequest> queryable, int? businessTripDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.BusinessTripDeclarationId == businessTripDeclarationId || (businessTripDeclarationId == null && q.BusinessTripDeclarationId == null)));
        }

        public static SuperfonWorks.Data.Entities.BusinessTripRequest GetByKey(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripRequest> queryable, int businessTripRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.BusinessTripRequest> dbSet)
                return dbSet.Find(businessTripRequestId);

            return queryable.FirstOrDefault(q => q.BusinessTripRequestId == businessTripRequestId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.BusinessTripRequest> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripRequest> queryable, int businessTripRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.BusinessTripRequest> dbSet)
                return dbSet.FindAsync(businessTripRequestId);

            var task = queryable.FirstOrDefaultAsync(q => q.BusinessTripRequestId == businessTripRequestId);
            return new ValueTask<SuperfonWorks.Data.Entities.BusinessTripRequest>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.BusinessTripRequest> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripRequest> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
