using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class ProductVerificationExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.ProductVerification GetByKey(this IQueryable<SuperfonWorks.Data.Entities.ProductVerification> queryable, int productVerificationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ProductVerification> dbSet)
                return dbSet.Find(productVerificationId);

            return queryable.FirstOrDefault(q => q.ProductVerificationId == productVerificationId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.ProductVerification> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.ProductVerification> queryable, int productVerificationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ProductVerification> dbSet)
                return dbSet.FindAsync(productVerificationId);

            var task = queryable.FirstOrDefaultAsync(q => q.ProductVerificationId == productVerificationId);
            return new ValueTask<SuperfonWorks.Data.Entities.ProductVerification>(task);
        }

        #endregion

    }
}
