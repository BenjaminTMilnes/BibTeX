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
            return ((BibTeXEntryName)Attribute.GetCustomAttribute(type, typeof(BibTeXEntryName))).EntryName;
        }

        public string GetBibTeXFieldName(PropertyInfo propertyInfo)
        {
            return ((BibTeXFieldName)propertyInfo.GetCustomAttribute(typeof(BibTeXFieldName))).FieldName;
        }

        public string[] GetBibTeXFieldNames(PropertyInfo[] propertyInfos)
        {
            return propertyInfos.Select((propertyInfo) => GetBibTeXFieldName(propertyInfo)).ToArray();
        }

        public string[] GetBibTeXFieldNames(Type type)
        {
            return GetBibTeXFieldNames(type.GetProperties());
        }

        public string[] GetBibTeXFieldNames(IBibTeXEntry entry)
        {
            return GetBibTeXFieldNames(GetBibTeXEntryType(entry));
        }
    }
}
