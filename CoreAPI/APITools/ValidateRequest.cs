using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace CoreAPI.APITools
{
    public static class ValidateRequest
    {
        public static bool validateSQL(object objObject)
        {
            if (objObject == null)
                return false;

            string values = "";
            Type typModelCls = objObject.GetType();
            foreach (PropertyInfo prop in typModelCls.GetProperties())
            {
                values = prop.GetValue(objObject, null) + "";

                values = values.Replace(" ", "");
                values = values.Replace(".dbo", "");
                if (values.ToString().Trim().ToUpper().Contains("SELECT*") || values.ToString().Trim().ToUpper().Contains("SELECT*FROM")
                        || values.ToString().Trim().ToUpper().Contains("TRUNCATETABLE") || values.ToString().Trim().ToUpper().Contains("DROPTALBE")
                        || values.ToString().Trim().ToUpper().Contains("DELETEFROM") || values.ToString().Trim().ToUpper().Contains("INSERTINTO")
                        || values.ToString().Trim().ToUpper().Contains("ALTERTABLE") || values.ToString().Trim().ToUpper().Contains("ALTERDATABASE")
                        )
                {
                    return false;
                }
            }


            return true;
        }

        public static bool validateSQLText(string values)
        {
            values = values.Replace(" ", "");
            values = values.Replace(".dbo", "");
            if (values.ToString().Trim().ToUpper().Contains("SELECT*") || values.ToString().Trim().ToUpper().Contains("SELECT*FROM")
                    || values.ToString().Trim().ToUpper().Contains("TRUNCATETABLE") || values.ToString().Trim().ToUpper().Contains("DROPTALBE")
                    || values.ToString().Trim().ToUpper().Contains("DELETEFROM") || values.ToString().Trim().ToUpper().Contains("INSERTINTO")
                        || values.ToString().Trim().ToUpper().Contains("ALTERTABLE") || values.ToString().Trim().ToUpper().Contains("ALTERDATABASE")
                    )
            {
                return false;
            }

            return true;
        }
    }
}
