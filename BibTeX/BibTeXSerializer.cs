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

        public IEnumerable<Tuple<string, int, string>> GetBibTeXFields(IBibTeXEntry entry, PropertyInfo[] propertyInfos)
        {
            foreach (var propertyInfo in propertyInfos)
            {
                yield return GetBibTeXField(entry, propertyInfo);
            }
        }
    }
}