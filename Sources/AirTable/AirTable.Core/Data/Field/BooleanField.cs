using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Field
{
    public class BooleanField : AbstractSimpleDataField<bool>
    {
        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public BooleanField(string name, bool value)
            : base(name, value)
        {
        }

        public override string ToJSONFormat()
        {
            return "\"" + FieldName + "\":" + (FieldValue ? "true" : "false");
        }
    }
}
