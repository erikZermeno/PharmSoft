﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simisoft.DAL
{
    public class DataAccess
    {
        #region Singleton
        private static volatile DataAccess instance = null;
        private static readonly object padlock = new object();
        public static string conString = ""; 

        private DataAccess() { }

        public static DataAccess Instance()
        {
            if (instance == null)
                lock (padlock)
                    if (instance == null)
                        instance = new DataAccess();
            return instance;
        }
        #endregion

        #region CRUD
        public T QuerySingle<T>(string query)
        {
            using (var con = new SqlConnection(conString))
                return con.QuerySingle<T>(query, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public T QuerySingle<T>(string query, DynamicParameters dynamicParameters)
        {
            using (var con = new SqlConnection(conString))
                return con.QuerySingleOrDefault<T>(query, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public T QuerySingleOrDefault<T>(string query)
        {
            using (var con = new SqlConnection(conString))
                return con.QuerySingleOrDefault<T>(query, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public T QuerySingleOrDefault<T>(string query, DynamicParameters dynamicParameters)
        {
            using (var con = new SqlConnection(conString))
                return con.QuerySingleOrDefault<T>(query, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public string QueryString(string query)
        {
            using (var con = new SqlConnection(conString))
                return con.QuerySingle<string>(query, commandType: CommandType.StoredProcedure, commandTimeout: 300);

        }

        public string QueryString(string query, DynamicParameters dynamicParameters)
        {
            using (var con = new SqlConnection(conString))
                return con.QuerySingle<string>(query, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public IEnumerable<T3> QueryMultiMapping<T1, T2, T3>(string query, Func<T1, T2, T3> map, string splitOn)
        {
            using (var con = new SqlConnection(conString))
                return con.Query<T1, T2, T3>(query, map, splitOn: splitOn, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public IEnumerable<T3> QueryMultiMapping<T1, T2, T3>(string query, DynamicParameters dynamicParameters, Func<T1, T2, T3> map, string splitOn)
        {
            using (var con = new SqlConnection(conString))
                return con.Query<T1, T2, T3>(query, map, dynamicParameters, splitOn: splitOn, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public IEnumerable<T> Query<T>(string query)
        {
            using (var con = new SqlConnection(conString))
                return con.Query<T>(query, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public IEnumerable<T> Query<T>(string query, DynamicParameters dynamicParameters)
        {
            using (var con = new SqlConnection(conString))
                return con.Query<T>(query, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public int Insert(string query, DynamicParameters dynamicParameters, string fieldName)
        {
            using (var con = new SqlConnection(conString))
                return (int)((IDictionary<string, object>)con.QuerySingle(query, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 300))[fieldName];
        }

        public int Last(string query, DynamicParameters parametros, string fieldName)
        {
            using (var con = new SqlConnection(conString))
                return (int)((IDictionary<string, object>)con.QuerySingle(query, parametros, commandType: CommandType.StoredProcedure, commandTimeout: 300))[fieldName];
        }

        public int Execute(string query)
        {
            using (var con = new SqlConnection(conString))
                return con.Execute(query, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        public int Execute(string query, DynamicParameters dynamicParameters)
        {
            using (var con = new SqlConnection(conString))
                return con.Execute(query, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        }

        #endregion
    }
}
