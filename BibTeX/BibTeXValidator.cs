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

        public bool ValidateBibTeXField(IBibTeXEntry entry, PropertyInfo property)
        {
            if (_attributeReader.IsBibTeXFieldRequired(property))
            {
                var entryName = _attributeReader.GetBibTeXEntryName(entry);
                var fieldName = _attributeReader.GetBibTeXFieldName(property);
                var fieldValue = property.GetValue(entry);

                if (fieldValue is BibTeXMonth)
                {
                    if ((BibTeXMonth)fieldValue == BibTeXMonth.None)
                    {
                        throw new RequiredFieldException(entryName, fieldName);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(fieldValue.ToString()))
                    {
                        throw new RequiredFieldException(entryName, fieldName);
                    }
                }
            }

            return true;
        }

        public bool ValidateBibTeXFields( IBibTeXEntry entry, IEnumerable<PropertyInfo> properties)
        {
            foreach(var property in properties)
            {
                ValidateBibTeXField(entry, property);
            }

            return true;
        }

        public bool ValidateBibTeXEntry( IBibTeXEntry entry)
        {
            var fields = _attributeReader.GetBibTeXFields(entry);

            ValidateBibTeXFields(entry, fields);

            return true;
        }

        public bool ValidateBibTeXDatabase( BibTeXDatabase database)
        {
            foreach(var entry in database.Entries)
            {
                ValidateBibTeXEntry(entry);
            }

            return true;
        }
    }
}
