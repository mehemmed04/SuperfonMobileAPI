using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class BusinessTripDeclarationExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.BusinessTripDeclaration GetByKey(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclaration> queryable, int businessTripDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.BusinessTripDeclaration> dbSet)
                return dbSet.Find(businessTripDeclarationId);

            return queryable.FirstOrDefault(q => q.BusinessTripDeclarationId == businessTripDeclarationId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.BusinessTripDeclaration> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclaration> queryable, int businessTripDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.BusinessTripDeclaration> dbSet)
                return dbSet.FindAsync(businessTripDeclarationId);

            var task = queryable.FirstOrDefaultAsync(q => q.BusinessTripDeclarationId == businessTripDeclarationId);
            return new ValueTask<SuperfonWorks.Data.Entities.BusinessTripDeclaration>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclaration> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.BusinessTripDeclaration> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
