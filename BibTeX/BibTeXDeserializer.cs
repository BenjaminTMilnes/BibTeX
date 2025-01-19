using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public class Marker
    {
        public int Position { get; set; }

        public int P { get { return Position; } set { Position = value; } }

        public Marker()
        {
            Position = 0;
        }
    }

    /// <summary>
    /// A class for parsing a BibTeX database from text.
    /// </summary>
    public class BibTeXDeserializer
    {
        public static readonly string CITATION_KEY_ALLOWED_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        public static readonly string FIELD_NAME_ALLOWED_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public static readonly string WHITE_SPACE_CHARACTERS = " \t\n\r";

        public BibTeXDatabase GetDatabaseFromFile(string filePath)
        {
            var fileText = File.ReadAllText(filePath);

            return GetDatabase(fileText);
        }

        public BibTeXDatabase GetDatabase(string inputText)
        {
            return GetDatabase(inputText, new Marker());
        }

        public BibTeXDatabase GetDatabase(string inputText, Marker marker)
        {
            var database = new BibTeXDatabase();

            while (marker.Position < inputText.Length)
            {
                var entry = GetEntry(inputText, marker);

                if (entry != null)
                {
                    database.Entries.Add(entry);
                }
                else
                {
                    break;
                }
            }

            return database;
        }

        /// <summary>
        /// Gets an entry at the given position in the input text and returns it. If no valid entry is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public BibTeXEntry GetEntry(string inputText, Marker marker)
        {
            GetWhiteSpace(inputText, marker);

            if (!Expect(inputText, marker, "@"))
            {
                return null;
            }

            var _type = GetType(inputText, marker);

            if (_type == null)
            {
                return null;
            }

            GetWhiteSpace(inputText, marker);

            if (!Expect(inputText, marker, "{"))
            {
                return null;
            }

            GetWhiteSpace(inputText, marker);

            var citationKey = GetCitationKey(inputText, marker);

            if (citationKey == null)
            {
                return null;
            }

            var fields = new List<Tuple<string, string>>();

            while (marker.Position < inputText.Length)
            {
                GetWhiteSpace(inputText, marker);

                if (!Expect(inputText, marker, ","))
                {
                    break;
                }

                GetWhiteSpace(inputText, marker);

                var field = GetField(inputText, marker);

                if (field != null)
                {
                    fields.Add(field);
                }
                else
                {
                    break;
                }
            }

            GetWhiteSpace(inputText, marker);

            if (!Expect(inputText, marker, "}"))
            {
                return null;
            }

            if (_type == "book")
            {
                var entry = new BibTeXBook();

                entry.CitationKey = citationKey;

                entry.Author = GetFieldValue(fields, "author", "");
                entry.Editor = GetFieldValue(fields, "editor", "");
                entry.Title = GetFieldValue(fields, "title", "");
                entry.Publisher = GetFieldValue(fields, "publisher", "");
                entry.Year = GetFieldValue(fields, "year", "");
                entry.Volume = GetFieldValue(fields, "volume", "");
                entry.Number = GetFieldValue(fields, "number", "");
                entry.Series = GetFieldValue(fields, "series", "");
                entry.Address = GetFieldValue(fields, "address", "");
                entry.Edition = GetFieldValue(fields, "edition", "");
                entry.Month = GetFieldValue(fields, "month", BibTeXMonth.None);

                return entry;
            }
            if (_type == "article")
            {
                var entry = new BibTeXArticle();

                entry.CitationKey = citationKey;

                entry.Author = GetFieldValue(fields, "author", "");
                entry.Title = GetFieldValue(fields, "title", "");
                entry.Journal = GetFieldValue(fields, "journal", "");
                entry.Year = GetFieldValue(fields, "year", "");
                entry.Volume = GetFieldValue(fields, "volume", "");
                entry.Number = GetFieldValue(fields, "number", "");
                entry.Pages = GetFieldValue(fields, "pages", "");
                entry.Month = GetFieldValue(fields, "month", BibTeXMonth.None);

                return entry;
            }

            return null;
        }

        public T GetFieldValue<T>(List<Tuple<string, string>> fields, string fieldName, T defaultValue)
        {
            if (fields.Any(f => f.Item1 == fieldName))
            {
                var fieldValue = fields.First(f => f.Item1 == fieldName).Item2;

                if (typeof(T) == typeof(BibTeXMonth))
                {
                    var month = BibTeXMonth.None;

                    switch (fieldValue.Trim().ToLower())
                    {
                        case "january":
                        case "jan":
                            month = BibTeXMonth.January;
                            break;
                        case "february":
                        case "feb":
                            month = BibTeXMonth.February;
                            break;
                        case "march":
                        case "mar":
                            month = BibTeXMonth.March;
                            break;
                        case "april":
                        case "apr":
                            month = BibTeXMonth.April;
                            break;
                        case "may":
                            month = BibTeXMonth.May;
                            break;
                        case "june":
                        case "jun":
                            month = BibTeXMonth.June;
                            break;
                        case "july":
                        case "jul":
                            month = BibTeXMonth.July;
                            break;
                        case "august":
                        case "aug":
                            month = BibTeXMonth.August;
                            break;
                        case "september":
                        case "sep":
                            month = BibTeXMonth.September;
                            break;
                        case "october":
                        case "oct":
                            month = BibTeXMonth.October;
                            break;
                        case "november":
                        case "nov":
                            month = BibTeXMonth.November;
                            break;
                        case "december":
                        case "dec":
                            month = BibTeXMonth.December;
                            break;
                    }

                    return (T)Convert.ChangeType(month, typeof(T));
                }
                else
                {
                    return (T)Convert.ChangeType(fieldValue, typeof(T));
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets an entry type at the given position in the input text and returns it. If none is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public string GetType(string inputText, Marker marker)
        {
            var _type = "";

            while (marker.Position < inputText.Length)
            {
                var c = inputText[marker.Position];

                if (FIELD_NAME_ALLOWED_CHARACTERS.Contains(c))
                {
                    _type += c;
                    marker.Position++;
                }
                else
                {
                    break;
                }
            }

            if (_type != "")
            {
                return _type;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a citation key at the given position in the input text and returns it. If none is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public string GetCitationKey(string inputText, Marker marker)
        {
            var citationKey = "";

            while (marker.Position < inputText.Length)
            {
                var c = inputText[marker.Position];

                if (CITATION_KEY_ALLOWED_CHARACTERS.Contains(c))
                {
                    citationKey += c;
                    marker.Position++;
                }
                else
                {
                    break;
                }
            }

            if (citationKey != "")
            {
                return citationKey;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a field at the given position in the input text and returns it. If none is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public Tuple<string, string> GetField(string inputText, Marker marker)
        {
            GetWhiteSpace(inputText, marker);

            var fieldName = GetFieldName(inputText, marker);

            if (fieldName == null)
            {
                return null;
            }

            GetWhiteSpace(inputText, marker);

            if (!Expect(inputText, marker, "="))
            {
                return null;
            }

            GetWhiteSpace(inputText, marker);

            var fieldValue = GetFieldValue(inputText, marker);

            if (fieldValue == null)
            {
                return null;
            }

            var field = new Tuple<string, string>(fieldName, fieldValue);

            return field;
        }

        /// <summary>
        /// Gets a field name at the given position in the input text and returns it. If none is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public string GetFieldName(string inputText, Marker marker)
        {
            var fieldName = "";

            while (marker.Position < inputText.Length)
            {
                var c = inputText[marker.Position];

                if (FIELD_NAME_ALLOWED_CHARACTERS.Contains(c))
                {
                    fieldName += c;
                    marker.Position++;
                }
                else
                {
                    break;
                }
            }

            if (fieldName != "")
            {
                return fieldName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a field value at the given position in the input text and returns it. If none is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public string GetFieldValue(string inputText, Marker marker)
        {
            var fieldValue = "";

            var delimiter = "";

            var c = inputText[marker.Position];

            if (c == '=')
            {
                return null;
            }

            if (c == '{')
            {
                delimiter = "{}";
                marker.Position++;
            }
            else if (c == '"')
            {
                delimiter = "\"";
                marker.Position++;
            }
            else
            {
                delimiter = "none";
            }

            while (marker.Position < inputText.Length)
            {
                c = inputText[marker.Position];

                if (delimiter == "{}" && c == '}')
                {
                    marker.Position++;
                    break;
                }
                else if (delimiter == "\"" && c == '"')
                {
                    marker.Position++;
                    break;
                }
                else if (delimiter == "none" && (c == ',' || c == '}'))
                {
                    break;
                }
                else
                {
                    fieldValue += c;
                    marker.Position++;
                }
            }

            if (delimiter == "none")
            {
                fieldValue = fieldValue.Trim();
            }

            if (fieldValue != "")
            {
                return fieldValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets any white space at the given position in the input text and returns it. If none is found, returns null.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        public string GetWhiteSpace(string inputText, Marker marker)
        {
            var whiteSpace = "";

            while (marker.Position < inputText.Length)
            {
                var c = inputText[marker.Position];

                if (WHITE_SPACE_CHARACTERS.Contains(c))
                {
                    whiteSpace += c;
                    marker.Position++;
                }
                else
                {
                    break;
                }
            }

            if (whiteSpace != "")
            {
                return whiteSpace;
            }
            else
            {
                return null;
            }
        }

        public bool Expect(string inputText, Marker marker, string text)
        {
            if (inputText.Substring(marker.Position, text.Length) == text)
            {
                marker.Position += text.Length;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
