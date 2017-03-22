using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Field
{
    public class ArrayField : AbstractField
    {
        /// <summary>
        /// Field container
        /// </summary>
        private List<string> ArrayFieldValue { get; set; }

        /// <summary>
        /// Kind of ReadOnly version.
        /// AsReadOnly not available on PCL.
        /// </summary>
        public List<string> ReadOnlyArrayFieldValue { get { return ArrayFieldValue.ToList(); } }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        public ArrayField(string name, IEnumerable<string> array)
            : base(name)
        {
            ArrayFieldValue = new List<string>(array);
        }

        /// <summary>
        /// Add value to list.
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(string value)
        {
            ArrayFieldValue.Add(value);
            HasBeenModified = true;
        }

        /// <summary>
        /// Remove value to list.
        /// </summary>
        /// <param name="value"></param>
        public void RemoveValue(string value)
        {
            ArrayFieldValue.Remove(value);
            HasBeenModified = true;
        }

        /// <summary>
        /// Clear all containing values.
        /// </summary>
        public void ClearValue()
        {
            ArrayFieldValue.Clear();
            HasBeenModified = true;
        }

        public override string ToJSONFormat()
        {
            return "\"" + FieldName + "\":[" + string.Join(",", ArrayFieldValue.Select(c => "\"" + c + "\"")) + "]";
        }
    }
}
