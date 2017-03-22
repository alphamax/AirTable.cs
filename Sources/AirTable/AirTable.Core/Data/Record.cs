using AirTable.Core.Data;
using AirTable.Core.Data.Field;
using AirTable.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core
{
    /// <summary>
    /// Record that represent a line of the base/table
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Id of the record
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Fields that compose this record
        /// </summary>
        public Dictionary<string, AbstractField> Fields { get; set; }

        /// <summary>
        /// Get the array field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayField ExtractArrayField(string name)
        {
            if(!Fields.ContainsKey(name))
            {
                Fields.Add(name, new ArrayField(name, Enumerable.Empty<string>()));
            }
            return Fields[name] as ArrayField;
        }

        /// <summary>
        /// Get the boolean field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BooleanField ExtractBooleanField(string name)
        {
             if(!Fields.ContainsKey(name))
            {
                Fields.Add(name, new BooleanField(name, false));
            }
            return Fields[name] as BooleanField;
        }

        /// <summary>
        /// Get the integer field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IntegerField ExtractIntegerField(string name)
        {
             if(!Fields.ContainsKey(name))
            {
                Fields.Add(name, new IntegerField(name, 0));
            }
            return Fields[name] as IntegerField;
        }

        /// <summary>
        /// Get the string field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StringField ExtractStringField(string name)
        {
             if(!Fields.ContainsKey(name))
            {
                Fields.Add(name, new StringField(name, String.Empty));
            }
            return Fields[name] as StringField;
        }

        /// <summary>
        /// Create a record and fill it with the given json.
        /// </summary>
        /// <param name="recordContent"></param>
        public Record(JToken recordContent)
            : this()
        {
            Id = recordContent["id"].Value<string>();
            foreach (var item in recordContent["fields"])
            {
                var itemCreated = DataHelper.CreateField(item);
                Fields.Add(itemCreated.FieldName, itemCreated);
            }
        }

        /// <summary>
        /// Create a fully empty token
        /// </summary>
        public Record()
        {
            Fields = new Dictionary<string, AbstractField>();
        }

        public string ToJSONFormat(bool includeUnmodified)
        {
            var result = "{\"fields\":{";

            var partialResults = Fields.Where(c => c.Value.HasBeenModified == true || includeUnmodified).Select(c => c.Value.ToJSONFormat());

            return result + string.Join(",", partialResults) + "}}";
        }
    }
}
