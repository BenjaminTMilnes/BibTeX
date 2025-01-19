using System;
using System.Collections.Generic;
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

    public class BibTeXDeserializer
    {
        public static readonly string CITATION_KEY_ALLOWED_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        public static readonly string FIELD_NAME_ALLOWED_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public BibTeXEntry GetEntry(string inputText, Marker marker)
        {
            var c = inputText[marker.Position];

            if (c == '@')
            {
                marker.Position++;
            }
            else
            {
                return null;
            }

            var _type = GetType(inputText, marker);

            if (_type == null)
            {
                return null;
            }

            c = inputText[marker.Position];

            if (c == '{')
            {
                marker.Position++;
            }
            else
            {
                return null;
            }

            var citationKey = GetCitationKey(inputText, marker);

            if (citationKey == null)
            {
                return null;
            }

            var fields = new List<Tuple<string, string>>();

            while (marker.Position < inputText.Length)
            {
                GetWhiteSpace(inputText, marker);

                c = inputText[marker.Position];

                if (c == ',')
                {
                    marker.Position++;
                }
                else
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

            c = inputText[marker.Position];

            if (c == '}')
            {
                marker.Position++;
            }
            else
            {
                return null;
            }

            if (_type == "book")
            {
                var entry = new BibTeXBook();

                entry.CitationKey = citationKey;

                if (fields.Any(f => f.Item1 == "author"))
                {
                    entry.Author = fields.First(f => f.Item1 == "author").Item2;
                }

                return entry;
            }

            return null;
        }

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

        public Tuple<string, string> GetField(string inputText, Marker marker)
        {
            var fieldName = GetFieldName(inputText, marker);

            if (fieldName == null)
            {
                return null;
            }

            var c = inputText[marker.Position];

            if (c == '=')
            {
                marker.Position++;
            }
            else
            {
                return null;
            }

            var fieldValue = GetFieldValue(inputText, marker);

            if (fieldValue == null)
            {
                return null;
            }

            var field = new Tuple<string, string>(fieldName, fieldValue);

            return field;
        }

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

        public string GetFieldValue(string inputText, Marker marker)
        {
            var fieldValue = "";

            if (inputText[marker.Position] == '{')
            {
                marker.Position++;
            }
            else
            {
                return null;
            }

            while (marker.Position < inputText.Length)
            {
                var c = inputText[marker.Position];

                if (c == '}')
                {
                    marker.Position++;
                    break;
                }
                else
                {
                    fieldValue += c;
                    marker.Position++;
                }
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

        public string GetWhiteSpace(string inputText, Marker marker)
        {
            var t = "";

            while (marker.Position < inputText.Length)
            {
                var c = inputText[marker.Position];

                if (" \t\n\r".Contains(c))
                {
                    t += c;
                    marker.Position++;
                }
                else
                {
                    break;
                }
            }

            if (t != "")
            {
                return t;
            }
            else
            {
                return null;
            }
        }
    }
}
