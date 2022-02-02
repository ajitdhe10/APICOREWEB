
using DataAccessLayer.WebAPI.CommonInfo;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrizonCoreAPI.APITools
{
    [AttributeUsage(AttributeTargets.All)]
    public class CoreAPIActionAuthorization : Attribute, IAuthorizationFilter
    {
        private readonly int _roleId;
        public CoreAPIActionAuthorization(int RoleId)
        {
            this._roleId = RoleId;
        }
        //private readonly ICommonUserInfoDA _ICommonUserInfoDA;

        //public PowerUpAPIActionAuthorization(ICommonUserInfoDA ICommonUserInfoDA)
        //{
        //    this._ICommonUserInfoDA = ICommonUserInfoDA;
        //}
        //private readonly IConfiguration _IConfiguration;

        //public PowerUpAPIActionAuthorization(IConfiguration IConfiguration)
        //{
        //    _IConfiguration = IConfiguration;
        //}

        public async void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {

                Microsoft.Extensions.Primitives.StringValues Authorization;

                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out Authorization);

                var _token = Authorization.FirstOrDefault();

                if (_token == null)
                {
                    filterContext.HttpContext.Response.Headers.Clear();
                    filterContext.HttpContext.Response.Headers.Add("Authorization", _token);
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Invalid Token"
                        },
                    };
                }
                else
                {
                    int UserId = 0;
                    using (CommonUserInfoDA objCommonUserInfoDA = new CommonUserInfoDA(Program.ConfigurationData))
                    {

                        string SecretKey = filterContext.HttpContext.Request.GetHeader("SecretKey");
                        string TokenCounter = filterContext.HttpContext.Request.GetHeader("TokenCounter");
                        string ApplicationId = filterContext.HttpContext.Request.GetHeader("ApplicationId"); ;

                        UserId = await objCommonUserInfoDA.GetUserId(_token, SecretKey, TokenCounter, ApplicationId);
                    }


                    if (UserId > 0)
                    {
                        bool ValidateRole = true;
                        using (CommonUserInfoDA objCommonUserInfoDA = new CommonUserInfoDA(Program.ConfigurationData))
                        {
                            ValidateRole = await objCommonUserInfoDA.ValidateRoleId(UserId);
                        }
                        if (ValidateRole)
                        {
                            filterContext.HttpContext.Response.Headers.Clear();
                            filterContext.HttpContext.Response.Headers.Add("Authorization", _token);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Clear();
                            filterContext.HttpContext.Response.Headers.Add("Authorization", _token);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Token"
                                },
                            };
                        }
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Headers.Clear();
                        filterContext.HttpContext.Response.Headers.Add("Authorization", _token);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        filterContext.HttpContext.Response.HttpContext.Features
                                .Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        filterContext.Result = new JsonResult("NotAuthorized")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Not Authorized"
                            },
                        };
                    }
                }
            }
        }
    }
}
