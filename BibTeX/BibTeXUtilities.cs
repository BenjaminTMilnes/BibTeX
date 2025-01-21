using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    /// <summary>
    /// A static class that contains the most useful functions of this BibTeX library, such as converting between the object and text forms of a BibTeX databases.
    /// </summary>
    public static class BibTeXUtilities
    {
        public static string ConvertBibTeXDatabaseToText(BibTeXDatabase database, BibTeXBeginEndFieldValueCharacterType beginEndFieldValueCharacterType = BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle monthStyle = BibTeXMonthStyle.January, BibTeXFormatStyle formatStyle = BibTeXFormatStyle.Readable)
        {
            var serializer = new BibTeXSerializer(beginEndFieldValueCharacterType, monthStyle, formatStyle);

            return serializer.SerializeBibTeXDatabase(database);
        }

        public static string[] SplitOnAnd(string text)
        {
            return text.Split(new string[] { " and " }, StringSplitOptions.None).Select(t => t.Trim()).Where(t => t != "").ToArray();
        }
    }
}
