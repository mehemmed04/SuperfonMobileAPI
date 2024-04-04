using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Queries
{
    public static partial class UserWarehousePermissionExtensions
    {
        #region Generated Extensions
        public static IQueryable<SuperfonWorks.Data.Entities.UserWarehousePermission> ByUserId(this IQueryable<SuperfonWorks.Data.Entities.UserWarehousePermission> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        public static SuperfonWorks.Data.Entities.UserWarehousePermission GetByKey(this IQueryable<SuperfonWorks.Data.Entities.UserWarehousePermission> queryable, int userWarehousePermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserWarehousePermission> dbSet)
                return dbSet.Find(userWarehousePermissionId);

            return queryable.FirstOrDefault(q => q.UserWarehousePermissionId == userWarehousePermissionId);
        }

        public static ValueTask<SuperfonWorks.Data.Entities.UserWarehousePermission> GetByKeyAsync(this IQueryable<SuperfonWorks.Data.Entities.UserWarehousePermission> queryable, int userWarehousePermissionId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<SuperfonWorks.Data.Entities.UserWarehousePermission> dbSet)
                return dbSet.FindAsync(userWarehousePermissionId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserWarehousePermissionId == userWarehousePermissionId);
            return new ValueTask<SuperfonWorks.Data.Entities.UserWarehousePermission>(task);
        }

        public static SuperfonWorks.Data.Entities.UserWarehousePermission GetByWarehousePermissionTypeIdUserIdWarehouseNumber(this IQueryable<SuperfonWorks.Data.Entities.UserWarehousePermission> queryable, byte warehousePermissionTypeId, int userId, int warehouseNumber)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.FirstOrDefault(q => q.WarehousePermissionTypeId == warehousePermissionTypeId
                && q.UserId == userId
                    && q.WarehouseNumber == warehouseNumber);
            }

            public static Task<SuperfonWorks.Data.Entities.UserWarehousePermission> GetByWarehousePermissionTypeIdUserIdWarehouseNumberAsync(this IQueryable<SuperfonWorks.Data.Entities.UserWarehousePermission> queryable, byte warehousePermissionTypeId, int userId, int warehouseNumber)
            {
                if (queryable is null)
                    throw new ArgumentNullException(nameof(queryable));

                return queryable.FirstOrDefaultAsync(q => q.WarehousePermissionTypeId == warehousePermissionTypeId
                    && q.UserId == userId
                        && q.WarehouseNumber == warehouseNumber);
                }

                #endregion

    }
}
