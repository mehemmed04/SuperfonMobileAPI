using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class ExpenseAdvanceRequestExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.ExpenseAdvanceRequest GetByKey(this IQueryable<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> queryable, int expenseAdvanceRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> dbSet)
                return dbSet.Find(expenseAdvanceRequestId);

            return queryable.FirstOrDefault(q => q.ExpenseAdvanceRequestId == expenseAdvanceRequestId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> queryable, int expenseAdvanceRequestId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> dbSet)
                return dbSet.FindAsync(expenseAdvanceRequestId);

            var task = queryable.FirstOrDefaultAsync(q => q.ExpenseAdvanceRequestId == expenseAdvanceRequestId);
            return new ValueTask<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> ByExpenseDeclarationId(this IQueryable<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> queryable, int? expenseDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.ExpenseDeclarationId == expenseDeclarationId || (expenseDeclarationId == null && q.ExpenseDeclarationId == null)));
        }

        public static IQueryable<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
