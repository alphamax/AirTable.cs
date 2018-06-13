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
        public ArrayField(string propertyName, IEnumerable<string> array)
            : base(propertyName)
        {
            ArrayFieldValue = new List<AbstractField>(array.Select(c => new StringField(c)));
        }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        public ArrayField(IEnumerable<AbstractField> array)
            : base(string.Empty)
        {
            ArrayFieldValue = new List<AbstractField>(array);
        }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        public ArrayField(string propertyName, IEnumerable<AbstractField> array)
            : base(propertyName)
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
            if (string.IsNullOrWhiteSpace(FieldName))
            {
              return "{" + string.Join(",", ArrayFieldValue.Select(c => c.ToJSONFormat())) + "}";
            }
            else
            {
                return "\"" + FieldName + "\":[" + string.Join(",", ArrayFieldValue.Select(c => c.ToJSONFormat())) + "]";
            }
        }

        public List<AbstractField> FieldValues
        {
            get
            {
                if (ArrayFieldValue.Count == 1 && ArrayFieldValue[0] is ArrayField)
                    return ((ArrayField)ArrayFieldValue[0]).FieldValues;
                else
                    return ArrayFieldValue;
            }
        }

        /// <summary>
        /// Get the array field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayField ExtractArrayField(string name)
        {
            if (FieldValues.Count(c => c.FieldName == name) == 0)
            {
                FieldValues.Add(new ArrayField(name, Enumerable.Empty<string>()));
            }
            return FieldValues.FirstOrDefault(c => c.FieldName == name) as ArrayField;
        }

        /// <summary>
        /// Get the boolean field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BooleanField ExtractBooleanField(string name)
        {
            if (FieldValues.Count(c => c.FieldName == name) == 0)
            {
                FieldValues.Add(new BooleanField(name, false));
            }
            return FieldValues.FirstOrDefault(c => c.FieldName == name) as BooleanField;
        }

        /// <summary>
        /// Get the integer field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IntegerField ExtractIntegerField(string name)
        {
            if (FieldValues.Count(c => c.FieldName == name) == 0)
            {
                FieldValues.Add(new IntegerField(name, 0));
            }
            return FieldValues.FirstOrDefault(c => c.FieldName == name) as IntegerField;
        }

        /// <summary>
        /// Get the string field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StringField ExtractStringField(string name)
        {
            if (FieldValues.Count(c => c.FieldName == name) == 0)
            {
                FieldValues.Add(new StringField(name, String.Empty));
            }
            return FieldValues.FirstOrDefault(c => c.FieldName == name) as StringField;
        }

        /// <summary>
        /// Get the string field corresponding of the given name.
        /// Create an empty one if not existing. (UseCase of creating record).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObjectField ExtractObjectField(string name)
        {
            if (FieldValues.Count(c => c.FieldName == name) == 0)
            {
                FieldValues.Add(new ObjectField(name, Enumerable.Empty<ArrayField>()));
            }
            return FieldValues.FirstOrDefault(c => c.FieldName == name) as ObjectField;
        }
    }
}
