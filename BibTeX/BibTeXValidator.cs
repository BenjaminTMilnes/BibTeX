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

    public class RequiredFieldGroupException : Exception
    {
        public string EntryName { get; }
        public string GroupName { get; }
        public IEnumerable<string> FieldNames { get; }

        public RequiredFieldGroupException(string entryName, string groupName, IEnumerable<string> fieldNames)
        {
            EntryName = entryName;
            GroupName = groupName;
            FieldNames = fieldNames;
        }

        public override string ToString()
        {
            return $"One field in the field group '{GroupName}' ({string.Join(", ", FieldNames)}) is required for '{EntryName}' entries.";
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

        public bool ValidateBibTeXRequiredFieldGroup(IBibTeXEntry entry, string groupName, IEnumerable<PropertyInfo> properties)
        {
            var entryName = _attributeReader.GetBibTeXEntryName(entry);
            var fieldNames = _attributeReader.GetBibTeXFieldNames(properties);

            var isOneFieldSet = false;

            foreach (var property in properties)
            {
                var fieldValue = property.GetValue(entry);

                if (!IsBibTeXFieldValueNone(fieldValue))
                {
                    isOneFieldSet = true;
                }
            }

            if (isOneFieldSet == false)
            {
                throw new RequiredFieldGroupException(entryName, groupName, fieldNames);
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

            var groupNames = _attributeReader.GetBibTeXRequiredFieldGroupNames(properties);

            foreach (var groupName in groupNames)
            {
                var groupFields = _attributeReader.GetFieldsInBibTeXRequiredFieldGroup(properties, groupName);

                ValidateBibTeXRequiredFieldGroup(entry, groupName, groupFields);
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
