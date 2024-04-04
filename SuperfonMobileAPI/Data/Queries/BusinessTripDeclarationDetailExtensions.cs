using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class BusinessTripDeclarationDetailExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail GetByKey(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> queryable, int businessTripDeclarationDetailId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> dbSet)
                return dbSet.Find(businessTripDeclarationDetailId);

            return queryable.FirstOrDefault(q => q.BusinessTripDeclarationDetailId == businessTripDeclarationDetailId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> queryable, int businessTripDeclarationDetailId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> dbSet)
                return dbSet.FindAsync(businessTripDeclarationDetailId);

            var task = queryable.FirstOrDefaultAsync(q => q.BusinessTripDeclarationDetailId == businessTripDeclarationDetailId);
            return new ValueTask<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> ByBusinessTripDeclarationId(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> queryable, int businessTripDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.BusinessTripDeclarationId == businessTripDeclarationId);
        }

        #endregion

    }
}
