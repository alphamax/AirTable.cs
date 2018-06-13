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
            AbstractField result;
            if (json.Type == JTokenType.Property)
            {
                var property = (json as JProperty);
                if (property.Value.Type == JTokenType.Array)
                {
                    result = new ArrayField(property.Name, (property.Value as JArray).Select(c => CreateField(c)).ToArray());
                }
                else if (property.Value.Type == JTokenType.String)
                {
                    result = new StringField(property.Name, property.Value.Value<string>());
                }
                else if (property.Value.Type == JTokenType.Integer)
                {
                    result = new IntegerField(property.Name, property.Value.Value<int>());
                }
                else if (property.Value.Type == JTokenType.Boolean)
                {
                    result = new BooleanField(property.Name, property.Value.Value<bool>());
                }
                else if (property.Value.Type == JTokenType.Object)
                {
                    var arrayOfData = json.ToArray();
                    result = new ObjectField(property.Name, arrayOfData.Select(c => CreateField(c)));
                }
                else
                {
                    throw new Exception("Unable to find data type.");
                }
            }
            else if (json.Count() > 0)
            {
                var arrayOfData = json.ToArray();
                result = new ArrayField(arrayOfData.Select(c => CreateField(c)));
            }
            else
            {
                throw new Exception("Unable to serialize in a field.");
            }
            return result;
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
