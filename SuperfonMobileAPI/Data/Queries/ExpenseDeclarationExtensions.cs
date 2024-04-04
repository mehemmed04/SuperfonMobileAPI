using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class ExpenseDeclarationExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.ExpenseDeclaration GetByKey(this IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclaration> queryable, int expenseDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ExpenseDeclaration> dbSet)
                return dbSet.Find(expenseDeclarationId);

            return queryable.FirstOrDefault(q => q.ExpenseDeclarationId == expenseDeclarationId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.ExpenseDeclaration> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclaration> queryable, int expenseDeclarationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.ExpenseDeclaration> dbSet)
                return dbSet.FindAsync(expenseDeclarationId);

            var task = queryable.FirstOrDefaultAsync(q => q.ExpenseDeclarationId == expenseDeclarationId);
            return new ValueTask<SuperfonWorks.Data.Entities.ExpenseDeclaration>(task);
        }

        public static IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclaration> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.ExpenseDeclaration> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
