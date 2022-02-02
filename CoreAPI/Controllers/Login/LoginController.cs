using CoreAPI.APITools;
using DataAccessLayer.WebAPI.LoginLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.WebAPI.Common;
using Model.WebAPI.Login;

namespace CoreAPI.Controllers.Login
{
    [Route("api/react")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginDA _ILoginDA;
        private IConfiguration _iConfiguration;
        public LoginController(IConfiguration iConfiguration, ILoginDA ILoginDA)
        {
            _iConfiguration = iConfiguration;
            _ILoginDA = ILoginDA;
        }

        [HttpPost("validatedealer")]
        //   [ValidateAntiForgeryToken]
        public async Task<ActionResult<object>> LoginUser(LoginMdl objLoginModel)
        {
            if (!ValidateRequest.validateSQL(objLoginModel))
                return await Task.FromResult(this.BadRequest("InValid data, SQL injection detected."));

            objLoginModel.ApplicationId = await AuthInformation.getApplicationId(Request);
            AccessTokenMdl objAccessToken = await _ILoginDA.validateDealerLogin(objLoginModel);

            return await Task.FromResult(this.Ok(objAccessToken));
        }

    }
}
