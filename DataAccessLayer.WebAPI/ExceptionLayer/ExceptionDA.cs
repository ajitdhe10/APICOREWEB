using DataAccessLayer.WebAPI.DBManagement; 
using Microsoft.Extensions.Configuration;
using Model.WebAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.WebAPI.ExceptionLayer
{
    public class ExceptionDA : IExceptionDA, IDisposable
    {

        private IConfiguration _iConfiguration;

        //private readonly IProcessEncryption _IProcessEncryption;

        public ExceptionDA(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            //_IProcessEncryption = IProcessEncryption;
        }

        public void Dispose()
        {

        }

        public async Task<string> addExceptionDetails(string ClassName, string MethodName, string RequestData,
                                                     string Message, string ExceptionDetails, int UserId, int ApplicationId)
        {
            try
            {


                string spName = "Comm_Exception_Add";
                DataSet objDT = new DataSet();
                IList<SPParameter> objSPParameterList = new List<SPParameter>();
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@ClassName",
                    ParameterValue = ClassName,
                    ParameterDataType = SqlDbType.VarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@MethodName",
                    ParameterValue = MethodName,
                    ParameterDataType = SqlDbType.VarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@RequestData",
                    ParameterValue = RequestData,
                    ParameterDataType = SqlDbType.VarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@Message",
                    ParameterValue = Message,
                    ParameterDataType = SqlDbType.VarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@ExceptionDetails",
                    ParameterValue = ExceptionDetails,
                    ParameterDataType = SqlDbType.VarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@UserId",
                    ParameterValue = UserId,
                    ParameterDataType = SqlDbType.Int
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@ApplicationId",
                    ParameterValue = ApplicationId,
                    ParameterDataType = SqlDbType.Int
                });
                using (DataConnection objDataConnection = new DataConnection(_iConfiguration, "APIDBConnection"))
                {
                    objDT = objDataConnection.getDataSet(spName, objSPParameterList, "UserDetails");
                }

            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult("Success");
        }
    }
}
