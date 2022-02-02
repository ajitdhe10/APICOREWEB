using Microsoft.AspNetCore.Http;
using Model.WebAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OrizonCoreAPI.APITools
{
    
    public static class AuthInformation
    {
        public static async Task<AccessTokenMdl> getAccessToken(HttpRequest Request)
        {
            Microsoft.Extensions.Primitives.StringValues Authorization;

            Request.Headers.TryGetValue("Authorization", out Authorization);

            var _token = Authorization.FirstOrDefault();
            string SecretKey = Request.GetHeader("SecretKey"); ; 
            string TokenCounter = Request.GetHeader("TokenCounter");
            string ApplicationId = Request.GetHeader("ApplicationId"); ;
            AccessTokenMdl objAccessToken = new AccessTokenMdl();
            objAccessToken.JwtAccessToken = _token;
            objAccessToken.SecretKey = SecretKey;
            try
            {
                objAccessToken.TokenCounter = int.Parse(TokenCounter);
            }
            catch (Exception)
            {
            }
            try
            {
                objAccessToken.ApplicationId = int.Parse(ApplicationId);
            }
            catch (Exception)
            {
            }

            return await Task.FromResult(objAccessToken);
        }
        public static async Task<int> getApplicationId(HttpRequest Request)
        {

            string ApplicationId = Request.GetHeader("ApplicationId");
            try
            {
                return int.Parse(ApplicationId);
            }
            catch (Exception)
            {
            }

            return await Task.FromResult(0);
        }
    }
}
