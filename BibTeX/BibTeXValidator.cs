using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BibTeX
{
    public class RequiredFieldException : Exception
    {
        public string EntryName { get; }
        public string Name { get; }

        public RequiredFieldException(string entryName, string name)
        {
            EntryName = entryName;
            Name = name;
        }

        public override string ToString()
        {
            return $"The field '{Name}' is a required field for '{EntryName}' entries.";
        }
    }

    public class BibTeXValidator
    {
        protected BibTeXAttributeReader _attributeReader;

        public BibTeXValidator()
        {
            _attributeReader = new BibTeXAttributeReader();
        }

        public bool IsBibTeXFieldValueNone(object fieldValue)
        {
            if (fieldValue is BibTeXMonth)
            {
                return (BibTeXMonth)fieldValue == BibTeXMonth.None;
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldValue.ToString());
            }
        }

        public bool ValidateBibTeXRequiredField(IBibTeXEntry entry, PropertyInfo property)
        {
            var entryName = _attributeReader.GetBibTeXEntryName(entry);
            var fieldName = _attributeReader.GetBibTeXFieldName(property);
            var fieldValue = property.GetValue(entry);

            if (IsBibTeXFieldValueNone(fieldValue))
            {
                throw new RequiredFieldException(entryName, fieldName);
            }

            return true;
        }

        public bool ValidateBibTeXFields(IBibTeXEntry entry, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                if (_attributeReader.IsBibTeXFieldRequired(property))
                {
                    ValidateBibTeXRequiredField(entry, property);
                }
            }

            return true;
        }

        public bool ValidateBibTeXEntry(IBibTeXEntry entry)
        {
            var fields = _attributeReader.GetBibTeXFields(entry);

            ValidateBibTeXFields(entry, fields);

            return true;
        }

        public bool ValidateBibTeXDatabase(BibTeXDatabase database)
        {
            foreach (var entry in database.Entries)
            {
                ValidateBibTeXEntry(entry);
            }

            return true;
        }
    }
}
