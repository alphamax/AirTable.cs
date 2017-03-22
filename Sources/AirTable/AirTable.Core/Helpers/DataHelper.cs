using AirTable.Core.Data;
using AirTable.Core.Data.Field;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Helpers
{
    public class DataHelper
    {
        /// <summary>
        /// Create a field from a JSON content.
        /// Kind of a Factory.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static AbstractField CreateField(JToken json)
        {
            if (json.Type == JTokenType.Property)
            {
                var property = (json as JProperty);
                if (property.Value.Type == JTokenType.Array)
                {
                    return new ArrayField(property.Name, (property.Value as JArray).Select(c => c.Value<string>()).ToArray());
                }
                else if (property.Value.Type == JTokenType.String)
                {
                    return new StringField(property.Name, property.Value.Value<string>());
                }
                else if (property.Value.Type == JTokenType.Integer)
                {
                    return new IntegerField(property.Name, property.Value.Value<int>());
                }
                else if (property.Value.Type == JTokenType.Boolean)
                {
                    return new BooleanField(property.Name, property.Value.Value<bool>());
                }
                else
                {
                    throw new Exception("Unable to find data type.");
                }
            }
            else
            {
                throw new Exception("Unable to serialize in a field.");
            }
        }

        /// <summary>
        /// Just a single point of UrlEncode. 
        /// In case of something may change...
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(string value)
        {
            return WebUtility.UrlEncode(value);
        }
    }
}
