using Model.WebAPI.Common;
using Model.WebAPI.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.WebAPI.LoginLayer
{
    public interface ILoginDA
    {
        Task<AccessTokenMdl> validateDealerLogin(LoginMdl objLoginModel);

    }
}
