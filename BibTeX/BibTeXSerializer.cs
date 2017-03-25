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

        public Type GetBibTeXEntryType(IBibTeXEntry entry)
        {
            if (entry is BibTeXBook) return typeof(BibTeXBook);

            throw new UnknownBibTeXEntryException();
        }

        public string GetBibTeXEntryName(Type type)
        {
            return type.GetCustomAttribute<BibTeXEntryName>().EntryName;
        }

        public string GetBibTeXEntryName(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXEntryName(type);
        }

        public string GetBibTeXFieldName(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<BibTeXFieldName>().FieldName;
        }

        public IEnumerable<string> GetBibTeXFieldNames(IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Select((propertyInfo) => GetBibTeXFieldName(propertyInfo));
        }

        public IEnumerable<string> GetBibTeXFieldNames(Type type)
        {
            var fields = GetBibTeXFields(type);

            return GetBibTeXFieldNames(fields);
        }

        public IEnumerable<string> GetBibTeXFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFieldNames(type);
        }


        public IEnumerable<string> GetBibTeXOptionalFieldNames(Type type)
        {
            var optionalFields = GetBibTeXOptionalFields(type);

            return GetBibTeXFieldNames(optionalFields);
        }

        public IEnumerable<string> GetBibTeXOptionalFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXOptionalFieldNames(type);
        }

        public IEnumerable<string> GetBibTeXRequiredFieldNames(Type type)
        {
            var requiredFields = GetBibTeXRequiredFields(type);


            return GetBibTeXFieldNames(requiredFields);

        }
        public IEnumerable<string> GetBibTeXRequiredFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXRequiredFieldNames(type);
        }
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