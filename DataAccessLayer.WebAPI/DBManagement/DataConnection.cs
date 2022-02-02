using Microsoft.Extensions.Configuration;
using Model.WebAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer.WebAPI.DBManagement
{
    public class DataConnection : IDisposable
    {
        private string _connectionString;
        public DataConnection(IConfiguration iconfiguration, string dbConnectionName)
        {
            _connectionString = iconfiguration.GetConnectionString(dbConnectionName);
        }

        public void Dispose()
        {

        }

        //public DataSet getDataSet(string spName, IList<SqlParameter> cmdParameters, string tableName)
        //{ 
        //    DataSet objDS = new DataSet();
        //    using (SqlConnection _con = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand _cmd = new SqlCommand(spName, _con))
        //        {
        //            foreach (var parameter in cmdParameters)
        //            {
        //                _cmd.Parameters.Add(parameter);
        //            }

        //            using (var _dap = new SqlDataAdapter(_cmd))
        //            {
        //                _cmd.CommandType = CommandType.StoredProcedure;
        //                _dap.Fill(objDS, tableName);
        //            }

        //            //_con.Open();
        //            //_dap.Fill(objDS, tableName);
        //            //_con.Close();
        //        }
        //    }

        //    return objDS;
        //}
        public DataSet getDataSet(string spName, IList<SPParameter> cmdParameters, string tableName)
        {
            DataSet objDS = new DataSet();
            using (SqlConnection _con = new SqlConnection(_connectionString))
            {
                using (SqlCommand _cmd = new SqlCommand(spName, _con))
                {
                    foreach (var parameter in cmdParameters)
                    {
                        // _cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                        _cmd.Parameters.Add(parameter.ParameterName, parameter.ParameterDataType).Value = (object)parameter.ParameterValue;
                        // _cmd.Parameters.Add(parameter);
                    }

                    using (var _dap = new SqlDataAdapter(_cmd))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _dap.Fill(objDS, tableName);
                    }
                }
            }

            return objDS;
        }

    }
}
