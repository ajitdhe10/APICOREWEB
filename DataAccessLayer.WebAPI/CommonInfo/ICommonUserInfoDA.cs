using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.WebAPI.CommonInfo
{
    public interface ICommonUserInfoDA
    {
        Task<int> GetUserId(string _token, string SecretKey, string TokenCounter, string ApplicationId);
        Task<bool> ValidateRoleId(int UserId);
    }
}
