using DataAccessLayer.WebAPI.DBManagement;
using Microsoft.Extensions.Configuration;
using Model.WebAPI.Common;
using Model.WebAPI.DataModels; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccessLayer.WebAPI.CommonInfo
{
    public class CommonUserInfoDA : ICommonUserInfoDA, IDisposable
    {

        private IConfiguration _iConfiguration;

        //private readonly IProcessEncryption _IProcessEncryption;

        public CommonUserInfoDA(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            //_IProcessEncryption = IProcessEncryption;
        }

        public void Dispose()
        {

        }

        public async Task<int> GetUserId(string _token, string SecretKey, string TokenCounter, string ApplicationId)
        {
            try
            {
                string spName = "Comm_AccessToken_UserId";
                //IList<SqlParameter> objSqlParameterList = new List<SqlParameter>();
                DataSet objDT = new DataSet();
                // objSqlParameterList.Add(new SqlParameter("@JwtAccessToken", _token));
                // objSqlParameterList.Add(new SqlParameter("@SecretKey", SecretKey));
                // objSqlParameterList.Add(new SqlParameter("@TokenCounter", TokenCounter));
                // objSqlParameterList.Add(new SqlParameter("@ApplicationId", ApplicationId));
                IList<SPParameter> objSPParameterList = new List<SPParameter>();
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@JwtAccessToken",
                    ParameterValue = _token,
                    ParameterDataType = SqlDbType.NVarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@SecretKey",
                    ParameterValue = SecretKey,
                    ParameterDataType = SqlDbType.NVarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@TokenCounter",
                    ParameterValue = TokenCounter,
                    ParameterDataType = SqlDbType.Int
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@ApplicationId",
                    ParameterValue = ApplicationId,
                    ParameterDataType = SqlDbType.Int
                });


                using (DataConnection objDataConnection = new DataConnection(_iConfiguration, "PowerUpConnection"))
                {
                    objDT = objDataConnection.getDataSet(spName, objSPParameterList, "UserDetails");
                }

                int UserId = 0;
                if (objDT.Tables["UserDetails"] != null)
                    if (objDT.Tables["UserDetails"].Rows.Count > 0)
                        UserId = int.Parse(objDT.Tables["UserDetails"].Rows[0]["UserId"].ToString());

                return await Task.FromResult(UserId);
            }
            catch (Exception ex)
            {
                //  CommonDetails.addAPIExceptionDetails("UserAccountController", "processTransaction",
                //       JsonConvert.SerializeObject("objLoginModel :{" + objLoginModel + "}"),
                //  "", ex.ToString(), objLoginModel.UserID);
            }

            return await Task.FromResult(0);
        }

        public async Task<bool> ValidateRoleId(int UserId)
        {
            AccessTokenMdl objAccessToken = new AccessTokenMdl();
            try
            {
                return true;


                //string spName = "ECO_User_Login_Validate";
                //IList<SqlParameter> objSqlParameterList = new List<SqlParameter>();
                //DataSet objDT = new DataSet();
                //objSqlParameterList.Add(new SqlParameter("@AccessToken", _token));

                //using (DataConnection objDataConnection = new DataConnection(_iConfiguration, "PowerUpConnection"))
                //{
                //    objDT = objDataConnection.getDataSet(spName, objSqlParameterList);
                //}

                //int UserId = 0;
                //if (objDT.Tables["UserDetails"] != null)
                //    if (objDT.Tables["UserDetails"].Rows.Count > 0)
                //        UserId = int.Parse(objDT.Tables["UserDetails"].Rows[0]["Id"].ToString());

            }
            catch (Exception ex)
            {
                //  CommonDetails.addAPIExceptionDetails("UserAccountController", "processTransaction",
                //       JsonConvert.SerializeObject("objLoginModel :{" + objLoginModel + "}"),
                //  "", ex.ToString(), objLoginModel.UserID);
            }

            return await Task.FromResult(false); ;
        }
    }
}
