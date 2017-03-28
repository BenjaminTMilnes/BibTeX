using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BibTeX
{
    public class UnknownBibTeXEntryException : Exception { }

    public class BibTeXSerializer
    {
        protected readonly string BibTeXBeginEntryCharacter = "@";
        protected readonly string BibTeXBeginFieldsCharacter = "{";
        protected readonly string BibTeXEndFieldsCharacter = "}";
        protected readonly string BibTeXBeginFieldValueCharacter;
        protected readonly string BibTeXEndFieldValueCharacter;
        protected readonly string BibTeXFieldSeparatorCharacter = ",";

        public BibTeXFormatStyle FormatStyle { get; }
        public BibTeXMonthStyle MonthStyle { get; }

        #region Constructors

        public BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType beginEndFieldValueCharacterType)
        {
            if (beginEndFieldValueCharacterType == BibTeXBeginEndFieldValueCharacterType.QuotationMarks)
            {
                BibTeXBeginFieldValueCharacter = "\"";
                BibTeXEndFieldValueCharacter = "\"";
            }
            else if (beginEndFieldValueCharacterType == BibTeXBeginEndFieldValueCharacterType.RecurveBrackets)
            {

                BibTeXBeginFieldValueCharacter = "{";
                BibTeXEndFieldValueCharacter = "}";
            }

            FormatStyle = BibTeXFormatStyle.Readable;
        }

        #endregion

        #region GetEntryType

        /// <summary>
        /// Gets the C# type of the IBibTeXEntry instance, which is used for getting attribute data of the entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public Type GetBibTeXEntryType(IBibTeXEntry entry)
        {
            if (entry is BibTeXBook) return typeof(BibTeXBook);

            throw new UnknownBibTeXEntryException();
        }

        #endregion

        #region GetEntryName

        /// <summary>
        /// Gets the BibTeX entry name for this C# type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetBibTeXEntryName(Type type)
        {
            return type.GetCustomAttribute<BibTeXEntryName>().EntryName;
        }

        /// <summary>
        /// Gets the BibTeX entry name for this BibTeX entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public string GetBibTeXEntryName(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXEntryName(type);
        }

        #endregion

        #region GetFieldName

        /// <summary>
        /// Gets the BibTeX field name for this C# property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetBibTeXFieldName(PropertyInfo property)
        {
            return property.GetCustomAttribute<BibTeXFieldName>().FieldName;
        }

        /// <summary>
        /// Gets the BibTeX field names for these C# properties.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXFieldNames(IEnumerable<PropertyInfo> properties)
        {
            return properties.Select((property) => GetBibTeXFieldName(property));
        }

        /// <summary>
        /// Gets the BibTeX field names for all of the BibTeX fields in this C# type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXFieldNames(Type type)
        {
            var fields = GetBibTeXFields(type);

            return GetBibTeXFieldNames(fields);
        }

        /// <summary>
        /// Gets the BibTeX field names for all of the BibTeX fields in this entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFieldNames(type);
        }

        /// <summary>
        /// Gets the BibTeX field names for all of the optional BibTeX fields in this C# type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXOptionalFieldNames(Type type)
        {
            var optionalFields = GetBibTeXOptionalFields(type);

            return GetBibTeXFieldNames(optionalFields);
        }

        /// <summary>
        /// Gets the BibTeX field names for all of the optional BibTeX fields in this entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXOptionalFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXOptionalFieldNames(type);
        }

        /// <summary>
        /// Gets the BibTeX field names for all of the required BibTeX fields in this C# type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXRequiredFieldNames(Type type)
        {
            var requiredFields = GetBibTeXRequiredFields(type);

            return GetBibTeXFieldNames(requiredFields);
        }

        /// <summary>
        /// Gets the BibTeX field names for all of the required BibTeX fields in this entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXRequiredFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXRequiredFieldNames(type);
        }

        #endregion

        #region GetFields

        /// <summary>
        /// Gets the properties of this C# type which are BibTeX fields.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetBibTeXFields(Type type)
        {
            var properties = type.GetProperties();
            var fields = properties.Where((property) => property.GetCustomAttributes<BibTeXFieldName>().Any());

            return fields;
        }

        /// <summary>
        /// Gets the properties of this BibTeX entry that are BibTeX fields.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetBibTeXFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFields(type);
        }

        /// <summary>
        /// Returns whether or not this C# property is an optional BibTeX field.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool IsBibTeXFieldOptional(PropertyInfo property)
        {
            return property.GetCustomAttributes<BibTeXOptionalField>().Any();
        }

        /// <summary>
        /// Gets the properties of this C# type that are optional BibTeX fields.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetBibTeXOptionalFields(Type type)
        {
            var fields = GetBibTeXFields(type);

            return fields.Where((field) => IsBibTeXFieldOptional(field));
        }

        /// <summary>
        /// Gets the properties of this BibTeX entry that are optional BibTeX fields.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetBibTeXOptionalFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXOptionalFields(type);
        }

        /// <summary>
        /// Returns whether or not this C# property is a required BibTeX field.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool IsBibTeXFieldRequired(PropertyInfo property)
        {
            return property.GetCustomAttributes<BibTeXFieldName>().Any() && !property.GetCustomAttributes<BibTeXOptionalField>().Any();
        }

        /// <summary>
        /// Gets the properties of this C# type that are required BibTeX fields.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetBibTeXRequiredFields(Type type)
        {
            var fields = GetBibTeXFields(type);

            return fields.Where((field) => IsBibTeXFieldRequired(field));
        }

        /// <summary>
        /// Gets the properties of this BibTeX entry that are required BibTeX fields.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetBibTeXRequiredFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXRequiredFields(type);
        }

        /// <summary>
        /// Gets the BibTeX field with the given field name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyInfo GetBibTeXFieldByName(Type type, string name)
        {
            var fields = GetBibTeXFields(type);

            return fields.First((field) => GetBibTeXFieldName(field) == name);
        }

        /// <summary>
        /// Gets the BibTeX field with the given field name.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyInfo GetBibTeXFieldByName(IBibTeXEntry entry, string name)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFieldByName(type, name);
        }

        /// <summary>
        /// Given a BibTeX entry and a field, returns a tuple of the field name, order index, and value.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public Tuple<string, int, string> GetBibTeXFieldWithValue(IBibTeXEntry entry, PropertyInfo property)
        {
            var fieldName = GetBibTeXFieldName(property);
            var fieldValue = property.GetValue(entry).ToString();

            return new Tuple<string, int, string>(fieldName, 0, fieldValue);
        }

        /// <summary>
        /// Given a BibTeX entry and an enumerable of fields, returns an enumerable of tuples of the field names, order indices, and values.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IEnumerable<Tuple<string, int, string>> GetBibTeXFieldsWithValues(IBibTeXEntry entry, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                yield return GetBibTeXFieldWithValue(entry, property);
            }
        }

        /// <summary>
        /// Given a BibTeX entry, returns all of its fields and their values.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<Tuple<string, int, string>> GetBibTeXFieldsWithValues(IBibTeXEntry entry)
        {
            var fields = GetBibTeXFields(entry);

            foreach (var field in fields)
            {
                yield return GetBibTeXFieldWithValue(entry, field);
            }
        }

        #endregion

        #region SerializeFields

        /// <summary>
        /// Serializes a BibTeX field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string SerializeBibTeXField(Tuple<string, int, string> field)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXField(stringBuilder, field);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXField(StringBuilder stringBuilder, Tuple<string, int, string> field)
        {
            stringBuilder.Append(field.Item1);

            if (FormatStyle == BibTeXFormatStyle.Readable)
            {
                stringBuilder.Append(" = ");
            }
            else
            {
                stringBuilder.Append("=");
            }

            stringBuilder.Append(BibTeXBeginFieldValueCharacter);
            stringBuilder.Append(field.Item3);
            stringBuilder.Append(BibTeXEndFieldValueCharacter);
        }

        public string SerializeBibTeXFields(IEnumerable<Tuple<string, int, string>> fields)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXFields(stringBuilder, fields);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXFields(StringBuilder stringBuilder, IEnumerable<Tuple<string, int, string>> fields)
        {
            foreach (var field in fields)
            {
                stringBuilder.Append(BibTeXFieldSeparatorCharacter);

                if (FormatStyle == BibTeXFormatStyle.Readable)
                {
                    stringBuilder.Append("\n\t");
                }

                SerializeBibTeXField(stringBuilder, field);
            }
        }

        #endregion

        #region SerializeEntry

        public string SerializeBibTeXEntry(IBibTeXEntry entry)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXEntry(stringBuilder, entry);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXEntry(StringBuilder stringBuilder, IBibTeXEntry entry)
        {
            var entryName = GetBibTeXEntryName(entry);
            var fields = GetBibTeXFieldsWithValues(entry);

            stringBuilder.Append(BibTeXBeginEntryCharacter);
            stringBuilder.Append(entryName);
            stringBuilder.Append(BibTeXBeginFieldsCharacter);
            stringBuilder.Append(entry.CitationKey);

            SerializeBibTeXFields(stringBuilder, fields);

            if (FormatStyle == BibTeXFormatStyle.Readable)
            {
                stringBuilder.Append("\n");
            }

            stringBuilder.Append(BibTeXEndFieldsCharacter);
            stringBuilder.Append("\n");
        }

        #endregion

        public string SerializeBibTeXMonth(BibTeXMonth month)
        {
            return "";
        }
    }
}