using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Helpers
{
    /// <summary>
    /// Simple request parameter.
    /// </summary>
    public class RequestParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public RequestParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
