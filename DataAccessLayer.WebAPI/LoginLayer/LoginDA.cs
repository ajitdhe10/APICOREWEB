using CommonLibrary.WebAPI.Encryption;
using DataAccessLayer.WebAPI.DBManagement;
using DataAccessLayer.WebAPI.ExceptionLayer;
using Microsoft.Extensions.Configuration;
using Model.WebAPI.Common;
using Model.WebAPI.DataModels;
using Model.WebAPI.Login;
using Model.WebAPI.UserInformation;
using Newtonsoft.Json;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
namespace DataAccessLayer.WebAPI.LoginLayer
{
    public class LoginDA : ILoginDA
    {
        private IConfiguration _iConfiguration; 
        private readonly IExceptionDA _IExceptionDA; 
        private readonly IProcessEncryption _IProcessEncryption;
        public LoginDA(IConfiguration iConfiguration, IProcessEncryption IProcessEncryption, IExceptionDA IExceptionDA)
        {
            _iConfiguration = iConfiguration;
            _IProcessEncryption = IProcessEncryption;
            _IExceptionDA = IExceptionDA;
        }

        public async Task<AccessTokenMdl> validateDealerLogin(LoginMdl objLoginModel)
        {
            //  LogFileExcception.WriteLog("validateDealerLogin"); 
            AccessTokenMdl objAccessToken = new AccessTokenMdl();
            try
            {
                string PasswordD = _IProcessEncryption.subEncryption(objLoginModel.Password.ToString().Trim(), EncryptionKeys.dealerPasswordKey.ToString(), true);
                string spName = "Dealer_User_Login_Validate";
                // IList<SqlParameter> objSqlParameterList = new List<SqlParameter>();
                DataSet objDT = new DataSet();
                //objSqlParameterList.Add(new SqlParameter("@EmailAddress", objLoginModel.EmailAddress));
                //objSqlParameterList.Add(new SqlParameter("@Password", PasswordD));
                //objSqlParameterList.Add(new SqlParameter("@RememberMe", objLoginModel.RememberMe));

                IList<SPParameter> objSPParameterList = new List<SPParameter>();
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@EmailAddress",
                    ParameterValue = objLoginModel.EmailAddress,
                    ParameterDataType = SqlDbType.NVarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@Password",
                    ParameterValue = PasswordD,
                    ParameterDataType = SqlDbType.NVarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@RememberMe",
                    ParameterValue = objLoginModel.RememberMe,
                    ParameterDataType = SqlDbType.Bit
                });


                using (DataConnection objDataConnection = new DataConnection(_iConfiguration, "APIDBConnection"))
                {
                    objDT = objDataConnection.getDataSet(spName, objSPParameterList, "UserDetails");
                }
                objLoginModel.UserId = 0;
                if (objDT.Tables["UserDetails"] != null)
                {
                    if (objDT.Tables["UserDetails"].Rows.Count > 0)
                    {
                        objLoginModel.UserId = int.Parse(objDT.Tables["UserDetails"].Rows[0]["Id"].ToString());



                        objAccessToken.Message = "Invalid Credentials";


                        if (objLoginModel.UserId > 0)
                        {

                            if (objDT.Tables["UserDetails"].Rows[0]["AccountConfirmed"] != null)
                                if (objDT.Tables["UserDetails"].Rows[0]["AccountConfirmed"].ToString().Trim().Length > 0)
                                    objLoginModel.AccountConfirmed = Convert.ToBoolean(objDT.Tables["UserDetails"].Rows[0]["AccountConfirmed"].ToString());

                            if (objDT.Tables["UserDetails"].Rows[0]["AccountExpiry"] != null)
                            {
                                if (objDT.Tables["UserDetails"].Rows[0]["AccountExpiry"].ToString().Trim().Length > 0)
                                    objLoginModel.AccountExpiry = Convert.ToDateTime(objDT.Tables["UserDetails"].Rows[0]["AccountExpiry"].ToString());
                                else
                                    objLoginModel.AccountExpiry = DateTime.Now.AddDays(-1);
                            }
                            else
                                objLoginModel.AccountExpiry = DateTime.Now.AddDays(-1);


                            if (!objLoginModel.AccountConfirmed)
                            {
                                objAccessToken.Message = "Please confirm email address";
                            }
                            else if (objLoginModel.AccountExpiry < DateTime.Now)
                            {
                                objAccessToken.Message = "Account expired";
                            }
                            else
                            {
                                objAccessToken.JwtAccessToken = _IProcessEncryption.GenerateToken(objLoginModel.EmailAddress, 150);
                                objAccessToken.SecretKey = _IProcessEncryption.GenerateToken(50);
                                Random a = new Random();
                                int Counter = a.Next(1000, 9999);
                                objAccessToken.TokenCounter = Counter;
                                objAccessToken.Message = "LoginSuccess";
                                objAccessToken.TokenExpire = DateTime.Now.AddDays(3);

                                await addUserAccessToken(objLoginModel.UserId, objLoginModel.ApplicationId, objAccessToken);

                                return await Task.FromResult(objAccessToken);
                            }

                            return await Task.FromResult(objAccessToken);
                        }
                        else
                        {
                            if (objLoginModel.UserId == -69)
                            {
                                objAccessToken.Message = "InValidEmail";

                            }
                            else if (objLoginModel.UserId == -59)
                            {
                                objAccessToken.Message = "InValidPassword";

                            }
                            else if (objLoginModel.UserId == -99)
                            {
                                objAccessToken.Message = "PendingActivation";

                            }
                            else
                            {
                                objAccessToken.Message = "Invalid";

                            }

                        }
                    }
                    else
                        objLoginModel.UserId = 0;
                }
                else
                    objLoginModel.UserId = 0;

            }
            catch (Exception ex)
            {
                //    LogFileExcception.WriteLog(ex.ToString()); ;

                objAccessToken.Message = ex.ToString();
                return await Task.FromResult(objAccessToken); ;
                await _IExceptionDA.addExceptionDetails(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                       JsonConvert.SerializeObject("objLoginModel :{" + objLoginModel + "}"),
                                         "", ex.ToString(), objLoginModel.UserId, objLoginModel.ApplicationId);

            }


