using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using SuperfonMobileAPI.Models.Entities;
using SuperfonMobileAPI.Models.Dtos;
using SuperfonWorks.Data.Entities;
using SuperfonMobileAPI.Domain.Exceptions;
using SuperfonMobileAPI.Models.EFlow;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using SuperfonMobileAPI.Domain.Models;
using System.Xml.Linq;
using SuperfonMobileAPI.Domain.Validation;

namespace SuperfonMobileAPI.Services
{
    public class TigerDataService
    {
        IConfiguration configuration = null;
        IDbConnection dbConnection = null;
        const string oldFirmno = "001";
        const string firmno = "024";
        public TigerDataService(IConfiguration _configuration)
        {
            configuration = _configuration;
            dbConnection = new SqlConnection(configuration.GetConnectionString("TigerConnection"));
        }

        public async Task<IEnumerable<ServiceCard>> GetServiceCards()
        {
            return await dbConnection.QueryAsync<ServiceCard>($"select CODE ServiceCardCode , DEFINITION_ ServiceCardName from LG_{firmno}_SRVCARD WHERE CODE != 'ÿ'");
        }

        public async Task<IEnumerable<TigerBranch>> GetBranches()
        {
            return await dbConnection.QueryAsync<TigerBranch>($"select NR BranchNumber , NAME BranchName from L_CAPIDIV WHERE FIRMNR = {firmno}");
        }
        public async Task<IEnumerable<TigerBank>> GetBanks()
        {
            return await dbConnection.QueryAsync<TigerBank>($"select CODE AS BankCode, DEFINITION_ as BankName, BRANCH as BranchName from LG_{firmno}_BNCARD WHERE ACTIVE = 0 ");
        }
        public async Task<IEnumerable<TigerSafebox>> GetSafeboxes()
        {
            return await dbConnection.QueryAsync<TigerSafebox>($"select Code as SafeboxCode, NAME as SafeboxName from LG_{firmno}_KSCARD ");
        }

        public async Task<IEnumerable<TigerBankAccount>> GetBankAccounts()
        {
            return await dbConnection.QueryAsync<TigerBankAccount>($"select LOGICALREF as AccountId, CODE as AccountCode, DEFINITION_ as AccountName from LG_{firmno}_BANKACC ");
        }

        public async Task<double> GetSafeboxTotal(string safeboxCode, DateTime date)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<double>($@"
            SELECT ROUND(ISNULL(SUM(CASE WHEN TRCURR IN(0, 162)
                   THEN AMOUNT * (CASE SIGN
                                      WHEN 0
                                      THEN 1
                                      ELSE-1
                                  END)
                   ELSE 0
                END),0),2) AS Total
            FROM LG_{firmno}_01_KSLINES WITH (NOLOCK)
            WHERE CARDREF = (SELECT LOGICALREF FROM LG_{firmno}_KSCARD WHERE CODE = @KSCODE)
            AND CANCELLED = 0
            AND STATUS = 0
		    AND DATE_ <= @DATE ", new { KSCODE = safeboxCode, DATE = date.Date });
        }

        public async Task<IEnumerable<TigerCard>> GetCards(string expression)
        {
            expression = "%" + expression + "%";
            return await dbConnection.QueryAsync<TigerCard>($"select LOGICALREF CardId,CODE as CardCode, DEFINITION_ as CardName from LG_{firmno}_CLCARD where CODE like @card or DEFINITION_ like @card ", new { card = expression });
        }

        public async Task<TigerCard> GetCard(int id)
        {

            return await dbConnection.QueryFirstOrDefaultAsync<TigerCard>($"select LOGICALREF CardId,CODE as CardCode, DEFINITION_ as CardName from LG_{firmno}_CLCARD where LOGICALREF=@id", new { id });
        }

        public async Task<TigerCard> GetCardByCode(string code)
        {

            return await dbConnection.QueryFirstOrDefaultAsync<TigerCard>($"select LOGICALREF CardId,CODE as CardCode, DEFINITION_ as CardName from LG_{firmno}_CLCARD where CODE=@code", new { code });
        }

        public async Task<TigerBankAccount> GetBankAccountByCode(string code)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<TigerBankAccount>($"select LOGICALREF as AccountId, CODE as AccountCode, DEFINITION_ as AccountName from LG_{firmno}_BANKACC WHERE CODE = @code ", new { code });
        }

