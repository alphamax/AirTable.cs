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
        private List<AbstractField> ArrayFieldValue { get; set; }

        /// <summary>
        /// Kind of ReadOnly version.
        /// AsReadOnly not available on PCL.
        /// </summary>
        public List<AbstractField> ReadOnlyArrayFieldValue { get { return ArrayFieldValue.ToList(); } }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        public ArrayField(string name, IEnumerable<string> array)
            : base(name)
        {
            ArrayFieldValue = new List<AbstractField>(array.Select(c => new StringField(c)));
        }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        public ArrayField(string name, IEnumerable<AbstractField> array)
            : base(name)
        {
            ArrayFieldValue = new List<AbstractField>(array);
        }

        /// <summary>
        /// Add value to list.
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(string value)
        {
            ArrayFieldValue.Add(new StringField(value));
            HasBeenModified = true;
        }

        /// <summary>
        /// Remove value to list.
        /// </summary>
        /// <param name="value"></param>
        public void RemoveValue(string value)
        {
            var found = ArrayFieldValue.FirstOrDefault(c => c is StringField && ((StringField)c).FieldValue == value);
            ArrayFieldValue.Remove(found);
            HasBeenModified = found != null;
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
            //if (ArrayFieldValue.Count ==1)
            //{
                return "\"" + FieldName + "\":[" + string.Join(",", ArrayFieldValue.Select(c => c.ToJSONFormat())) + "]";
            //}
            //else
            {
            //    return "\"" + FieldName + "\":" + string.Join(",", ArrayFieldValue.Select(c => c.ToJSONFormat()));
            }
        }
    }
}