          return  await Task.FromResult(objAccessToken); ;
             
        }
        public async Task<User> addUserAccessToken(int UserId, int ApplicationId, AccessTokenMdl objAccessToken)
        {
            User objUser = new User();
            try
            {
                string spName = "Comm_AccessToken_Add";
                DataSet objDT = new DataSet();
                //objSqlParameterList.Add(new SqlParameter("@UserId", UserId));
                //objSqlParameterList.Add(new SqlParameter("@ApplicationId", ApplicationId));
                //objSqlParameterList.Add(new SqlParameter("@JwtAccessToken", objAccessToken.JwtAccessToken));
                //objSqlParameterList.Add(new SqlParameter("@SecretKey", objAccessToken.SecretKey));
                //objSqlParameterList.Add(new SqlParameter("@TokenCounter", objAccessToken.TokenCounter));


                IList<SPParameter> objSPParameterList = new List<SPParameter>();
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@UserId",
                    ParameterValue = UserId.ToString(),
                    ParameterDataType = SqlDbType.Int
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@ApplicationId",
                    ParameterValue = ApplicationId.ToString(),
                    ParameterDataType = SqlDbType.Int
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@JwtAccessToken",
                    ParameterValue = objAccessToken.JwtAccessToken.ToString(),
                    ParameterDataType = SqlDbType.NVarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@SecretKey",
                    ParameterValue = objAccessToken.SecretKey.ToString(),
                    ParameterDataType = SqlDbType.NVarChar
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@TokenCounter",
                    ParameterValue = objAccessToken.TokenCounter.ToString(),
                    ParameterDataType = SqlDbType.Int
                });
                objSPParameterList.Add(new SPParameter()
                {
                    ParameterName = "@TokenExpiry",
                    ParameterValue = objAccessToken.TokenExpire,//.ToString("yyyy-MM-dd HH:mm:ss")
                    ParameterDataType = SqlDbType.DateTime
                }); ; ; ;

                using (DataConnection objDataConnection = new DataConnection(_iConfiguration, "APIDBConnection"))
                {
                    objDT = objDataConnection.getDataSet(spName, objSPParameterList, "UserDetails");
                    //  objDT = objDataConnection.getDataSet(spName, objSqlParameterList, "UserDetails");
                }
            }
            catch (Exception ex)
            {
                await _IExceptionDA.addExceptionDetails(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                       JsonConvert.SerializeObject("objAccessToken :{" + objAccessToken + "}"),
                                         "", ex.ToString(), UserId, objAccessToken.ApplicationId);
            }

            return await Task.FromResult(objUser); ;
        }

    }
}
