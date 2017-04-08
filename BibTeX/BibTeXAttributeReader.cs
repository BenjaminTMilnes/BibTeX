using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BibTeX
{
    public class UnknownBibTeXEntryException : Exception { }

    /// <summary>
    /// Handles the getting of attribute data and the getting and setting of property values, which is used in reading, writing, and validating BibTeX databases.
    /// </summary>
    public class BibTeXAttributeReader
    {
        #region GetEntryType

        /// <summary>
        /// Gets the C# type of the IBibTeXEntry instance, which is used for getting attribute data of the entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public Type GetBibTeXEntryType(IBibTeXEntry entry)
        {
            if (entry is BibTeXArticle) return typeof(BibTeXArticle);
            if (entry is BibTeXBook) return typeof(BibTeXBook);
            if (entry is BibTeXBooklet) return typeof(BibTeXBooklet);
            if (entry is BibTeXConference) return typeof(BibTeXConference);
            if (entry is BibTeXInBook) return typeof(BibTeXInBook);
            if (entry is BibTeXInCollection) return typeof(BibTeXInCollection);
            if (entry is BibTeXInProceedings) return typeof(BibTeXInProceedings);
            if (entry is BibTeXManual) return typeof(BibTeXManual);
            if (entry is BibTeXMastersThesis) return typeof(BibTeXMastersThesis);
            if (entry is BibTeXMiscellaneous) return typeof(BibTeXMiscellaneous);
            if (entry is BibTeXPhDThesis) return typeof(BibTeXPhDThesis);
            if (entry is BibTeXProceedings) return typeof(BibTeXProceedings);
            if (entry is BibTeXTechReport) return typeof(BibTeXTechReport);
            if (entry is BibTeXUnpublished) return typeof(BibTeXUnpublished);

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

        #endregion

        #region GetOptionalFields

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

        #endregion

        #region GetRequiredFields

        /// <summary>
        /// Returns whether or not this C# property is a required BibTeX field.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool IsBibTeXFieldRequired(PropertyInfo property)
        {
            return property.GetCustomAttributes<BibTeXFieldName>().Any() && !property.GetCustomAttributes<BibTeXRequiredFieldGroup>().Any() && !property.GetCustomAttributes<BibTeXOptionalField>().Any();
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

        #endregion

        #region GetRequiredFieldGroups

        /// <summary>
        /// Gets whether this BibTeX field is in a required field group.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool IsBibTeXFieldInRequiredFieldGroup(PropertyInfo property)
        {
            return property.GetCustomAttributes<BibTeXRequiredFieldGroup>().Any();
        }

        /// <summary>
        /// Gets the name of the required field group that this BibTeX field is in.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetBibTeXRequiredFieldGroupName(PropertyInfo property)
        {
            return property.GetCustomAttribute<BibTeXRequiredFieldGroup>().Name;
        }

        /// <summary>
        /// Gets all of the required field group names from a set of BibTeX fields.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXRequiredFieldGroupNames(IEnumerable<PropertyInfo> properties)
        {
            var groupNames = new List<string>();

            foreach (var property in properties)
            {
                if (IsBibTeXFieldInRequiredFieldGroup(property))
                {
                    var groupName = GetBibTeXRequiredFieldGroupName(property);

                    if (!groupNames.Contains(groupName))
                    {
                        groupNames.Add(groupName);
                    }
                }
            }

            return groupNames;
        }

        /// <summary>
        /// Gets all of the required field group names for a given C# Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXRequiredFieldGroupNames(Type type)
        {
            var fields = GetBibTeXFields(type);

            return GetBibTeXRequiredFieldGroupNames(fields);
        }

        /// <summary>
        /// Gets all of the required field group names for a given BibTeX Entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IEnumerable<string> GetBibTeXRequiredFieldGroupNames(IBibTeXEntry entry)
        {
            var type = GetBibTeXEntryType(entry);

            return GetBibTeXRequiredFieldGroupNames(type);
        }

        /// <summary>
        /// Gets all of the fields in a required field group out of a set of C# properties.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetFieldsInBibTeXRequiredFieldGroup(IEnumerable<PropertyInfo> properties, string groupName)
        {
            return properties.Where((property) => IsBibTeXFieldInRequiredFieldGroup(property) && GetBibTeXRequiredFieldGroupName(property) == groupName);
        }

        /// <summary>
        /// Gets all of the fields in a required field group for a given C# Type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetFieldsInBibTeXRequiredFieldGroup(Type type, string groupName)
        {
            var fields = GetBibTeXFields(type);

            return GetFieldsInBibTeXRequiredFieldGroup(fields, groupName);
        }

        /// <summary>
        /// Gets all of the fields in a required field group for a given BibTeX Entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetFieldsInBibTeXRequiredFieldGroup(IBibTeXEntry entry, string groupName)
        {
            var type = GetBibTeXEntryType(entry);

            return GetFieldsInBibTeXRequiredFieldGroup(type, groupName);
        }

        #endregion

        #region GetFieldByName

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

        #endregion

        #region GetFieldsWithValues

        /// <summary>
        /// Given a BibTeX entry and a field, returns a tuple of the field name, order index, and value.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public Tuple<string, int, object> GetBibTeXFieldWithValue(IBibTeXEntry entry, PropertyInfo property)
        {
            var fieldName = GetBibTeXFieldName(property);
            var fieldValue = property.GetValue(entry);

            return new Tuple<string, int, object>(fieldName, 0, fieldValue);
        }

        /// <summary>
        /// Given a BibTeX entry and an enumerable of fields, returns an enumerable of tuples of the field names, order indices, and values.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IEnumerable<Tuple<string, int, object>> GetBibTeXFieldsWithValues(IBibTeXEntry entry, IEnumerable<PropertyInfo> properties)
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
        public IEnumerable<Tuple<string, int, object>> GetBibTeXFieldsWithValues(IBibTeXEntry entry)
        {
            var fields = GetBibTeXFields(entry);

            foreach (var field in fields)
            {
                yield return GetBibTeXFieldWithValue(entry, field);
            }
        }

        #endregion
    }
}
