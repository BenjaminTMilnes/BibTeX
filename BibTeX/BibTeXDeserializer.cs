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
    }
}
