using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX.Tests
{
    public class BibTeXSerializerTests
    {
        private BibTeXAttributeReader _attributeReader;
        private BibTeXSerializer _serializer;

        public BibTeXSerializerTests()
        {
            _attributeReader = new BibTeXAttributeReader();
            _serializer = new BibTeXSerializer();
        }

        [Fact]
        public void SerializeBibTeXFieldTest()
        {
            var book = new BibTeXBook();

            book.Author = "B. T. Milnes";

            var property = _attributeReader.GetBibTeXFieldByName(book, "author");
            var field = _attributeReader.GetBibTeXFieldWithValue(book, property);

            Assert.Equal("author = \"B. T. Milnes\"", _serializer.SerializeBibTeXField(field));
        }

        [Fact]
        public void SerializeBibTeXMonthTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January);
            var month = BibTeXMonth.December;

            Assert.Equal("December", serializer.SerializeBibTeXMonth(month));

            serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.Jan);
            month = BibTeXMonth.November;

            Assert.Equal("Nov", serializer.SerializeBibTeXMonth(month));

            serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.Numeric);
            month = BibTeXMonth.October;

            Assert.Equal("10", serializer.SerializeBibTeXMonth(month));
        }

        [Fact]
        public void SerializeBibTeXMonthFieldTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January);
            var book = new BibTeXBook();

            book.Month = BibTeXMonth.September;

            var property = _attributeReader.GetBibTeXFieldByName(book, "month");
            var field = _attributeReader.GetBibTeXFieldWithValue(book, property);

            Assert.Equal("month = \"September\"", serializer.SerializeBibTeXField(field));
        }

        [Fact]
        public void SerializeBibTeXEntryTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January, BibTeXFormatStyle.Minimal);

            var miscellaneous = new BibTeXMiscellaneous();

            miscellaneous.CitationKey = "wxyz";
            miscellaneous.Author = "abcd";

            Assert.Equal("@misc{wxyz,author=\"abcd\"}\n", serializer.SerializeBibTeXEntry(miscellaneous));
        }

        [Fact]
        public void EscapeBibTeXFieldValueTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January, BibTeXFormatStyle.Minimal);

            var miscellaneous = new BibTeXMiscellaneous();

            miscellaneous.CitationKey = "wxyz";
            miscellaneous.Title = "abcd \"efgh\" ijkl";

            Assert.Equal("@misc{wxyz,title=\"abcd \\\"efgh\\\" ijkl\"}\n", serializer.SerializeBibTeXEntry(miscellaneous));
        }
    }
}