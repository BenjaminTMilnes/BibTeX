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


        public string GetBibTeXFieldName(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<BibTeXFieldName>().FieldName;

        }

        public string[] GetBibTeXFieldNames(PropertyInfo[] propertyInfos)
        {
            return propertyInfos.Select((propertyInfo) => GetBibTeXFieldName(propertyInfo)).ToArray();
        }

        public string[] GetBibTeXFieldNames(Type type)
        {
            var fields = GetBibTeXFields(type);

            return GetBibTeXFieldNames(fields);
        }

        public string[] GetBibTeXFieldNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFieldNames(type);
        }

        public PropertyInfo[] GetBibTeXFields(Type type)
        {
            var properties = type.GetProperties();
            var fields = properties.Where((property) => property.GetCustomAttributes<BibTeXFieldName>().Any()).ToArray();

            return fields;
        }

        public PropertyInfo[] GetBibTeXFields(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXFields(type);

        }
    }
}
