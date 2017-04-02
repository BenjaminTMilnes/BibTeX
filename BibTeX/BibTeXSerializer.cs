using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public class BibTeXSerializer
    {
        protected BibTeXAttributeReader _attributeReader;

        protected readonly string BibTeXBeginEntryCharacter = "@";
        protected readonly string BibTeXBeginFieldsCharacter = "{";
        protected readonly string BibTeXEndFieldsCharacter = "}";
        protected readonly string BibTeXBeginFieldValueCharacter;
        protected readonly string BibTeXEndFieldValueCharacter;
        protected readonly string BibTeXFieldSeparatorCharacter = ",";

        protected readonly string[] MonthNames = { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        protected readonly string[] AbbreviatedMonthNames = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public BibTeXFormatStyle FormatStyle { get; }
        public BibTeXMonthStyle MonthStyle { get; }

        #region Constructors

        public BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType beginEndFieldValueCharacterType = BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle monthStyle = BibTeXMonthStyle.January, BibTeXFormatStyle formatStyle = BibTeXFormatStyle.Readable)
        {
            _attributeReader = new BibTeXAttributeReader();

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

            MonthStyle = monthStyle;
            FormatStyle = formatStyle;
        }

        #endregion

        #region SerializeFieldValue

        public string SerializeBibTeXFieldValue( object fieldValue)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXFieldValue(stringBuilder, fieldValue);

            return stringBuilder.ToString();
        }
        
        public void SerializeBibTeXFieldValue( StringBuilder stringBuilder, object fieldValue)
        {
            if (fieldValue is BibTeXMonth)
            {
                stringBuilder.Append(SerializeBibTeXMonth((BibTeXMonth)fieldValue));
            }
            else
            {
                stringBuilder.Append(fieldValue.ToString());
            }
        }

        #endregion

        #region SerializeFields

        /// <summary>
        /// Serializes a BibTeX field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string SerializeBibTeXField(Tuple<string, int, object> field)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXField(stringBuilder, field);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXField(StringBuilder stringBuilder, Tuple<string, int, object> field)
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
            SerializeBibTeXFieldValue(stringBuilder, field.Item3);
            stringBuilder.Append(BibTeXEndFieldValueCharacter);
        }

        public string SerializeBibTeXFields(IEnumerable<Tuple<string, int, object>> fields)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXFields(stringBuilder, fields);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXFields(StringBuilder stringBuilder, IEnumerable<Tuple<string, int, object>> fields)
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

        #region SerializeEntries

        public string SerializeBibTeXEntry(IBibTeXEntry entry)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXEntry(stringBuilder, entry);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXEntry(StringBuilder stringBuilder, IBibTeXEntry entry)
        {
            var entryName = _attributeReader.GetBibTeXEntryName(entry);
            var fields = _attributeReader.GetBibTeXFieldsWithValues(entry);

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

        public string SerializeBibTeXEntries(IEnumerable<IBibTeXEntry> entries)
        {
            var stringBuilder = new StringBuilder();

            SerializeBibTeXEntries(stringBuilder, entries);

            return stringBuilder.ToString();
        }

        public void SerializeBibTeXEntries(StringBuilder stringBuilder, IEnumerable<IBibTeXEntry> entries)
        {
            foreach (var entry in entries)
            {
                SerializeBibTeXEntry(stringBuilder, entry);
            }
        }

        #endregion

        #region SerializeBibTeXDatabase

        public string SerializeBibTeXDatabase(BibTeXDatabase database)
        {
            return SerializeBibTeXEntries(database.Entries);
        }

        #endregion

        #region SerializeBibTeXMonth

        public string SerializeBibTeXMonth(BibTeXMonth month)
        {
            var monthNumber = (int)month;

            if (MonthStyle == BibTeXMonthStyle.January)
            {
                return MonthNames[monthNumber];
            }
            else if (MonthStyle == BibTeXMonthStyle.Jan)
            {
                return AbbreviatedMonthNames[monthNumber];
            }
            else
            {
                if (monthNumber >= 1 && monthNumber <= 12)
                {
                    return monthNumber.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion
    }
}