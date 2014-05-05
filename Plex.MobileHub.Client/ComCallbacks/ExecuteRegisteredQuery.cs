﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MobileHubClient.PMH;
using System.Threading;
using MobileHubClient.Logs;
using MobileHubClient.Data;
using MobileHubClient.Data.Types;
using System.Globalization;
using Oracle.DataAccess.Client;

namespace MobileHubClient.ComCallbacks
{
    public static partial class Functions
    {
        public static RQryResult ExecuteRegisteredQuery(string dbCode, int queryId, DateTime? time = null)
        {
            RQryResult Result = new RQryResult();
            try
            {
                using (var Con = ClientDbConnectionFactory.Instance.GetConnection(dbCode, true))
                {
                    try
                    {
                        LogManager.Instance.Add("Executing Query " + queryId);
                        string Query = (string)Con.Query("select pmh.QueryGetSql(:a,:b) as SQL from dual", queryId, time)[0, 0];
                        LogManager.Instance.Add(Query);

                        var Start = DateTime.Now;
                        Result = Con.Query(Query).ToQueryResult();
                        Result.StartTimeStamp = Start;
                        Result.CompletionTimeStamp = DateTime.Now;

                        var Cs = Result.Columns.ToList();
                        int AIndex = Cs.FindIndex(p => p.ColumnName.Equals("PMH$_DBACTION", StringComparison.OrdinalIgnoreCase));
                        int RVIndex = Cs.FindIndex(p => p.ColumnName.Equals("PMH$_ROW_VERSION", StringComparison.OrdinalIgnoreCase));

                        for (int i = 0; i < Result.Rows.Length; i++)
                        {
                            Result.Rows[i].DBAction = Convert.ToInt32(Result.Rows[i].Values[AIndex]);
                            Result.Rows[i].RowVersion = Convert.ToInt32(Result.Rows[i].Values[RVIndex]);
                        }
                        Result.Response = Result.Rows.Length;
                        Result.Msg = "Success";
                    }
                    catch (OracleException e)
                    {
                        Result.Msg = e.Message;
                        Result.Response = e.HResult;
                        Result.DBErrorCode = e.ErrorCode;
                        Result.DBErrorMessage = e.ToString();
                        throw;
                    }
                    catch (Exception e)
                    {
                        Result.Msg = e.Message;
                        Result.Response = e.HResult;
                        throw;
                    }
                }
            }
            catch(Exception e)
            {
                LogManager.Instance.Add(e);
            }
            return Result;
        }
    }
}