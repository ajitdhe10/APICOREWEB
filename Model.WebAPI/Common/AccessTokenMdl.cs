using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.WebAPI.Common
{
    public class AccessTokenMdl
    {
        public string JwtAccessToken { get; set; }
        public string SecretKey { get; set; }
        public int TokenCounter { get; set; }
        public DateTime TokenExpire { get; set; }
        public string Message { get; set; }
        public int ApplicationId { get; set; }
    }
}
