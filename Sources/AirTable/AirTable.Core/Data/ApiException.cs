using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data
{
    /// <summary>
    /// API Exception
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Error API number (can be found in documentation).
        /// </summary>
        public int ApiErrorNumber { get; set; }
        /// <summary>
        /// Token to identify error type.
        /// </summary>
        public string ApiErrorType { get; set; }
        /// <summary>
        /// More readable error value.
        /// </summary>
        public string ApiErrorMessage { get; set; }

        public ApiException(int errorNumber, string json)
        {
            var error = JsonConvert.DeserializeObject(json) as JObject;
            var errorContent = error["error"];
            if (errorContent != null)
            {
                ApiErrorNumber = errorNumber;
                ApiErrorType = errorContent["type"].Value<string>();
                ApiErrorMessage = errorContent["message"].Value<string>();
            }
        }

        public override string ToString()
        {
            return ApiErrorNumber + " - " + ApiErrorType + " - " + ApiErrorMessage;
        }
    }
}
