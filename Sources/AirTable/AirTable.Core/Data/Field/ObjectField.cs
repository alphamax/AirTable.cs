using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Field
{
    public class ObjectField : ArrayField
    {
        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public ObjectField(string propertyName, IEnumerable<AbstractField> array)
            : base(propertyName, array)
        {
        }

        public override string ToJSONFormat()
        {
            if (ReadOnlyArrayFieldValue.Count == 1 && ReadOnlyArrayFieldValue[0] is ArrayField)
            {
                return "\"" + FieldName + "\":" + "{" + (string.Join(",", (ReadOnlyArrayFieldValue.First() as ArrayField).ReadOnlyArrayFieldValue.Select(c => c.ToJSONFormat())) + "}");
            }
            else
            {
                return "\"" + FieldName + "\":" + "{" + string.Join(",", ReadOnlyArrayFieldValue.Select(c => c.ToJSONFormat())) + "}";
            }
        }
    }
}
