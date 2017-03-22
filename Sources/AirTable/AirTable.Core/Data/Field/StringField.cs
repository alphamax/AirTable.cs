using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Field
{
    ///    ___  _____
    /// .'/,-Y"     "~-.
    /// l.Y             ^.
    /// /\               _\_
    ///i            ___/"   "\
    ///|          /"   "\   o !
    ///l         ]     o !__./
    /// \ _  _    \.___./    "~\
    ///  X \/ \            ___./
    /// ( \ ___.   _..--~~"   ~`-.
    ///  ` Z,--   /               \
    ///    \__.  (   /       ______)
    ///      \   l  /-----~~" /
    ///       Y   \          /
    ///       |    "x______.^
    ///       |           \
    ///       j            Y
    public class StringField : AbstractSimpleDataField<string>
    {
        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public StringField(string name, string value)
            : base(name, value)
        {
        }

        public override string ToJSONFormat()
        {
            return "\"" + FieldName + "\":\"" + FieldValue + "\"";
        }
    }
}
