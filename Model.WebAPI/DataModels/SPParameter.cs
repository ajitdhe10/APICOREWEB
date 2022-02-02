using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.WebAPI.DataModels
{
    public class SPParameter
    {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
        public SqlDbType ParameterDataType { get; set; }
    }
}
