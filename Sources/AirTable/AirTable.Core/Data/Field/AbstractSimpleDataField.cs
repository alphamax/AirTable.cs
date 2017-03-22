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
    public abstract class AbstractSimpleDataField<T> : AbstractField

    {
         /// <summary>
        /// Field container.
        /// </summary>
        private T _FieldValue;
        /// <summary>
        /// Field container that manage modification flag.
        /// </summary>
        public T FieldValue
        {
            get 
            { 
                return _FieldValue; 
            }
            set 
            { 
                if ((value != null && !value.Equals(_FieldValue)) || (value == null && _FieldValue != null)) 
                { 
                    _FieldValue = value; 
                    HasBeenModified = true; 
                }
            }
        }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public AbstractSimpleDataField(string name, T value)
            : base(name)
        {
            _FieldValue = value;
        }
    }
}
