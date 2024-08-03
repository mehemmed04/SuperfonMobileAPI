using AutoMapper.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using SuperfonMobileAPI.Models.Entities;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Writers;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SuperfonMobileAPI.Services
{
    public class SalesBackgroundService : BackgroundService
    {
        Microsoft.Extensions.Configuration.IConfiguration configuration = null;
        IMemoryCache memoryCache = null;
        IDbConnection dbConnection = null;
        IServiceProvider serviceProvider = null;
        private const string CacheKey = "SalesData";
        private readonly ILogger<SalesBackgroundService> logger;

        public SalesBackgroundService(Microsoft.Extensions.Configuration.IConfiguration _configuration, IServiceProvider _serviceProvider, IMemoryCache _memoryCache, ILogger<SalesBackgroundService> _logger)
        {
            configuration = _configuration;
            dbConnection = new SqlConnection(configuration.GetConnectionString("TigerConnection"));
            serviceProvider = _serviceProvider;
            memoryCache = _memoryCache;
            logger = _logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("SalesBackgroundService started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    logger.LogInformation("Refreshing sales data.");
                    await RefreshSalesDataAsync();
                }
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
            logger.LogInformation("SalesBackgroundService stopped.");
        }

        private async Task RefreshSalesDataAsync()
        {
            try
            {
                using (var dbConnection = new SqlConnection(configuration.GetConnectionString("TigerConnection")))
                {
                    var salesData = await dbConnection.QueryAsync<TigerSales>(@"SELECT NR, [Satış nöqtəsi] as SaleLocation, Plan_növü as PlanType, Ayın_nömrəsi as NumberOfMonth, 
                    [Satış məbləği] as SaleAmount, Hədəf as Goal, Nəticə as Result FROM WORKREPORT..[VW_SATIS_KATEQORIYA]", commandTimeout: 120);
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    };
                    memoryCache.Set(CacheKey, salesData, cacheEntryOptions);
                    logger.LogInformation("Sales data successfully refreshed and cached.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while refreshing sales data.");
            }
        }
    }
}
