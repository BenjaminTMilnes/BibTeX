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
        }

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

        public IEnumerable<PropertyInfo> GetBibTeXFields(Type type)
        {
            var properties = type.GetProperties();
            var fields = properties.Where((property) => property.GetCustomAttributes<BibTeXFieldName>().Any());

            return fields;
        }

        public IEnumerable<PropertyInfo> GetBibTeXFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFields(type);
        }

        public bool IsBibTeXFieldOptional(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes<BibTeXOptionalField>().Any();
        }

        public IEnumerable<PropertyInfo> GetBibTeXOptionalFields(Type type)
        {
            var fields = GetBibTeXFields(type);

            return fields.Where((field) => IsBibTeXFieldOptional(field));
        }

        public IEnumerable<PropertyInfo> GetBibTeXOptionalFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXOptionalFields(type);
        }

        public bool IsBibTeXFieldRequired(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes<BibTeXFieldName>().Any() && !propertyInfo.GetCustomAttributes<BibTeXOptionalField>().Any();
        }

        public IEnumerable<PropertyInfo> GetBibTeXRequiredFields(Type type)
        {
            var fields = GetBibTeXFields(type);

            return fields.Where((field) => IsBibTeXFieldRequired(field));
        }

        public IEnumerable<PropertyInfo> GetBibTeXRequiredFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXRequiredFields(type);
        }

        public PropertyInfo GetBibTeXFieldByName(Type type, string name)
        {
            var fields = GetBibTeXFields(type);

            return fields.First((field) => GetBibTeXFieldName(field) == name);
        }

        public PropertyInfo GetBibTeXFieldByName(IBibTeXEntry entry, string name)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFieldByName(type, name);
        }

        public Tuple<string, int, string> GetBibTeXField(IBibTeXEntry entry, PropertyInfo propertyInfo)
        {
            var fieldName = GetBibTeXFieldName(propertyInfo);
            var fieldValue = propertyInfo.GetValue(entry).ToString();

            return new Tuple<string, int, string>(fieldName, 0, fieldValue);
        }

        public IEnumerable<Tuple<string, int, string>> GetBibTeXFields(IBibTeXEntry entry, IEnumerable<PropertyInfo> propertyInfos)
        {
            foreach (var propertyInfo in propertyInfos)
            {
                yield return GetBibTeXField(entry, propertyInfo);
            }
        }

        public string SerializeBibTeXField(Tuple<string, int, string> field)
        {
            return $"{field.Item1} = {BibTeXBeginFieldValueCharacter}{field.Item3}{BibTeXEndFieldValueCharacter}";
        }

        public string SerializeBibTeXFields(IEnumerable<Tuple<string, int, string>> fields)
        {
            return string.Join(BibTeXFieldSeparatorCharacter + "\n\t", fields.Select((field) => SerializeBibTeXField(field)));

        }


        public string SerializeBibTeXEntry(IBibTeXEntry entry)
        {
            var entryName = GetBibTeXEntryName(entry);
            var propertyInfos = GetBibTeXFields(entry);
            var fields = GetBibTeXFields(entry, propertyInfos);
            var serializedFields = SerializeBibTeXFields(fields);

            return $"{BibTeXBeginEntryCharacter}{entryName}{BibTeXBeginFieldsCharacter}{serializedFields}{BibTeXEndFieldsCharacter}";


        }
        public string SerializeBibTeXMonth(BibTeXMonth month)
        {
            return "";
        }
    }
}