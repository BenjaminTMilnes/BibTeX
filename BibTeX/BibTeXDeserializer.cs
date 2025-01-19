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
        public static readonly string FIELD_NAME_ALLOWED_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

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
    }
}
