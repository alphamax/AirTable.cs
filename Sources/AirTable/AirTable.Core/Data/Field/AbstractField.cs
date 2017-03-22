using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Field
{
    /// <summary>
    /// Abstract field container.
    /// </summary>
    public abstract class AbstractField
    {
        /// <summary>
        /// Mark as modified.
        /// Fur Update purpose.
        /// </summary>
        public bool HasBeenModified { get; set; }

        /// <summary>
        /// Name of the field
        /// </summary>
        public string FieldName { get; private set; }

        public AbstractField(string name)
        {
            FieldName = name;
            HasBeenModified = false;
        }

        /// <summary>
        /// Extract a JSON representation.
        /// </summary>
        /// <returns></returns>
        public abstract string ToJSONFormat();
    }
}
