using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class SalaryAdvanceRequestExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.SalaryAdvanceRequest GetByKey(this IQueryable<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> queryable, int salaryAdvanceRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> dbSet)
                return dbSet.Find(salaryAdvanceRequestId);

            return queryable.FirstOrDefault(q => q.SalaryAdvanceRequestId == salaryAdvanceRequestId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> queryable, int salaryAdvanceRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> dbSet)
                return dbSet.FindAsync(salaryAdvanceRequestId);

            var task = queryable.FirstOrDefaultAsync(q => q.SalaryAdvanceRequestId == salaryAdvanceRequestId);
            return new ValueTask<SuperfonWorks.Data.Entities.SalaryAdvanceRequest>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
