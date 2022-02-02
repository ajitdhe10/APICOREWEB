using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.WebAPI.Encryption
{
    public interface IProcessEncryption
    {
        string subEncryption(string strTextToEncrypt, string strKeyValue, bool IsHasing);
        string subDecryprtion(string strTextToDecrypt, string strKeyValue, bool IsHasing);

        string GenerateToken(int length);
        string GenerateToken(string username, int expireMinutes = 20);

    }
}
