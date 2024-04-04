using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class SalesmanPlanExtensions
    {
        #region Generated Extensions
        public static SuperfonWorks.Data.Entities.SalesmanPlan GetByKey(this IQueryable<SuperfonWorks.Data.Entities.SalesmanPlan> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.SalesmanPlan> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.SalesmanPlan> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.SalesmanPlan> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.SalesmanPlan> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<SuperfonWorks.Data.Entities.SalesmanPlan>(task);
        }

        public static SuperfonWorks.Data.Entities.SalesmanPlan GetByYearMonthSalesmanCode(this IQueryable<SuperfonWorks.Data.Entities.SalesmanPlan> queryable, int? year, int? month, string salesmanCode)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.FirstOrDefault(q => (q.Year == year || (year == null && q.Year == null))
                && (q.Month == month || (month == null && q.Month == null))
                    && q.SalesmanCode == salesmanCode);
            }

            public static Task<SuperfonWorks.Data.Entities.SalesmanPlan> GetByYearMonthSalesmanCodeAsync(this IQueryable<SuperfonWorks.Data.Entities.SalesmanPlan> queryable, int? year, int? month, string salesmanCode)
            {
                if (queryable is null)
                    throw new ArgumentNullException(nameof(queryable));

                return queryable.FirstOrDefaultAsync(q => (q.Year == year || (year == null && q.Year == null))
                    && (q.Month == month || (month == null && q.Month == null))
                        && q.SalesmanCode == salesmanCode);
                }

                #endregion

            }
        }
