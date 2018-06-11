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
        public ObjectField(IEnumerable<AbstractField> array)
            : base(string.Empty, array)
        {
        }

        public override string ToJSONFormat()
        {
            return "{" + string.Join(",", ReadOnlyArrayFieldValue.Select(c => c.ToJSONFormat())) + "}";
        }
    }
}