        public async Task<TigerProduct> GetProduct(string barcode)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<TigerProduct>($"select  ITM.LOGICALREF ProductId,ITM.CODE ProductCode,ITM.NAME ProductName,BRC.BARCODE Barcode from LG_{firmno}_ITEMS ITM INNER JOIN LG_{firmno}_UNITBARCODE BRC ON  ITM.LOGICALREF = BRC.ITEMREF WHERE BRC.BARCODE = @Barcode ", new { barcode });
        }
        public async Task<TigerProduct> GetProductByCode(string code)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<TigerProduct>($"select  ITM.LOGICALREF ProductId,ITM.CODE ProductCode,ITM.NAME ProductName,BRC.BARCODE Barcode from LG_{firmno}_ITEMS ITM INNER JOIN LG_{firmno}_UNITBARCODE BRC ON  ITM.CODE =  @code ", new { code });
        }

        readonly string productSearch_cols = @"ITM.LOGICALREF ProductId,
            ITM.CODE ProductCode,
            ITM.NAME ProductName,
            BRC.BARCODE Barcode ";
        readonly string productSearch_query = @$"from LG_{firmno}_ITEMS ITM 
            INNER JOIN LG_{firmno}_UNITBARCODE BRC ON ITM.LOGICALREF = BRC.ITEMREF ";
        readonly string productSearch_conditions = " WHERE BRC.BARCODE LIKE @value OR ITM.CODE LIKE @value OR ITM.NAME LIKE @value ";
        public async Task<TigerProductListDTO> SearchProducts(string expression, int currentPage, int rowspPage)
        {
            TigerProductListDTO dto = new TigerProductListDTO();
            if (rowspPage == 0) rowspPage = 20;
            expression = "%" + expression + "%";
            string fetchOffset = @$"
            OFFSET ((@currentPage - 1) * @rowspPage) ROWS
            FETCH NEXT @rowspPage ROWS ONLY ";
            var countAll = await dbConnection.QueryFirstAsync<int>(@$" select COUNT(*) {productSearch_query} ", new { value = expression, currentPage, rowspPage });
            double pageCount = (double)((decimal)countAll / Convert.ToDecimal(rowspPage));
            dto.PageCount = (int)Math.Ceiling(pageCount);
            dto.Products = await dbConnection.QueryAsync<TigerProduct>(@$" select {productSearch_cols} {productSearch_query} {productSearch_conditions} ORDER BY ITM.NAME {fetchOffset}", new { value = expression, currentPage, rowspPage });
            dto.CurrentPageIndex = currentPage;
            return dto;
        }

        public Task<IEnumerable<TigerProduct>> SearchProducts(string expression)
        {
            return dbConnection.QueryAsync<TigerProduct>(@$" select {productSearch_cols} {productSearch_query} {productSearch_conditions} ORDER BY ITM.NAME ", new { value = expression });
        }

        public Task<IEnumerable<TigerProduct>> GetAllProducts()
        {
            return dbConnection.QueryAsync<TigerProduct>(@$" select TOP(1000) {productSearch_cols} {productSearch_query} ");
        }



        public async Task<TigerProduct> GetProductIncludingDetails(string barcode, int[] branches)
        {
            var product = await GetProduct(barcode);
            return await IncludeProductDetails(product, branches);
        }

        public async Task<TigerProduct> IncludeProductDetails(TigerProduct product, int[] branches)
        {
            var branchDetailsOfProduct = await
                dbConnection.QueryAsync<TigerProductBranchInfo>($@"
            select PRC.BUYPRICE Price,
            DIV.NR BranchNumber,
            DIV.NAME BranchName, 
            ROUND(ISNULL(TOT.STOCK,0),2) Stock 
            from LK_{firmno}_PRCLIST PRC
            INNER JOIN L_CAPIDIV DIV ON PRC.OFFICECODE = DIV.NR and DIV.FIRMNR = {firmno}
            OUTER APPLY(select SUM(ONHAND) STOCK from LV_{firmno}_01_STINVTOT TT WHERE TT.STOCKREF = PRC.STREF and TT.INVENNO = PRC.OFFICECODE) TOT
            where PRC.STREF = {product.ProductId} ");
            product.ProductBranchDetails = new List<TigerProductBranchInfo>(branchDetailsOfProduct.Where(x => x.Stock > 0));
            product.ProductBranchDetails.ForEach(x => x.IsPermitted = branches.Contains(x.BranchNumber));
            product.ProductBranchDetails = product.ProductBranchDetails.OrderByDescending(x => x.IsPermitted).ToList();
            return product;
        }

        //public async Task<TigerProduct> GetWasteProduct(int customerId, string barcode)
        //{
        //   var product = await GetProduct(barcode);
        //    if (product ==null)
        //    {
        //        throw new CustomDataException("Məhsul Tapılmadı!");
        //    }
        //   int cnt=   dbConnection.QueryFirstOrDefault<int>($"select COUNT(*) cnt  from LG_{firmno}_01_STLINE STL WHERE STL.TRCODE IN (7,8) AND STL.CANCELLED=0 AND STL.CLIENTREF=@customerId AND STL.STOCKREF=@ProductId ", new { customerId, product.ProductId });
        //    if (cnt>0)
        //    {
        //        return product;
        //    }
        //    throw new CustomDataException("Seçilmiş məhsul ilə müştəri əlaqəsi mövcud deyil!");

        //}


        public async Task<double> CheckRefundFromCustomer(int clientId, int itemId)
        {
            // GETDATE()-11 cunki GETDATE() saat deqie saniyrleri de qaytarir deye ve DATE_ tekce tarix olduguna gore hemin 1-cu gunu itirmemek ucun, 10 gunden berini hesabladiqda GETADTE() - 11
            double value = await dbConnection.QueryFirstOrDefaultAsync<double>($@" 
            select ISNULL(SUM(CASE WHEN TRCODE IN (2,3) THEN 0-AMOUNT*UINFO2 ELSE AMOUNT*UINFO2 END),0) REFUNDABLE_AMOUNT 
            from LG_{firmno}_01_STLINE WHERE TRCODE IN (7,8,3,2) and STOCKREF = {itemId} and CLIENTREF = {clientId} and CANCELLED = 0  and DATE_ < GETDATE()-11
            ");
            return value;

        }

        async Task<string> GetNewNumber(string tableName, IDbTransaction transaction = null)
        {
            try
            {
                return await dbConnection.ExecuteScalarAsync<string>(@"
            UPDATE ANDROID.dbo.AU_DOCMENT_NUMBER SET NUMBER = NUMBER + 1 WHERE TABLE_NAME = @tableName
            select ISNULL(PREFIX,'') + SUBSTRING('0000000000000000000',1,LENGTH_-LEN(CONVERT(varchar(25),NUMBER))) + CONVERT(varchar(25),NUMBER)
            FROM ANDROID.dbo.AU_DOCMENT_NUMBER WHERE TABLE_NAME = @tableName
            ", new { tableName }, transaction);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<int> InsertSafeAmountTransfer(SafeAmountTransfer safeAmount, string userDisplayName)
        {
            try
            {
                string tableName = "AU_001_01_KASATRANSFER";
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                if (safeAmount.TransferType == 1)
                {
                    await dbConnection.ExecuteAsync($@"INSERT INTO ANDROID.dbo.{tableName}
                (REFERANSID,TARIH,TRANSFERTIP,TRANSFERNO,KULLANICI,TRANSFER_ACIKLAMASI,CIKIS_KASASI,GIRIS_KASASI,TUTAR,EFLOW_DURUM)
                VALUES
                ({safeAmount.SafeAmountTransferId},GETDATE(),{safeAmount.TransferType},'{docNumber}','{userDisplayName}',@Note,@SourceSafeboxCode,@DestinationCode,@Amount,0)", safeAmount, transaction);
                }
                if (safeAmount.TransferType == 2)
                {
                    await dbConnection.ExecuteAsync($@"INSERT INTO ANDROID.dbo.{tableName}
                (REFERANSID,TARIH,TRANSFERTIP,TRANSFERNO,KULLANICI,TRANSFER_ACIKLAMASI,CIKIS_KASASI,GIRIS_BANKA_HESABI,TUTAR,EFLOW_DURUM)
                VALUES
                ({safeAmount.SafeAmountTransferId},GETDATE(),{safeAmount.TransferType},'{docNumber}','{userDisplayName}',@Note,@SourceSafeboxCode,@DestinationCode,@Amount,0)", safeAmount, transaction);
                }
                transaction.Commit();
                return safeAmount.SafeAmountTransferId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        //public async Task<int> insertTransferAccResult(int eflowPersID, string eflowPersName,int stficheId, CompleteTransferDTO dto)
        //{
        //    try
        //    {
        //        if ((dto.AddedLines == null || dto.AddedLines.Count == 0) && (dto.ModifiedLines == null || dto.ModifiedLines.Count == 0))
        //        {
        //            // eger butovdurse SPECODE = 'KABUL' eflow yoxdur.
        //            await ChangeStFiche(stficheId, "KABUL");
        //        }
        //        else
        //        {
        //            // eks halda SPECODE = 'HATALI' sonra da getsin EFLOW-a
        //            await ChangeStFiche(stficheId, "HATALI");
        //        }

        //        var ficheHeader = (await GetStockFiches(new TigerStockFicheFilter { StockFicheId = stficheId })).FirstOrDefault();
        //        if (ficheHeader == null) return 0;
        //        var lines = await GeTransferFicheLines(stficheId);
        //        List<MALZEMEFAZLAEKSIK> eflowLines = new();
        //        var whs = await GetWarehouses();
        //        foreach (var line in lines)
        //        {

        //            double accQuantity = line.Quantity;
        //            var added = dto.AddedLines.FirstOrDefault(x => x.ItemCode == line.ItemCode);
        //            if (added != null) accQuantity = added.Quantity;
        //            var modified = dto.ModifiedLines.FirstOrDefault(x => x.ItemCode == line.ItemCode);
        //            if (modified != null) accQuantity = modified.Quantity;
        //            if (accQuantity == line.Quantity) continue;
        //            MALZEMEFAZLAEKSIK mfe = new()
        //            {
        //                ACIKLAMA = string.Empty,
        //                MALZEME_KOD = line.ItemCode,
        //                AMBAR_FIS_NO = ficheHeader.Ficheno, 
        //                AMBAR_FIS_TARIHI = ficheHeader.Date, 
        //                ANA_AMBAR_ID = ficheHeader.SourceWarehouse, 
        //                ANA_AMBAR_ADI = whs.FirstOrDefault(x=>x.WarehouseNumber == ficheHeader.SourceWarehouse).WarehouseName,
        //                GIREN_AMBAR_ID = ficheHeader.DestWarehouse,
        //                GIREN_AMBAR_ADI = whs.FirstOrDefault(x => x.WarehouseNumber == ficheHeader.DestWarehouse).WarehouseName,
        //                MALZEME_ADI = line.ItemName, 
        //                MALZEME_ID = line.ItemId, 
        //                EFLOW_KONTROL = 0, 
        //                FISTEKI_MIKTAR = line.Quantity, 
        //                TESLIM_ALAN_ADI = eflowPersName, 
        //                TESLIM_ALAN_ID = eflowPersID, 
        //                TESLIM_ALINAN_MIKTAR = accQuantity
        //            };
        //            eflowLines.Add(mfe);
        //        }


        //        dbConnection.Open();
        //        using IDbTransaction transaction = dbConnection.BeginTransaction();

        //        foreach (var mfe in eflowLines)
        //        {
        //            await dbConnection.ExecuteAsync($@"INSERT INTO ANDROID.dbo.AU_001_01_MALZEMEFAZLAEKSIK
        //              ([MALZEME_ID]
        //                       ,[MALZEME_KOD]
        //                       ,[MALZEME_ADI]
        //                       ,[ANA_AMBAR_ID]
        //                       ,[ANA_AMBAR_ADI]
        //                       ,[GIREN_AMBAR_ID]
        //                       ,[GIREN_AMBAR_ADI]
        //                       ,[TESLIM_ALAN_ID]
        //                       ,[TESLIM_ALAN_ADI]
        //                       ,[AMBAR_FIS_TARIHI]
        //                       ,[AMBAR_FIS_NO]
        //                       ,[FISTEKI_MIKTAR]
        //                       ,[TESLIM_ALINAN_MIKTAR]
        //                       ,[ACIKLAMA]
        //                       ,[EFLOW_KONTROL])
        //                 VALUES
        //                       (@MALZEME_ID
        //                       ,@MALZEME_KOD
        //                       ,@MALZEME_ADI
        //                       ,@ANA_AMBAR_ID
        //                       ,@ANA_AMBAR_ADI
        //                       ,@GIREN_AMBAR_ID
        //                       ,@GIREN_AMBAR_ADI
        //                       ,@TESLIM_ALAN_ID
        //                       ,@TESLIM_ALAN_ADI
        //                       ,@AMBAR_FIS_TARIHI
        //                       ,@AMBAR_FIS_NO
        //                       ,@FISTEKI_MIKTAR
        //                       ,@TESLIM_ALINAN_MIKTAR
        //                       ,@ACIKLAMA
        //                       ,@EFLOW_KONTROL)
        //        ", mfe, transaction);
        //        }



        //        transaction.Commit();
        //        return stficheId;
        //    }
        //    catch (Exception ex)
        //    {

        //        return 0;
        //    }
        //}
        public async Task<int> insertTransferAccResult(int eflowPersID, string eflowPersName, int stficheId, CompleteTransferDTO dto)
        {
            try
            {
                if ((dto.AddedLines == null || dto.AddedLines.Count == 0) && (dto.ModifiedLines == null || dto.ModifiedLines.Count == 0))
                {
                    // eger butovdurse SPECODE = 'KABUL' eflow yoxdur.
                    await ChangeStFiche(stficheId, "KABUL");
                }
                else
                {
                    // eks halda SPECODE = 'HATALI' sonra da getsin EFLOW-a
                    await ChangeStFiche(stficheId, "HATALI");
                }
                List<MALZEMEFAZLAEKSIK> eflowLines = new();
                var ficheHeader = (await GetStockFiches(new TigerStockFicheFilter { StockFicheId = stficheId })).FirstOrDefault();
                if (ficheHeader == null) return 0;
                var lines = await GeTransferFicheLines(stficheId);
                var whs = await GetWarehouses();
                foreach (var added in dto.AddedLines)
                {
                    var product = await GetProductByCode(added.ItemCode);
                    MALZEMEFAZLAEKSIK mfe = new()
                    {
                        ACIKLAMA = string.Empty,
                        MALZEME_KOD = added.ItemCode,
                        AMBAR_FIS_NO = ficheHeader.Ficheno,
                        AMBAR_FIS_TARIHI = ficheHeader.Date,
                        ANA_AMBAR_ID = ficheHeader.SourceWarehouse,
                        ANA_AMBAR_ADI = whs.FirstOrDefault(x => x.WarehouseNumber == ficheHeader.SourceWarehouse).WarehouseName,
                        GIREN_AMBAR_ID = ficheHeader.DestWarehouse,
                        GIREN_AMBAR_ADI = whs.FirstOrDefault(x => x.WarehouseNumber == ficheHeader.DestWarehouse).WarehouseName,
                        MALZEME_ADI = product.ProductName,
                        MALZEME_ID = product.ProductId,
                        EFLOW_KONTROL = 0,
                        FISTEKI_MIKTAR = 0,
                        TESLIM_ALAN_ADI = eflowPersName,
                        TESLIM_ALAN_ID = eflowPersID,
                        TESLIM_ALINAN_MIKTAR = added.Quantity
                    };
                    eflowLines.Add(mfe);
                }




                foreach (var modified in dto.ModifiedLines)
                {

                    double accQuantity = modified.Quantity;
                    var line = lines.FirstOrDefault(x => x.ItemCode == modified.ItemCode);
                    if (accQuantity == line.Quantity) continue;
                    MALZEMEFAZLAEKSIK mfe = new()
                    {
                        ACIKLAMA = string.Empty,
                        MALZEME_KOD = line.ItemCode,
                        AMBAR_FIS_NO = ficheHeader.Ficheno,
                        AMBAR_FIS_TARIHI = ficheHeader.Date,
                        ANA_AMBAR_ID = ficheHeader.SourceWarehouse,
                        ANA_AMBAR_ADI = whs.FirstOrDefault(x => x.WarehouseNumber == ficheHeader.SourceWarehouse).WarehouseName,
                        GIREN_AMBAR_ID = ficheHeader.DestWarehouse,
                        GIREN_AMBAR_ADI = whs.FirstOrDefault(x => x.WarehouseNumber == ficheHeader.DestWarehouse).WarehouseName,
                        MALZEME_ADI = line.ItemName,
                        MALZEME_ID = line.ItemId,
                        EFLOW_KONTROL = 0,
                        FISTEKI_MIKTAR = line.Quantity,
                        TESLIM_ALAN_ADI = eflowPersName,
                        TESLIM_ALAN_ID = eflowPersID,
                        TESLIM_ALINAN_MIKTAR = accQuantity
                    };
                    eflowLines.Add(mfe);
                }


                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();

                foreach (var mfe in eflowLines)
                {
                    await dbConnection.ExecuteAsync($@"INSERT INTO ANDROID.dbo.AU_001_01_MALZEMEFAZLAEKSIK
                      ([MALZEME_ID]
                               ,[MALZEME_KOD]
                               ,[MALZEME_ADI]
                               ,[ANA_AMBAR_ID]
                               ,[ANA_AMBAR_ADI]
                               ,[GIREN_AMBAR_ID]
                               ,[GIREN_AMBAR_ADI]
                               ,[TESLIM_ALAN_ID]
                               ,[TESLIM_ALAN_ADI]
                               ,[AMBAR_FIS_TARIHI]
                               ,[AMBAR_FIS_NO]
                               ,[FISTEKI_MIKTAR]
                               ,[TESLIM_ALINAN_MIKTAR]
                               ,[ACIKLAMA]
                               ,[EFLOW_KONTROL])
                         VALUES
                               (@MALZEME_ID
                               ,@MALZEME_KOD
                               ,@MALZEME_ADI
                               ,@ANA_AMBAR_ID
                               ,@ANA_AMBAR_ADI
                               ,@GIREN_AMBAR_ID
                               ,@GIREN_AMBAR_ADI
                               ,@TESLIM_ALAN_ID
                               ,@TESLIM_ALAN_ADI
                               ,@AMBAR_FIS_TARIHI
                               ,@AMBAR_FIS_NO
                               ,@FISTEKI_MIKTAR
                               ,@TESLIM_ALINAN_MIKTAR
                               ,@ACIKLAMA
                               ,@EFLOW_KONTROL)
                ", mfe, transaction);
                }



                transaction.Commit();
                return stficheId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> InsertExpenseAdvanceRequest(ExpenseAdvanceRequest expenseAdvanceRequest, User user)
        {
            try
            {
                var userData = await GetEFlowPersonnel(user.UserPID);
                string tableName = "AU_001_01_ISAVANSTALEP";
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                           ([REFERANSID]
                           ,[TALEP_TARIHI]
                           ,[IS_AVANSNO]
                           ,[TALEP_EDEN_PERS_KOD]
                           ,[TALEP_EDEN_PERS_ADI]
                           ,[TALEP_TUTARI]
                           ,[TALEP_KASA_KODU]
                           ,[TALEP_SEBEBI],EFLOW_DURUM)
                     VALUES
                           (@RefId
                           ,@Date
                           ,@DocNumber
                           ,@UserPID
                           ,@UserDisplayName
                           ,@Amount
                           ,@SafeboxCode
                           ,@Note,0)
                ", new { RefId = expenseAdvanceRequest.ExpenseAdvanceRequestId, Date = DateTime.Now, DocNumber = docNumber, UserPID = user.UserPID, UserDisplayName = user.DisplayName, Amount = expenseAdvanceRequest.RequestAmount, SafeboxCode = userData.KASA_KODU, Note = expenseAdvanceRequest.RequestDescription }, transaction);
                transaction.Commit();
                return expenseAdvanceRequest.ExpenseAdvanceRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> InsertExpenseForceRequest(ExpenseAdvanceRequest expenseAdvanceRequest, User user)
        {
            try
            {
                var userData = await GetEFlowPersonnel(user.UserPID);
                string tableName = "AU_001_01_FORSMAJOR";
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                           ([REFERANSID]
                           ,[TALEP_TARIHI]
                           ,[TALEP_NO]
                           ,[TALEP_EDEN_PERS_KOD]
                           ,[TALEP_EDEN_PERS_ADI]
                           ,[TALEP_TUTARI]
                           ,[TALEP_KASA_KODU]
                           ,[TALEP_SEBEBI],EFLOW_DURUM)
                     VALUES
                           (@RefId
                           ,@Date
                           ,@DocNumber
                           ,@UserPID
                           ,@UserDisplayName
                           ,@Amount
                           ,@SafeboxCode
                           ,@Note,0)
                ", new { RefId = expenseAdvanceRequest.ExpenseAdvanceRequestId, Date = DateTime.Now, DocNumber = docNumber, UserPID = user.UserPID, UserDisplayName = user.DisplayName, Amount = expenseAdvanceRequest.RequestAmount, SafeboxCode = userData.KASA_KODU, Note = expenseAdvanceRequest.RequestDescription }, transaction);
                transaction.Commit();
                return expenseAdvanceRequest.ExpenseAdvanceRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> InsertExpenseAdvanceDeclaration(ExpenseDeclaration expenseDeclaration, User user, int[] requestIDs)
        {
            try
            {
                var userData = await GetEFlowPersonnel(user.UserPID);

                string tableName = "AU_001_01_ISAVANSHESAP";
                string personnelName = await GetPersonnelNameByPersonnelCode(user.UserPID);
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                        ([REFERANSID]
                        ,[IS_AVANSNO]
                        ,[PERSONEL_KODU]
                        ,[PERSONEL_ADI]
                        ,[AVANS_TUTARI]
                        ,[AVANS_KASA_KODU]
                        ,[EFLOW_DURUM])
                 VALUES
                       (@RefId
                       ,@DocNumber
                       ,@UserPID
                       ,@PersonnelName
                       ,@Amount
                       ,@SafeboxCode
                       ,0)
                ", new { RefId = expenseDeclaration.ExpenseDeclarationId, Date = DateTime.Now, DocNumber = docNumber, UserPID = user.UserPID, PersonnelName = personnelName, Amount = expenseDeclaration.ExpenseAdvanceRequests.Sum(x => x.RequestAmount), SafeboxCode = userData.KASA_KODU }, transaction);

                foreach (var det in expenseDeclaration.ExpenseDeclarationDetails)
                {
                    await dbConnection.ExecuteAsync($@"
                    INSERT INTO [ANDROID].[dbo].[AU_001_01_ISAVANSHESAPDETAY]
                       ([REFERANSID]
                       ,[HESAP_REF]
                       ,[MASRAF_ACIKLAMASI]
                       ,[MASRAF_TUTARI]
                       ,[MASRAF_TARIHI])
                 VALUES
                       (@RefId
                       ,@ExpId
                       ,@ExpenseDescription
                       ,@ExpenseAmount
                       ,@Date)
                ", new
                    {
                        RefId = det.ExpenseDeclarationDetailId,
                        ExpId = expenseDeclaration.ExpenseDeclarationId,
                        det.Date,
                        det.ExpenseAmount,
                        det.ExpenseDescription
                    }, transaction);

                }

                foreach (var reqId in requestIDs)
                {
                    await dbConnection.ExecuteAsync($@" update [ANDROID].[dbo].[AU_001_01_ISAVANSTALEP] set [ISAVANS_HESAP_REFERANSID] = {expenseDeclaration.ExpenseDeclarationId} where [REFERANSID] = {reqId} ", null, transaction);

                }

                transaction.Commit();
                return expenseDeclaration.ExpenseDeclarationId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<ExpenseDeclarationInformationViewModel> GetExpenseDeclarationInformation(int declarationDetailId)
        {
            var result = await dbConnection.QueryFirstOrDefaultAsync<ExpenseDeclarationInformationViewModel>(@"select MASRAF_ACIKLAMASI as ExpenseDescription, MASRAF_TUTARI as ExpenseAmount from [ANDROID].[dbo].[AU_001_01_ISAVANSHESAPDETAY]
            where REFERANSID = @id", new { id = declarationDetailId });

            return result;
        }


        public async Task<int> InsertExpenseAdvanceForceDeclaration(ExpenseAdvanceRequest request, IEnumerable<ExpenseDeclarationDetail> details, User user)
        {
            try
            {
                var personnelData = await GetEFlowPersonnel(user.UserPID);
                var reqData = await GetTabelEFlowData("AU_001_01_FORSMAJOR", request.ExpenseAdvanceRequestId);
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();

                foreach (var det in details)
                {
                    await dbConnection.ExecuteAsync($@"
                   INSERT INTO [ANDROID].[dbo].[AU_001_01_FORSMAJORHESAP]
                  (
	                   [REFERANSID]
                      ,[TALEP_NO]
                      ,[PERSONEL_KODU]
                      ,[PERSONEL_ADI]
                      ,[KASA_KODU]
                      ,[MASRAF_ACIKLAMASI]
                      ,[MASRAF_TUTARI]
                      ,[MASRAF_TARIHI]
                      ,[EFLOW_DURUMU]
                  )
                  VALUES 
                  (
  	                   @REFERANSID
                      ,@TALEP_NO
                      ,@PERSONEL_KODU
                      ,@PERSONEL_ADI
                      ,@KASA_KODU
                      ,@MASRAF_ACIKLAMASI
                      ,@MASRAF_TUTARI
                      ,@MASRAF_TARIHI
                      ,0
                  )
                ", new
                    {
                        REFERANSID = request.ExpenseAdvanceRequestId,
                        TALEP_NO = reqData.TALEP_NO,
                        PERSONEL_KODU = personnelData.FIN_KODU,
                        PERSONEL_ADI = personnelData.PERSONEL_ADI,
                        KASA_KODU = personnelData.KASA_KODU,
                        MASRAF_ACIKLAMASI = det.ExpenseDescription,
                        MASRAF_TUTARI = det.ExpenseAmount,
                        MASRAF_TARIHI = det.Date
                    }, transaction);

                }

                transaction.Commit();
                return request.ExpenseAdvanceRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public async Task<int> InsertSalaryAdvanceRequest(SalaryAdvanceRequest expenseAdvanceRequest, User user)
        {
            try
            {
                string tableName = "AU_001_01_MAASAVANS";
                var personnelData = await GetEFlowPersonnel(user.UserPID);
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
               INSERT INTO [ANDROID].[dbo].[AU_001_01_MAASAVANS]
                   ([REFERANSID]
                   ,[TALEP_TARIHI]
                   ,[TALEPNO]
                   ,[TALEP_EDEN_PERS_KOD]
                   ,[TALEP_EDEN_PERS_ADI]
                   ,[TALEP_ACIKLAMASI]
                   ,[AVANS_TUTARI]
                   ,[TAKSIT_SAYISI]
                   ,[ODEME_KASASI]
                   ,[EFLOW_DURUM])
             VALUES
                   (@RefId,
                   @Date, 
                   @DocNumber, 
                   @UserPID, 
                   @PersonnelName, 
                   @Note, 
                   @Amount, 
                   @PartitionCount,
                   @SafeboxCode,
                   0)
                ", new { RefId = expenseAdvanceRequest.SalaryAdvanceRequestId, Date = DateTime.Now, docNumber, user.UserPID, personnelName = personnelData.PERSONEL_ADI, Amount = expenseAdvanceRequest.RequestAmount, Note = expenseAdvanceRequest.RequestDescription, expenseAdvanceRequest.PartitionCount, SafeboxCode = personnelData.KASA_KODU }, transaction);
                transaction.Commit();
                return expenseAdvanceRequest.SalaryAdvanceRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> InsertEquipmentRequest(EquipmentRequest equipmentRequest, User user)
        {
            try
            {
                string tableName = "AU_001_01_TECHIZAT_SATINALMA";
                string personnelName = await GetPersonnelNameByPersonnelCode(user.UserPID);
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
               INSERT INTO [ANDROID].[dbo].[{tableName}]
           ([EFLOW_DURUM]
           ,[TALEP_EDEN_FINKODU]
           ,[TALEP_NO]
           ,[TALEP_TARIHI]
           ,[TALEP_ACIKLAMASI]
           )
		   VALUES
		   (0
           ,@UserPID
           ,@RefId
           ,GETDATE()
           ,@Note
		   )
                ", new { RefId = equipmentRequest.EquipmentRequestId, UserPID = user.UserPID, Note = equipmentRequest.RequestDescription }, transaction);
                transaction.Commit();
                return equipmentRequest.EquipmentRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> InsertExpenseDeclaration(ExpenseDeclaration expenseDeclaration, User user)
        {
            try
            {
                string tableName = "AU_001_01_ISAVANSHESAP";
                string personnelName = await GetPersonnelNameByPersonnelCode(user.UserPID);
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                       ([REFERANSID]
                       ,[IS_AVANSNO]
                       ,[PERSONEL_KODU]
                       ,[PERSONEL_ADI]
                       ,[AVANS_TUTARI]
                       ,[EFLOW_DURUM])
                 VALUES
                       (@RefId
                       ,@DocNumber
                       ,@UserPID
                       ,@PersonnelName
                       ,@Amount
                       ,0)
                ", new { RefId = expenseDeclaration.ExpenseDeclarationId, Date = DateTime.Now, DocNumber = docNumber, UserPID = user.UserPID, PersonnelName = personnelName, Amount = expenseDeclaration.ExpenseDeclarationDetails.Sum(x => x.ExpenseAmount) }, transaction);


                foreach (var detail in expenseDeclaration.ExpenseDeclarationDetails)
                {
                    await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[AU_001_01_ISAVANSHESAPDETAY]
                     (  [REFERANSID]
                       ,[HESAP_REF]
                       ,[MASRAF_ACIKLAMASI]
                       ,[MASRAF_TUTARI])
                 VALUES
                       (@ExpenseDeclarationDetailId
                       ,@ExpenseDeclarationId
                       ,@ExpenseDescription
                       ,@ExpenseAmount )
                ", detail, transaction);

                }

                transaction.Commit();
                return expenseDeclaration.ExpenseDeclarationId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<IEnumerable<TigerWarehouse>> GetWarehouses()
        {
            return await dbConnection.QueryAsync<TigerWarehouse>($"select NR as WarehouseNumber, NAME as WarehouseName from L_CAPIWHOUSE WHERE FIRMNR = {firmno} ");
        }

        public async Task<string> GetPersonnelNameByPersonnelCode(string personnelPin)
        {
            return (await GetEFlowPersonnel(personnelPin)).PERSONEL_ADI;
        }

        public async Task<EMEKTAS> GetEFlowPersonnel(string personnelPin)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<EMEKTAS>($"SELECT * FROM [ANDROID].[dbo].[AU_001_01_EMEKTAS] WHERE [FIN_KODU] = '{personnelPin}' ");
        }


        public async Task<int> InsertBusinessTripRequest(BusinessTripRequest request, string userPID)
        {
            try
            {
                var userData = await GetEFlowPersonnel(userPID);
                string tableName = "AU_001_01_EZAMIYYETALEP";
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                           ([REFERANSID]
                           ,[TARIH]
                           ,[EZAMIYYE_NO]
                           ,[TALEP_EDEN_PERS_KOD]
                           ,[TALEP_EDEN_PERS_ADI]
                           ,[TALEP_ACIKLAMASI]
                           ,[EZAMIYYE_GUN]
                           ,[KASA_KODU]
                           ,[COST_CONTROL_ONAY]
                           ,[ODEME_DURUMU]
                           ,[EFLOW_DURUM])
                     VALUES
                           (@RefId
                           ,@Date
                           ,@DocNumber
                           ,@UserPID
                           ,@UserDisplayName
                           ,@Note
                           ,@TripDaysCount
                           ,@SafeboxCode
                           ,0
                           ,0
                           ,0
                           )
                ", new
                {
                    RefId = request.BusinessTripRequestId,
                    Date = DateTime.Now,
                    DocNumber = docNumber,
                    UserPID = userData.FIN_KODU,
                    UserDisplayName = userData.PERSONEL_ADI,
                    Note = request.RequestDescription,
                    request.TripDaysCount,
                    SafeboxCode = userData.KASA_KODU
                }, transaction);
                transaction.Commit();
                return request.BusinessTripRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public async Task<int> InsertBusinessTripRequestDetail(BusinessTripDetailDTO dto, string userPID)
        {
            try
            {
                var userData = await GetEFlowPersonnel(userPID);
                string tableName = "AU_001_01_EZAMIYYETALEP";
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                int newID = Convert.ToInt32(docNumber.Replace("EZM-", ""));
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                  (
	                   [REFERANSID]
                      ,[TARIH]
                      ,[EZAMIYYE_NO]
                      ,[TALEP_EDEN_PERS_KOD]
                      ,[TALEP_EDEN_PERS_ADI]
                      ,[TALEP_ACIKLAMASI]
                      ,[KASA_KODU]
                      ,[EFLOW_DURUM]
	                  ,[ADET]
                      ,[BIRIM_FIYAT]
                      ,[TUTAR]
                  )
                  VALUES
                  (
	                  @REFERANSID
                      ,@TARIH
                      ,@EZAMIYYE_NO
                      ,@TALEP_EDEN_PERS_KOD
                      ,@TALEP_EDEN_PERS_ADI
                      ,@TALEP_ACIKLAMASI
                      ,@KASA_KODU
                      ,0
	                  ,@ADET
                      ,@BIRIM_FIYAT
                      ,@TUTAR
                  )
                ", new
                {
                    REFERANSID = newID,
                    TARIH = dto.Date,
                    EZAMIYYE_NO = docNumber,
                    TALEP_EDEN_PERS_KOD = userData.FIN_KODU,
                    TALEP_EDEN_PERS_ADI = userData.PERSONEL_ADI,
                    TALEP_ACIKLAMASI = dto.Note,
                    KASA_KODU = userData.KASA_KODU,
                    ADET = dto.Quantity,
                    BIRIM_FIYAT = dto.Price,
                    TUTAR = dto.Quantity * dto.Price

                }, transaction);
                transaction.Commit();
                return newID;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public async Task<bool> InsertBusinessTripDeclaration(int businessTripRequestId, IEnumerable<BusinessTripDeclarationDetailDTO> details, User user)
        {
            try
            {
                var personnelData = await GetEFlowPersonnel(user.UserPID);
                var tripData = await GetEZAMIYYETALEP(businessTripRequestId);
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                foreach (var detail in details)
                {
                    await dbConnection.ExecuteAsync($@"
                    INSERT INTO [ANDROID].[dbo].[AU_001_01_EZAMIYYEMASRAF]
                       ([REFERANSID]
                       ,[TARIH]
                       ,[EZAMIYYE_NO]
                       ,[TALEP_EDEN_PERS_KOD]
                       ,[TALEP_EDEN_PERS_ADI]
                       ,[EZAMIYYE_ACIKLAMASI]
                       ,[ADET]
                       ,[BIRIM_FIYAT]
                       ,[TUTAR]
                       ,[EFLOW_DURUM]
                       ,[AVANS_KASA_KODU]
                       ,[IMAGE_FILE])
                 VALUES
                       (@REFERANSID,
                        @TARIH, 
                        @EZAMIYYE_NO,
                        @TALEP_EDEN_PERS_KOD,
                        @TALEP_EDEN_PERS_ADI,
                        @EZAMIYYE_ACIKLAMASI,
                        @ADET,
                        @BIRIM_FIYAT,
                        @TUTAR,
                        0,
                        @AVANS_KASA_KODU,
			            @IMAGE_FILE)
                    ", new
                    {
                        REFERANSID = businessTripRequestId,
                        TARIH = detail.Date,
                        EZAMIYYE_NO = tripData.EZAMIYYE_NO,
                        TALEP_EDEN_PERS_KOD = personnelData.FIN_KODU,
                        TALEP_EDEN_PERS_ADI = personnelData.PERSONEL_ADI,
                        EZAMIYYE_ACIKLAMASI = detail.Note,
                        ADET = detail.Quantity,
                        BIRIM_FIYAT = detail.Price,
                        TUTAR = detail.Quantity * detail.Price,
                        AVANS_KASA_KODU = personnelData.KASA_KODU,
                        IMAGE_FILE = detail.Image == null ? null : Convert.FromBase64String(detail.Image)
                    }, transaction);
                }



                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<TigerSTFiche>> GetUncompleteTransferFiches(int[] destIndexes)
        {
            try
            {
                return await dbConnection.QueryAsync<TigerSTFiche>($@"
                select STF.LOGICALREF StockFicheId, STF.FICHENO Ficheno,STF.SOURCEINDEX SourceWarehouse,SRW.NAME SourceWarehouseName,DSW.NAME DestWarehouseName,STF.DESTINDEX DestWarehouse, STF.BRANCH SourceBranch , STF.COMPBRANCH DestBranch, STF.DATE_ Date,
                STF.DOCODE SourceDocumentNumber , STF.CYPHCODE CyphCode , STF.GENEXP1 Note1,STF.GENEXP2 Note2,STF.GENEXP3 Note3,STF.GENEXP4 Note4,
                STF.SPECODE SpeCode, STF.GRPCODE GroupCode, STF.TRCODE TransactionCode, STF.DOCTRACKINGNR DocTrackingNumber,
                STF.CAPIBLOCK_CREADEDDATE CreatedDate, STF.WFStatus
                from LG_{firmno}_01_STFICHE STF
                inner join L_CAPIWHOUSE SRW on STF.SOURCEINDEX = SRW.NR and SRW.FIRMNR = {firmno}
                inner join L_CAPIWHOUSE DSW on STF.DESTINDEX = DSW.NR and DSW.FIRMNR = {firmno}
                WHERE TRCODE = 25 and STF.SPECODE = 'ISLEMDE' and STF.DESTINDEX IN @destIndexes and SRW.AREACODE = 0 
                ", new { destIndexes });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<dynamic>> GeTransferFicheLines(int id)
        {
            try
            {
                return await dbConnection.QueryAsync($@"
                SELECT DISTINCT LN.LOGICALREF LineId, LN.STOCKREF ItemId ,LN.LINETYPE LineType ,LN.STFICHELNNO LineNumber,IT.CODE ItemCode,IT.NAME ItemName,LN.AMOUNT Quantity, 
                UL.CODE UnitCode , LN.TOTAL Total,
                ROUND(LN.PRICE,5) Price, LN.UINFO2 UnitConv,
                MN.CODE MainUnitCode,
                LN.SPECODE SpeCode,SR.CODE SerialCode
                FROM LG_{firmno}_01_STLINE LN 
                LEFT JOIN LG_{firmno}_ITEMS IT ON LN.STOCKREF = IT.LOGICALREF 
                LEFT JOIN LG_{firmno}_UNITSETL UL ON LN.UOMREF = UL.LOGICALREF
                LEFT JOIN LG_{firmno}_UNITSETL MN ON MN.UNITSETREF = UL.UNITSETREF and MN.MAINUNIT = 1 
				OUTER APPLY
				(
					select TOP(1) CODE FROM LG_{firmno}_01_SERILOTN WHERE LOGICALREF IN 
					(select SLREF from LG_{firmno}_01_SLTRANS WHERE STFICHEREF = LN.STFICHEREF and ITEMREF = LN.STOCKREF
					) order by LOGICALREF DESC
				) SR
                WHERE LN.STFICHEREF = {id}  AND LN.IOCODE IN (3,4)      
                ");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<dynamic>> GetBusinessTrips(string userPID)
        {
            return await dbConnection.QueryAsync($@"
                  SELECT [REFERANSID] BusinessTripId
                      ,[TARIH] Date
                      ,[EZAMIYYE_NO] BusinessTripNumber
                      ,[TALEP_ACIKLAMASI] Note
                      ,[KASA_KODU] CashboxCode
                      ,[EFLOW_DURUM] StatusId
	                  ,[ADET] Quantity
                      ,[BIRIM_FIYAT] Price
                      ,[TUTAR] Total
	                  ,STATUS_NAME StatusName
					  , ISNULL(MSR.MSR_TUTAR,0) TotalDeclared
                  FROM [ANDROID].[dbo].[AU_001_01_EZAMIYYETALEP] TLP
                  LEFT JOIN [ANDROID].[dbo].[AU_FLOW_STATUS] STS ON TLP.EFLOW_DURUM = STS.STATUS_ID
				  outer apply
				  (
					SELECT SUM([TUTAR]) MSR_TUTAR FROM [ANDROID].[dbo].[AU_001_01_EZAMIYYEMASRAF] MSR where MSR.REFERANSID = TLP.REFERANSID
				  ) MSR
                  where TARIH > GETDATE()-32 and [TALEP_EDEN_PERS_KOD] = '{userPID}' 
                ");
        }

        public async Task<IEnumerable<dynamic>> GetBusinessTripDeclarations(string userPID)
        {
            return await dbConnection.QueryAsync($@"
              SELECT MSR.[REFERANSID] BusinessTripId
                  ,MSR.[TARIH] Date
                  ,MSR.[EZAMIYYE_NO] BusinessTripNumber
                  ,MSR.EZAMIYYE_ACIKLAMASI Note
                  ,MSR.AVANS_KASA_KODU CashboxCode
                  ,MSR.[EFLOW_DURUM] StatusId
	              ,MSR.[ADET] Quantity
                  ,MSR.[BIRIM_FIYAT] Price
                  ,MSR.[TUTAR] Total
	              ,STS.STATUS_NAME StatusName
				  ,TLP.TARIH RequestDate
				  ,TLP.TUTAR RequestTotal
				  ,TLP.TALEP_ACIKLAMASI RequestDescription
              FROM [ANDROID].[dbo].[AU_001_01_EZAMIYYEMASRAF] MSR
              LEFT JOIN [ANDROID].[dbo].[AU_FLOW_STATUS] STS ON MSR.EFLOW_DURUM = STS.STATUS_ID
			  LEFT JOIN [ANDROID].[dbo].[AU_001_01_EZAMIYYETALEP] TLP ON MSR.REFERANSID = TLP.REFERANSID
              where MSR.TARIH > GETDATE()-32 and MSR.[TALEP_EDEN_PERS_KOD] = '{userPID}' 
                ");
        }

        public async Task<BusinessTripDeclarationDetailNewDTO> GetBusinessTripDeclarationDetails(string userPID, BusinessTripDetailsRequest request)
        {
            var query = @"
SELECT 
    TLP.TUTAR RequestTotal,
    TLP.TALEP_ACIKLAMASI RequestDescription
FROM [ANDROID].[dbo].[AU_001_01_EZAMIYYEMASRAF] MSR
LEFT JOIN [ANDROID].[dbo].[AU_FLOW_STATUS] STS ON MSR.EFLOW_DURUM = STS.STATUS_ID
LEFT JOIN [ANDROID].[dbo].[AU_001_01_EZAMIYYETALEP] TLP ON MSR.REFERANSID = TLP.REFERANSID
WHERE MSR.TARIH > GETDATE() - 32
AND MSR.TALEP_EDEN_PERS_KOD = @userPID
AND MSR.REFERANSID = @BusinessTripId
AND MSR.TARIH = @Date
AND MSR.EZAMIYYE_NO = @BusinessTripNumber
AND MSR.EZAMIYYE_ACIKLAMASI = @Note
AND MSR.AVANS_KASA_KODU = @CashboxCode
AND MSR.EFLOW_DURUM = @StatusId
AND MSR.ADET = @Quantity
AND MSR.BIRIM_FIYAT = @Price
AND MSR.TUTAR = @Total
AND STS.STATUS_NAME = N'Təsdiqləndi'
AND TLP.TARIH = @RequestDate
AND TLP.TUTAR = @RequestTotal
AND TLP.TALEP_ACIKLAMASI = @RequestDescription";

            var result = await dbConnection.QueryFirstOrDefaultAsync<BusinessTripDeclarationDetailNewDTO>(query, new
            {
                userPID = userPID,
                BusinessTripId = request.BusinessTripId,
                Date = request.Date,
                BusinessTripNumber = request.BusinessTripNumber,
                Note = request.Note,
                CashboxCode = request.CashboxCode,
                StatusId = request.StatusId,
                Quantity = request.Quantity,
                Price = request.Price,
                Total = request.Total,
                RequestDate = request.RequestDate,
                RequestTotal = request.RequestTotal,
                RequestDescription = request.RequestDescription
            });

            return result;
        }
      
        public async Task<dynamic> GetEZAMIYYETALEP(int REFERANSID)
        {
            return await dbConnection.QueryFirstOrDefaultAsync($@"select * from [ANDROID].[dbo].AU_001_01_EZAMIYYETALEP where REFERANSID = {REFERANSID}");
        }

        public async Task<dynamic> GetTabelEFlowData(string tableName, int REFERANSID)
        {
            return await dbConnection.QueryFirstOrDefaultAsync($@"select * from [ANDROID].[dbo].[{tableName}] where REFERANSID = {REFERANSID}");
        }

        public async Task<object> GetTransferFiche(BusinessTripRequest request, string userPID)
        {
            try
            {
                var userData = await GetEFlowPersonnel(userPID);
                string tableName = "AU_001_01_EZAMIYYETALEP";
                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                string docNumber = await GetNewNumber(tableName, transaction);
                await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[{tableName}]
                           ([REFERANSID]
                           ,[TARIH]
                           ,[EZAMIYYE_NO]
                           ,[TALEP_EDEN_PERS_KOD]
                           ,[TALEP_EDEN_PERS_ADI]
                           ,[TALEP_ACIKLAMASI]
                           ,[EZAMIYYE_GUN]
                           ,[KASA_KODU]
                           ,[COST_CONTROL_ONAY]
                           ,[ODEME_DURUMU]
                           ,[EFLOW_DURUM])
                     VALUES
                           (@RefId
                           ,@Date
                           ,@DocNumber
                           ,@UserPID
                           ,@UserDisplayName
                           ,@Note
                           ,@TripDaysCount
                           ,@SafeboxCode
                           ,0
                           ,0
                           ,0
                           )
                ", new
                {
                    RefId = request.BusinessTripRequestId,
                    Date = DateTime.Now,
                    DocNumber = docNumber,
                    UserPID = userData.FIN_KODU,
                    UserDisplayName = userData.PERSONEL_ADI,
                    Note = request.RequestDescription,
                    request.TripDaysCount,
                    SafeboxCode = userData.KASA_KODU
                }, transaction);
                transaction.Commit();
                return request.BusinessTripRequestId;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public async Task<IEnumerable<TigerSTFiche>> GetQuickTransferFiches(int[] destIndexes)
        {
            try
            {
                return await dbConnection.QueryAsync<TigerSTFiche>($@"
                select STF.LOGICALREF StockFicheId, STF.FICHENO Ficheno,STF.SOURCEINDEX SourceWarehouse,SRW.NAME SourceWarehouseName,DSW.NAME DestWarehouseName,STF.DESTINDEX DestWarehouse, STF.BRANCH SourceBranch , STF.COMPBRANCH DestBranch, STF.DATE_ Date,
                STF.DOCODE SourceDocumentNumber , STF.CYPHCODE CyphCode , STF.GENEXP1 Note1,STF.GENEXP2 Note2,STF.GENEXP3 Note3,STF.GENEXP4 Note4,
                STF.SPECODE SpeCode, STF.GRPCODE GroupCode, STF.TRCODE TransactionCode, STF.DOCTRACKINGNR DocTrackingNumber,
                STF.CAPIBLOCK_CREADEDDATE CreatedDate, STF.WFStatus
                from LG_{firmno}_01_STFICHE STF
                inner join L_CAPIWHOUSE SRW on STF.SOURCEINDEX = SRW.NR and SRW.FIRMNR = {firmno}
                inner join L_CAPIWHOUSE DSW on STF.DESTINDEX = DSW.NR and DSW.FIRMNR = {firmno}
                WHERE TRCODE = 25 and STF.SPECODE = 'ISLEMDE' and STF.DESTINDEX IN @destIndexes  and SRW.AREACODE = 1 
                ", new { destIndexes });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal async Task ApproveQuickTransferFiche(int stficheId)
        {
            await ChangeStFiche(stficheId, "KABUL", 0);
        }

        internal async Task ChangeStFiche(int stficheId, string speCode = null, short? status = null, string note4 = null)
        {
            dbConnection.Open();
            var transaction = dbConnection.BeginTransaction();

            List<string> upd = new();
            if (speCode != null)
                upd.Add($" SPECODE = '{speCode}' ");
            if (status != null)
            {
                upd.Add($" STATUS = {status} ");
                upd.Add($" DESTSTATUS = {status} ");
            }
            if (note4 != null)
                upd.Add($" GENEXP4 = '{note4}' ");

            await dbConnection.ExecuteAsync($@"
               UPDATE LG_{firmno}_01_STFICHE SET {string.Join(",", upd)} WHERE LOGICALREF = {stficheId}
            ", null, transaction);

            if (status != null)
            {
                await dbConnection.ExecuteAsync($@"
                UPDATE LG_{firmno}_01_STLINE SET STATUS = {status},DESTSTATUS = {status} WHERE STFICHEREF = {stficheId} ", null, transaction);

                await dbConnection.ExecuteAsync($@"
                UPDATE LG_{firmno}_01_SLTRANS SET STATUS = {status} WHERE STFICHEREF = {stficheId} ", null, transaction);
            }

            transaction.Commit();
            dbConnection.Close();
        }

        internal async Task ControlStFiche(int stficheId, string userName)
        {
            string n4 = userName + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            await ChangeStFiche(stficheId, speCode: "MALIYEDE", note4: n4);
        }

        public async Task<IEnumerable<TigerSTFiche>> GetStockFiches(TigerStockFicheFilter filter)
        {
            var parameters = new DynamicParameters();
            string sql_query = $@" 
                select STF.LOGICALREF StockFicheId, STF.FICHENO Ficheno,STF.SOURCEINDEX SourceWarehouse,STF.DESTINDEX DestWarehouse, STF.BRANCH SourceBranch , STF.COMPBRANCH DestBranch, STF.DATE_ Date,
                STF.DOCODE SourceDocumentNumber ,CL.CODE CardCode ,CL.DEFINITION_ CardName, SL.CODE SalesmanCode ,
                STF.REPORTRATE ReportRate, STF.CYPHCODE CyphCode , STF.TRADINGGRP TradingGroup,
                ISNULL(PP.CODE,'') PaymentCode, CL.ADDR1 Address,SL.DEFINITION_ SalesmanName,
                STF.SALESMANREF SalesmanId, STF.GENEXP1 Note1,STF.GENEXP2 Note2,STF.GENEXP3 Note3,STF.GENEXP4 Note4,
                STF.GROSSTOTAL GrossTotal, STF.NETTOTAL NetTotal, STF.TOTALDISCOUNTS TotalDiscounts,
                STF.SPECODE SpeCode, STF.GRPCODE GroupCode, STF.TRCODE TransactionCode, STF.DOCTRACKINGNR DocTrackingNumber,
                STF.CAPIBLOCK_CREADEDDATE CreatedDate, STF.WFStatus
                from LG_{firmno}_01_STFICHE STF 
                LEFT JOIN LG_{firmno}_CLCARD CL   ON STF.CLIENTREF = CL.LOGICALREF
                LEFT JOIN LG_SLSMAN SL   ON STF.SALESMANREF = SL.LOGICALREF
                LEFT JOIN LG_{firmno}_PAYPLANS PP ON STF.PAYDEFREF = PP.LOGICALREF
                WHERE 0=0 ";

            if (filter.StockFicheId != null)
            {
                sql_query += " AND STF.LOGICALREF = @Id ";
                parameters.Add(name: "@Id", value: filter.StockFicheId);
            }
            if (filter.TransactionCodes != null && filter.TransactionCodes.Count() > 0)
            {
                sql_query += " AND STF.TRCODE IN @TRCS ";
                parameters.Add(name: "@TRCS", value: filter.TransactionCodes, direction: ParameterDirection.Input);
            }
            if (filter.BeginDate != null)
            {
                sql_query += " AND STF.DATE_ >= @BEGINDATE ";
                parameters.Add(name: "@BEGINDATE", value: filter.BeginDate.Value.Date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            }
            if (filter.EndDate != null)
            {
                sql_query += " AND STF.DATE_ <= @ENDDATE ";
                parameters.Add(name: "@ENDDATE", value: filter.EndDate.Value.Date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            }
            if (filter.Printed != null)
            {
                string mark = filter.Printed.Value ? " > 0 " : " = 0 ";
                sql_query += " AND STF.PRINTCNT " + mark;
            }
            if (filter.SourceWarehouses != null && filter.SourceWarehouses.Count() > 0)
            {
                sql_query += " AND STF.SOURCEINDEX IN @WHS ";
                parameters.Add(name: "@WHS", value: filter.SourceWarehouses, direction: ParameterDirection.Input);
            }
            if (filter.DestWarehouses != null && filter.DestWarehouses.Count() > 0)
            {
                sql_query += " AND STF.DESTINDEX IN @WHD ";
                parameters.Add(name: "@WHD", value: filter.DestWarehouses, direction: ParameterDirection.Input);
            }
            if (filter.SourceBranches != null && filter.SourceBranches.Count() > 0)
            {
                sql_query += " AND STF.BRANCH IN @BCS ";
                parameters.Add(name: "@BCS", value: filter.SourceBranches, direction: ParameterDirection.Input);
            }
            if (filter.DestBranches != null && filter.DestBranches.Count() > 0)
            {
                sql_query += " AND STF.COMPBRANCH IN @BCD ";
                parameters.Add(name: "@BCD", value: filter.DestBranches, direction: ParameterDirection.Input);
            }
            if (filter.CyphCode != null)
            {
                sql_query += " AND STF.CYPHCODE = @CYPHCODE ";
                parameters.Add(name: "@CYPHCODE", value: filter.CyphCode);
            }
            if (filter.SpeCode != null)
            {
                sql_query += " AND STF.SPECODE = @SPECODE ";
                parameters.Add(name: "@SPECODE", value: filter.SpeCode);
            }
            if (filter.Note1 != null)
            {
                sql_query += " AND STF.GENEXP1 = @Note1 ";
                parameters.Add(name: "@Note1", value: filter.Note1);
            }
            if (filter.Note2 != null)
            {
                sql_query += " AND STF.GENEXP2 = @Note2 ";
                parameters.Add(name: "@Note2", value: filter.Note2);
            }
            if (filter.Note3 != null)
            {
                sql_query += " AND STF.GENEXP3 = @Note3 ";
                parameters.Add(name: "@Note3", value: filter.Note3);
            }
            if (filter.Note4 != null)
            {
                sql_query += " AND STF.GENEXP4 = @Note4 ";
                parameters.Add(name: "@Note4", value: filter.Note4);
            }
            if (filter.WFStatus != null)
            {
                sql_query += " AND STF.WFStatus = @WFStatus ";
                parameters.Add(name: "@WFStatus", value: filter.WFStatus);
            }
            if (filter.Cancelled != null)
            {
                sql_query += " AND STF.Cancelled = @Cancelled ";
                parameters.Add(name: "@Cancelled", value: filter.Cancelled);
            }
            if (filter.DocTrackingNumber != null)
            {
                sql_query += " AND STF.DOCTRACKINGNR = @DocTrackingNumber ";
                parameters.Add(name: "@DocTrackingNumber", value: filter.DocTrackingNumber);
            }
            if (filter.Ficheno != null)
            {
                sql_query += " AND STF.FICHENO = @FICHENO ";
                parameters.Add(name: "@FICHENO", value: filter.Ficheno);
            }
            if (filter.SourceDocumentNumber != null)
            {
                sql_query += " AND STF.DOCODE = @DOCODE ";
                parameters.Add(name: "@DOCODE", value: filter.SourceDocumentNumber);
            }
            sql_query += " AND STF.DESTINDEX NOT IN (299) ";
            return await dbConnection.QueryAsync<TigerSTFiche>(sql_query, parameters, null, 0);
        }

        public async Task<IEnumerable<dynamic>> GetSalaryPayrolls(string userPID)
        {
            try
            {
                // DUSTURUN ACIQLAMASI:
                // DB-de il ve ay saxlanilir, GETDATE() ise bize indiki tarixi verir. onlari YYYYMM formatina saldqida,
                // //bu ayi ve kecen ayi gorsun deye onlarin ferqi mes 202205-202205 = 0 ve 202205-202204 = 1, lakin yanvar ayinda 202201-202112 = 89
                // // NETICEDE 3 value ola biler 0 , 1 , 89
                // :)
                return await dbConnection.QueryAsync($@"
                select 
                REFERANSID as Id,
                YIL as Year, 
                AY as Month, 
                CALISAN_HR_KOD as UserPID,
                CALISAN_AD PersonName,
                MAAS_TUTAR SalaryTotal,
                ODEME_KASA CashCode
                from ANDROID..AU_001_01_MAASODEME1 
where 
CALISAN_HR_KOD = '{userPID.Replace("'", "")}'
and EFLOW_DURUM = 1 
and ODEME_DURUM = 0
and Convert(int,LEFT(Convert(varchar,GETDATE(),112),6)) - Convert(int,LEFT(Convert(varchar,Convert(date,Convert(varchar,YIL) +'-'+ Convert(varchar,AY) + '-1'),112),6))
                IN (0,1,89)
                ");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> GetSalaryPayrollById(int id)
        {
            try
            {

                return await dbConnection.QueryFirstOrDefaultAsync($@"
                select 
                REFERANSID as Id,
                YIL as Year, 
                AY as Month, 
                CALISAN_HR_KOD as UserPID,
                CALISAN_AD PersonName,
                MAAS_TUTAR SalaryTotal,
                ODEME_KASA CashCode
                from ANDROID..AU_001_01_MAASODEME1 
                where REFERANSID = {id}
                ");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        readonly string cashOutsQuery = $@"
                Select TOP(30)
                REFERANSID as Id,
                TALEP_TARIHI as RequestDate,
                CARI_KODU as CardCode,
                CARI_ADI as CardName,
                TALEP_TUTARI as Amount,
                TALEP_ACIKLAMASI as Note,
                STS.STATUS_NAME StatusName,
                ONAY_DURUMU Approved,
                TALEP_EDEN_PERS_ADI as PersonName
                FROM [ANDROID].[dbo].[AU_001_01_KASACIKIS] KC
                LEFT JOIN [ANDROID].[dbo].[AU_FLOW_STATUS] STS ON KC.EFLOW_DURUM = STS.STATUS_ID
                WHERE 0=0 
                ";


        public async Task<IEnumerable<dynamic>> GetCashouts(string userPID)
        {
            return await dbConnection.QueryAsync($@" {cashOutsQuery} AND TALEP_EDEN_PERS_KOD = '{userPID}' ORDER BY REFERANSID DESC ");
        }

        public async Task<dynamic> GetCashout(int id)
        {
            return await dbConnection.QueryFirstOrDefaultAsync($@" {cashOutsQuery} AND REFERANSID = {id} ");
        }

        public async Task<int> InsertCashout(CashoutDTO dto, User user, TigerCard cardData)
        {
            try
            {
                var userData = await GetEFlowPersonnel(user.UserPID);

                dbConnection.Open();
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                int inserted = await dbConnection.ExecuteAsync($@"
                INSERT INTO [ANDROID].[dbo].[AU_001_01_KASACIKIS]
                           (TALEP_TARIHI,
	                        TALEP_EDEN_PERS_KOD,
	                        TALEP_EDEN_PERS_ADI,
	                        CARI_KODU,
	                        CARI_ADI,
	                        TALEP_TUTARI,
	                        TALEP_ACIKLAMASI,
	                        ONAY_DURUMU,
	                        EFLOW_DURUM)
                     VALUES
                           (@TALEP_TARIHI,
	                        @TALEP_EDEN_PERS_KOD,
	                        @TALEP_EDEN_PERS_ADI,
	                        @CARI_KODU,
	                        @CARI_ADI,
	                        @TALEP_TUTARI,
	                        @TALEP_ACIKLAMASI,
	                        @ONAY_DURUMU,
	                        @EFLOW_DURUM)
                ", new
                {
                    TALEP_TARIHI = DateTime.Now,
                    TALEP_EDEN_PERS_KOD = userData.FIN_KODU,
                    TALEP_EDEN_PERS_ADI = userData.PERSONEL_ADI,
                    CARI_KODU = cardData.CardCode,
                    CARI_ADI = cardData.CardName,
                    TALEP_TUTARI = dto.Amount,
                    TALEP_ACIKLAMASI = dto.Note,
                    ONAY_DURUMU = false,
                    EFLOW_DURUM = (short)0
                }, transaction);
                transaction.Commit();
                return inserted;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<IEnumerable<TigerSalesman>> GetSalesmen()
        {
            return await dbConnection.QueryAsync<TigerSalesman>(@$"SELECT [LOGICALREF] as SalesmanId, [CODE] as SalesmanCode, [DEFINITION_] as SalesmanName FROM [dbo].[LG_SLSMAN] WHERE FIRMNR={firmno}");
        }

        public async Task<IEnumerable<SalesmanReportDto>> GetSalesmenReport(int year, int month, string brnachNumbers)
        {
            var str = @$"select SP.SalesmanCode, TOT.DEFINITION_ AS SalesmanName, SP.Year_ AS Year, SP.Month_ AS Month,  SP.PlannedSale, 
                ISNULL(TOT.ActualSale,0) ActualSale, ROUND(ISNULL(TOT.ActualSale,0)/SP.PlannedSale * 100.00,0) PercentageValue, 
                TOT.ActualSaleOther, 
                ISNULL(SP.PlannedSaleOther,0) AS PlannedSaleOther, 
                ISNULL(ROUND(TOT.ActualSaleOther/NULLIF(SP.PlannedSaleOther,0) * 100.00,0),0) PercentageValueOther
                from SuperfonWorks..SalesmanPlan SP 
                INNER JOIN
                (
				    SELECT        
				    ROUND(sum(IIF(ISNULL(XT.KATEGORI,0) NOT IN(7, 20),
						    CASE WHEN LINE.DISCPER = 0 THEN LINE.LINENET ELSE LINE.TOTAL / 100 * (100 - LINE.DISCPER) 
							    END * 
						    CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END,0)), 2) AS ActualSale,
				    ISNULL(ROUND(sum(IIF(XT.KATEGORI=7 OR XT.KATEGORI=20,
						    CASE WHEN LINE.DISCPER = 0 THEN LINE.LINENET ELSE LINE.TOTAL / 100 * (100 - LINE.DISCPER) 
							    END * 
						    CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END,0)),2),0) AS ActualSaleOther,
				     SLS.DEFINITION_, SLS.CODE, YEAR(INV.DATE_) Year_, MONTH(INV.DATE_) Month_
				    FROM TIGER3DB..LG_{firmno}_01_INVOICE AS INV WITH (NOLOCK) 
				    inner join TIGER3DB..LG_SLSMAN SLS  WITH (NOLOCK)  ON INV.SALESMANREF = SLS.LOGICALREF AND SLS.FIRMNR = {firmno}
				    INNER JOIN TIGER3DB..LG_{firmno}_01_STFICHE FICHE WITH (NOLOCK) ON  FICHE.INVOICEREF=INV.LOGICALREF
				    INNER JOIN TIGER3DB..LG_{firmno}_01_STLINE LINE WITH (NOLOCK) ON LINE.STFICHEREF = FICHE.LOGICALREF
				    INNER JOIN TIGER3DB..LG_{firmno}_ITEMS ITM WITH (NOLOCK) ON LINE.STOCKREF=ITM.LOGICALREF
				    LEFT JOIN TIGER3DB..LG_XT1001_{firmno} XT WITH (NOLOCK) ON XT.PARLOGREF = ITM.LOGICALREF
				    WHERE INV.BRANCH IN({brnachNumbers}) AND MONTH(INV.DATE_)=@month and year(INV.DATE_)=@year AND INV.TRCODE IN (2, 3, 7, 8) AND INV.CANCELLED = 0
				    group by SLS.DEFINITION_, SLS.CODE, YEAR(INV.DATE_), MONTH(INV.DATE_)
                ) TOT ON TOT.CODE = SP.SalesmanCode and TOT.Year_ = SP.Year_ and TOT.Month_ = SP.Month_
            Where SP.Year_=@year and SP.Month_=@month ORDER BY TOT.DEFINITION_";
            return await dbConnection.QueryAsync<SalesmanReportDto>(str, param: new { year, month });
        }

        public async Task<IEnumerable<BranchPlanReportDto>> GetBranchPlanReport(int year, int month)
        {
            var str = @$"
            select SP.BranchNumber, BR.NAME AS BranchName,  SP.PlannedSale, 
                ISNULL(TOT.ActualSale,0) ActualSale, ROUND(ISNULL(TOT.ActualSale,0)/SP.PlannedSale * 100.00,0) PercentageValue
                from SuperfonWorks..BranchPlan SP 
                INNER JOIN
                (
				    SELECT INV.BRANCH,
				    ROUND(sum(IIF(ISNULL(XT.KATEGORI,0) NOT IN(7, 20),
						    CASE WHEN LINE.DISCPER = 0 THEN LINE.LINENET ELSE LINE.TOTAL / 100 * (100 - LINE.DISCPER) 
							    END * 
						    CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END,0)), 2) AS ActualSale
				    
				    FROM TIGER3DB..LG_{firmno}_01_STLINE LINE WITH (NOLOCK)
					INNER JOIN TIGER3DB..LG_{firmno}_01_INVOICE AS INV WITH (NOLOCK) ON LINE.INVOICEREF = INV.LOGICALREF
				    LEFT JOIN TIGER3DB..LG_XT1001_{firmno} XT WITH (NOLOCK) ON XT.PARLOGREF = LINE.STOCKREF
				    WHERE LINE.MONTH_ = {month} and LINE.YEAR_= {year} AND INV.TRCODE IN (2, 3, 7, 8) AND INV.CANCELLED = 0
                    and XT.MEHSUL_TIPI IN (0,3) and LINE.STOCKREF != 0
				    group by INV.BRANCH, YEAR(INV.DATE_), MONTH(INV.DATE_)
                ) TOT ON TOT.BRANCH = SP.BranchNumber
				INNER JOIN TIGER3DB..L_CAPIDIV BR ON TOT.BRANCH = BR.NR AND BR.FIRMNR = {firmno}
            Where SP.Year_={year} and SP.Month_={month} ORDER BY BR.NAME
            ";
            return await dbConnection.QueryAsync<BranchPlanReportDto>(str, param: new { year, month }, commandTimeout: 0);
        }

        public async Task<IEnumerable<dynamic>> GetBranchCategorySalesCustom(int year, int month)
        {   
            var str = @$"
            SELECT BRANCH,KATEGORI_BP,Total Total FROM (
	            SELECT INV.BRANCH,XT.MEHSUL_TIPI,
	            CASE
	            WHEN XT.MEHSUL_TIPI = 0 THEN N'Aksesuar'
	            WHEN XT.MEHSUL_TIPI = 1 THEN N'E/H'
	            WHEN XT.MEHSUL_TIPI = 2 THEN N'Telefon-Nömrə'
	            WHEN XT.MEHSUL_TIPI = 3 THEN N'Kiçik Məişət'
	            END AS KATEGORI_BP,
	            sum(
	            CASE WHEN LINE.DISCPER = 0 THEN LINE.LINENET ELSE LINE.TOTAL / 100 * (100 - LINE.DISCPER) 
	            END * 
	            CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END) AS Total
	            FROM TIGER3DB..LG_{firmno}_01_STLINE LINE WITH (NOLOCK)
	            LEFT OUTER JOIN TIGER3DB..LG_{firmno}_01_INVOICE AS INV WITH (NOLOCK) ON LINE.INVOICEREF = INV.LOGICALREF
	            LEFT OUTER JOIN TIGER3DB..LG_XT1001_{firmno} XT WITH (NOLOCK) ON XT.PARLOGREF = LINE.STOCKREF
	            WHERE LINE.MONTH_ = {month} and LINE.YEAR_={year} 
	            AND INV.TRCODE IN (2, 3, 7, 8)
	            AND INV.CANCELLED = 0 AND LINE.STOCKREF <> 0
	            group by XT.MEHSUL_TIPI, INV.BRANCH
            ) T 
            where 
            T.KATEGORI_BP !=''
            ";
            return await dbConnection.QueryAsync(str, param: new { year, month }, commandTimeout: 0);
        }

        public async Task<IEnumerable<dynamic>> GetSalesmanCategorySalesByBranch(int year, int month, int branchNumber)
        {
            var str = @$"
           	SELECT SL.CODE SalesmanCode,SL.DEFINITION_ SalesmanName,XT.KATEGORI,
	        ROUND(sum(
			        CASE WHEN LINE.DISCPER = 0 THEN LINE.LINENET ELSE LINE.TOTAL / 100 * (100 - LINE.DISCPER) 
				        END * 
			        CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END),0) AS Total,
			SUM(LINE.AMOUNT * (CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END)) Quantity
	        FROM TIGER3DB..LG_{firmno}_01_STLINE LINE WITH (NOLOCK)
	        INNER JOIN TIGER3DB..LG_{firmno}_01_INVOICE AS INV WITH (NOLOCK) ON LINE.INVOICEREF = INV.LOGICALREF
	        INNER JOIN TIGER3DB..LG_SLSMAN SL ON LINE.SALESMANREF = SL.LOGICALREF
	        LEFT JOIN TIGER3DB..LG_XT1001_{firmno} XT WITH (NOLOCK) ON XT.PARLOGREF = LINE.STOCKREF
	        WHERE LINE.MONTH_ = {month} and LINE.YEAR_= {year} AND INV.TRCODE IN (2, 3, 7, 8) AND INV.CANCELLED = 0
	        and INV.BRANCH = {branchNumber} AND LINE.STOCKREF <> 0
	        group by XT.KATEGORI,SL.CODE,SL.DEFINITION_
	        order by SL.DEFINITION_, XT.KATEGORI
            ";
            return await dbConnection.QueryAsync(str, param: new { year, month });
        }

        public async Task<IEnumerable<dynamic>> GetSalesmanReportDetail(int year, int month, string salesmanCode, string brnachNumbers)
        {
            string query = @$"SELECT SLS.DEFINITION_ SalesmanName, YEAR(INV.DATE_) Year, MONTH(INV.DATE_) Month, DIV.NAME AS BranchName, 
					 ROUND(SUM(
							CASE WHEN LINE.DISCPER = 0 THEN LINE.LINENET ELSE LINE.TOTAL / 100 * (100 - LINE.DISCPER) END * 
						    CASE WHEN LINE.IOCODE = 1 THEN - 1 WHEN LINE.IOCODE = 4 THEN 1 END), 2) ActualSale
				    FROM TIGER3DB..LG_{firmno}_01_INVOICE AS INV WITH (NOLOCK)
				    inner join TIGER3DB..LG_SLSMAN SLS  WITH (NOLOCK)  ON INV.SALESMANREF = SLS.LOGICALREF AND SLS.FIRMNR={firmno}
					INNER JOIN L_CAPIDIV DIV ON DIV.NR=INV.BRANCH AND DIV.FIRMNR={firmno}
				    INNER JOIN TIGER3DB..LG_{firmno}_01_STFICHE FICHE WITH (NOLOCK) ON  FICHE.INVOICEREF=INV.LOGICALREF
				    INNER JOIN TIGER3DB..LG_{firmno}_01_STLINE LINE WITH (NOLOCK) ON LINE.STFICHEREF = FICHE.LOGICALREF
				    INNER JOIN TIGER3DB..LG_{firmno}_ITEMS ITM WITH (NOLOCK) ON LINE.STOCKREF=ITM.LOGICALREF
				    WHERE INV.BRANCH IN({brnachNumbers}) AND SLS.CODE=@salesmanCode AND year(INV.DATE_)={year} AND MONTH(INV.DATE_)={month}
					AND INV.TRCODE IN (2, 3, 7, 8) AND INV.CANCELLED=0 
					group by SLS.CODE, YEAR(INV.DATE_), MONTH(INV.DATE_), DIV.NAME, SLS.DEFINITION_";
            return await dbConnection.QueryAsync<dynamic>(sql: query, param: new { salesmanCode });
        }

        public async Task<IEnumerable<dynamic>> GetProductStockByBranches()
        {
            string query = $@"SELECT
    ITEM.CYPHCODE ,
    ITEM.CODE as Sku,
    SUM(ONHAND) Stock,
    Round(ISNULL(LG_PRC.PRICE, 0), 2) Price,
    ITEM.NAME as SkuName,
    ITEM.SPECODE AS SpeCode
FROM
    LV_{firmno}_01_STINVTOT TT
INNER JOIN
    TIGER3DB..L_CAPIDIV DIV ON TT.INVENNO = DIV.NR AND DIV.FIRMNR = {firmno}
INNER JOIN
    TIGER3DB..LG_{firmno}_ITEMS ITEM ON ITEM.LOGICALREF = TT.STOCKREF
OUTER APPLY
    (
        SELECT TOP(1) PRICE
        FROM TIGER3DB..LG_{firmno}_PRCLIST
        WHERE GETDATE() BETWEEN BEGDATE AND ENDDATE
        AND CLSPECODE5 = 'SN.PERAKEN'
        AND ACTIVE = 0
        AND BRANCH = -1
        AND CARDREF = ITEM.LOGICALREF
        ORDER BY BEGDATE DESC
    ) LG_PRC
WHERE
    (CAST(DIV.NR AS NVARCHAR(MAX)) LIKE '5__'
    OR CAST(DIV.NR AS NVARCHAR(MAX)) LIKE '6__')
AND CAST(DIV.NR AS NVARCHAR(MAX))!='555'
GROUP BY
     ITEM.CYPHCODE , ITEM.CODE, LG_PRC.PRICE, ITEM.NAME, ITEM.SPECODE
HAVING
    CASE
        WHEN ITEM.CYPHCODE IN ('1', '2', '3', '4', '5') THEN 0
        ELSE 1
    END = 1";
            var result = await dbConnection.QueryAsync<dynamic>(sql: query);
            return result;
        }

        static readonly Dictionary<int, string> dc = new()
        {
            [0] = "Seçim Yok",
            [1] = "E/H HOUSING",
            [2] = "E/H LCD",
            [3] = "E/H PARTS",
            [4] = "E/H TOUCH",
            [5] = "E/H USTA LEVAZIMATI",
            [8] = "D/T SES GUCLENDIRICILER",
            [9] = "D/T HOLDER",
            [10] = "BATTERY",
            [11] = "H/A FERDI BAXIM",
            [12] = "F/P FASHION",
            [13] = "T/A CHARGERS",
            [14] = "F/P SCREEN PROTECTORS",
            [15] = "T/A KABEL VE OTURUCULER",
            [16] = "D/T LIFE CREATIVE",
            [18] = "T/A QULAQLIQLAR",
            [19] = "D/T YADDAS",
            [20] = "T/N TELEFON-NOMRE",
            [21] = "D/T DEVELOP",
            [22] = "D/T SAAT",
            [23] = "D/T RETAIL",
            [24] = "PACKING",
            [25] = "TECHIZAT",
            [26] = "H/A KICIK MEISET"
        };

        public static string GetCategoryName(int id)
        {
            if (dc.ContainsKey(id))
                return dc[id];
            return $"Kateqoriya id - {id}";
        }
    }
}
