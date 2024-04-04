using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections;
using System.Collections.Generic;
using SuperfonWorks.Data;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuperfonMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PowerBIController : ControllerBase
    {
        readonly string powerBIConnStr;
        readonly SuperfonWorksContext dbContext = null;
        readonly string firmNr = "024";
        public PowerBIController(IConfiguration configuration, SuperfonWorksContext _context)
        {
            powerBIConnStr = configuration.GetConnectionString("PowerBIConnection");
            dbContext = _context;
        }
        int userId { get { return Convert.ToInt32(User.FindFirstValue("UserId")); } }

        [HttpGet("Emekhaqqi/{month:int}")]
        public async Task<dynamic> Get_Emekhaqqi(int month)
        {
            using IDbConnection connection = new SqlConnection(powerBIConnStr);
            return await connection.QueryAsync($"SELECT * FROM [dbo].[Emekhaqqi] ({month},0)");
        }

        [HttpGet("PlanIcra/{month:int}")]
        public async Task<dynamic> Get_PlanIcra(int month)
        {
            using IDbConnection connection = new SqlConnection(powerBIConnStr);
            return await connection.QueryAsync($"SELECT * FROM [dbo].[PlanIcra] ({month},0)");
        }

        [HttpGet("VCicra/{month:int}/{interval}")]
        public async Task<dynamic> Get_Emekhaqqi(int month, string interval)
        {
            using IDbConnection connection = new SqlConnection(powerBIConnStr);
            return await connection.QueryAsync($"SELECT * FROM [dbo].[VCicra] (@ay,@user_id,@araliq)", new { ay = month, user_id = 0, araliq = interval });
        }

        [HttpGet("VCReportByBranches/{month:int}/{interval}")]
        public async Task<ActionResult<dynamic>> Get_VCReport1530(int month, bool interval, [FromQuery] string orderby)
        {
            if (month < 1 || month > 12) return BadRequest();
            int year = DateTime.Now.Year;
            int day = 15;
            if(interval) day = DateTime.DaysInMonth(year, month);
            var branches = await dbContext.UserBranchPermissions.Where(x => x.UserId == userId).Select(t => t.BranchNumber).ToListAsync();
            if (branches.Count == 0) return Forbid("Filial səlahiyyəti tapılmadı");
            using IDbConnection connection = new SqlConnection(powerBIConnStr);
            var data =  await connection.QueryAsync<BranchVCModel>(@$"  
            SELECT BranchId,BranchName, SUM(InitalVC) InitalVC, SUM(RemainingVC) - SUM(InitalVC) + SUM(TotalPaid) PeriodVC, SUM(TotalPaid) TotalPaid, SUM(RemainingVC) RemainingVC ,
            CASE WHEN (SUM(RemainingVC) + SUM(TotalPaid)) = 0 THEN 0 ELSE ROUND(SUM(TotalPaid) / (SUM(RemainingVC) + SUM(TotalPaid)) * 100,1) END ExecutionPercentage
            FROM 
            (
            select 
            div.NR BranchId,
            div.name BranchName,
            cl.code CustomerCode,
            cl.definition_ CustomerName,
            ISNULL(ROUND(case when [vc_init].VC_BORC>0 then [vc_init].VC_BORC  else 0 end,2),0) InitalVC,
            ISNULL(ROUND(odeme.odeme,2),0) TotalPaid, 
            ISNULL(ROUND(case when [vc_last].VC_BORC>0 then [vc_last].VC_BORC  else 0 end,2),0) RemainingVC
            from 
            (select clientref, BRANCH, sum(AMOUNT) a from tiger3db..LG_{firmNr}_01_CLFLINE
            group by clientref, BRANCH)
            clf  
            inner join tiger3db..lg_{firmNr}_clcard cl on (cl.logicalref=clf.CLIENTREF) and cl.CODE LIKE '211.%' and cl.CODE NOT LIKE '211.01.100%'
            left join 
            (
            select nr, clientref,    sum(MEBLEG*-1) odeme from tiger3db..[E_VIEW_MUSTERI_HEREKETLERI_2{firmNr}] where TARIX between '{year}-{month}-01' and '{year}-{month}-{day}'  and
              FIS_TURU in ('5 Pul (Medax)', '6 Pul (Mex)', 'Gelen Havaleler') 
            group by nr,clientref, month(TARIX)
            ) odeme on (odeme.clientref=clf.CLIENTREF and clf.BRANCH=odeme.nr)
            left join 
            (SELECT CARDREF, [ISYERI], SUM(TOTAL*CASE WHEN SIGN=0 THEN 1 ELSE -1 END) VC_BORC   FROM  tiger3db..AS_{firmNr}_01_BT 
            WHERE DATE_<='{year}-{month}-{day}'
            group by CARDREF,[ISYERI]) [vc_last] on ([vc_last].CARDREF = clf.CLIENTREF and [vc_last].ISYERI=clf.BRANCH)
            left join 
            (SELECT CARDREF, [ISYERI], SUM(TOTAL*CASE WHEN SIGN=0 THEN 1 ELSE -1 END) VC_BORC   FROM  tiger3db..AS_{firmNr}_01_BT 
            WHERE DATE_< '{year}-{month}-01'
            group by CARDREF,[ISYERI]) [vc_init] on ([vc_init].CARDREF = clf.CLIENTREF and [vc_init].ISYERI=clf.BRANCH)
            inner join 
            (select nr, name from tiger3db..L_CAPIDIV where firmnr={firmNr}) div on (div.NR=clf.BRANCH)
            ) T
            where T.BranchId IN ({string.Join(",", branches)})
            group by BranchId,BranchName

            ");
            foreach (var d in data)
            {
                d.PeriodVC = Math.Round(d.PeriodVC,2);
                d.InitalVC = Math.Round(d.InitalVC, 2);
                d.RemainingVC = Math.Round(d.RemainingVC, 2);
                d.TotalPaid = Math.Round(d.TotalPaid, 2);
            }
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                int orderVal = Convert.ToInt32(new String(orderby.Where(Char.IsDigit).ToArray()));
                bool descending = orderby.Contains("descending", StringComparison.OrdinalIgnoreCase);
                switch (orderVal)
                {
                    case 1:
                        data = OrderBy(data,x => x.BranchId, descending);
                        break;
                    case 2:
                        data = OrderBy(data,x => x.BranchName, descending);
                        break;
                    case 3:
                        data = OrderBy(data,x => x.InitalVC, descending);
                        break;
                    case 4:
                        data = OrderBy(data,x => x.PeriodVC, descending);
                        break;
                    case 5:
                        data = OrderBy(data,x => x.TotalPaid, descending);
                        break;
                    case 6:
                        data = OrderBy(data,x => x.RemainingVC, descending);
                        break;
                    case 7:
                        data = OrderBy(data,x => x.ExecutionPercentage, descending);
                        break;
                    default:
                        break;
                }
            }
            return Ok(data);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool descending)
        {
            if (!descending)
                return source.OrderBy(keySelector);
            else
                return source.OrderByDescending(keySelector);
        }

        [HttpGet("VCReportByCustomers/{month:int}/{branchId:int}/{interval}")]
        public async Task<ActionResult<dynamic>> Get_VCReport1530(int month,int branchId, bool interval, [FromQuery] string orderby)
        {
            int year = DateTime.Now.Year;
            int day = 15;
            if (interval) day = DateTime.DaysInMonth(year, month);

            using IDbConnection connection = new SqlConnection(powerBIConnStr);
            var data = await connection.QueryAsync<CustomerVCModel>(@$" 
            SELECT T.CustomerCode, T.CustomerName, T.InitalVC, RemainingVC - InitalVC + TotalPaid PeriodVC, T.TotalPaid, T.RemainingVC,
            ROUND(CASE WHEN RemainingVC + TotalPaid = 0 THEN 0 ELSE TotalPaid / (RemainingVC + TotalPaid) END * 100,1) ExecutionPercentage
            FROM 
            (
            select 
            div.NR BranchId,
            div.name BranchName,
            cl.code CustomerCode,
            cl.definition_ CustomerName,
            ISNULL(ROUND(case when [vc_init].VC_BORC>0 then [vc_init].VC_BORC  else 0 end,2),0) InitalVC,
            ISNULL(ROUND(odeme.odeme,2),0) TotalPaid, 
            ISNULL(ROUND(case when [vc_last].VC_BORC>0 then [vc_last].VC_BORC  else 0 end,2),0) RemainingVC
            from 
            (select clientref, BRANCH, sum(AMOUNT) a from tiger3db..LG_{firmNr}_01_CLFLINE
            group by clientref, BRANCH)
            clf  
            inner join tiger3db..lg_{firmNr}_clcard cl on (cl.logicalref=clf.CLIENTREF) and cl.CODE LIKE '211.%' and cl.CODE NOT LIKE '211.01.100%'
            left join 
            (
            select nr, clientref,    sum(MEBLEG*-1) odeme from tiger3db..[E_VIEW_MUSTERI_HEREKETLERI_2{firmNr}] where TARIX between '{year}-{month}-01' and '{year}-{month}-{day}'  and
              FIS_TURU in ('5 Pul (Medax)', '6 Pul (Mex)', 'Gelen Havaleler') 
            group by nr,clientref, month(TARIX)
            ) odeme on (odeme.clientref=clf.CLIENTREF and clf.BRANCH=odeme.nr)
            left join 
            (SELECT CARDREF, [ISYERI], SUM(TOTAL*CASE WHEN SIGN=0 THEN 1 ELSE -1 END) VC_BORC   FROM  tiger3db..AS_{firmNr}_01_BT 
            WHERE DATE_<='{year}-{month}-{day}'
            group by CARDREF,[ISYERI]) [vc_last] on ([vc_last].CARDREF = clf.CLIENTREF and [vc_last].ISYERI=clf.BRANCH)
            left join 
            (SELECT CARDREF, [ISYERI], SUM(TOTAL*CASE WHEN SIGN=0 THEN 1 ELSE -1 END) VC_BORC   FROM  tiger3db..AS_{firmNr}_01_BT 
            WHERE DATE_< '{year}-{month}-01'
            group by CARDREF,[ISYERI]) [vc_init] on ([vc_init].CARDREF = clf.CLIENTREF and [vc_init].ISYERI=clf.BRANCH)
            inner join 
            (select nr, name from tiger3db..L_CAPIDIV where firmnr={firmNr}) div on (div.NR=clf.BRANCH)
            ) T
            WHERE T.BranchId = {branchId}
            ", new { ay = month });
            foreach (var d in data)
            {
                d.PeriodVC = Math.Round(d.PeriodVC, 2);
                d.InitalVC = Math.Round(d.InitalVC, 2);
                d.RemainingVC = Math.Round(d.RemainingVC, 2);
                d.TotalPaid = Math.Round(d.TotalPaid, 2);
            }
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                int orderVal = Convert.ToInt32(new String(orderby.Where(Char.IsDigit).ToArray()));
                bool descending = orderby.Contains("descending", StringComparison.OrdinalIgnoreCase);
                switch (orderVal)
                {
                    case 1:
                        data = OrderBy(data, x => x.CustomerCode, descending);
                        break;
                    case 2:
                        data = OrderBy(data, x => x.CustomerName, descending);
                        break;
                    case 3:
                        data = OrderBy(data, x => x.InitalVC, descending);
                        break;
                    case 4:
                        data = OrderBy(data, x => x.PeriodVC, descending);
                        break;
                    case 5:
                        data = OrderBy(data, x => x.TotalPaid, descending);
                        break;
                    case 6:
                        data = OrderBy(data, x => x.RemainingVC, descending);
                        break;
                    case 7:
                        data = OrderBy(data, x => x.ExecutionPercentage, descending);
                        break;
                    default:
                        break;
                }
            }
            return Ok(data);
        }
    }


    class VCModel
    {
        public double InitalVC { get; set; }
        public double PeriodVC { get; set; }
        public double TotalPaid { get; set; }
        public double RemainingVC { get; set; }
        public double ExecutionPercentage { get; set; }
    }

    class BranchVCModel : VCModel
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }
    class CustomerVCModel : VCModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }
}
