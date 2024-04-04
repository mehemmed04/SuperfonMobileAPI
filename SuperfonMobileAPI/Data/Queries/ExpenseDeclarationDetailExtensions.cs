using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class ExpenseDeclarationDetailExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.ExpenseDeclarationDetail GetByKey(this IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> queryable, int expenseDeclarationDetailId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> dbSet)
                return dbSet.Find(expenseDeclarationDetailId);

            return queryable.FirstOrDefault(q => q.ExpenseDeclarationDetailId == expenseDeclarationDetailId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> queryable, int expenseDeclarationDetailId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> dbSet)
                return dbSet.FindAsync(expenseDeclarationDetailId);

            var task = queryable.FirstOrDefaultAsync(q => q.ExpenseDeclarationDetailId == expenseDeclarationDetailId);
            return new ValueTask<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> ByExpenseDeclarationId(this IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> queryable, int expenseDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ExpenseDeclarationId == expenseDeclarationId);
        }

        #endregion

    }
}
