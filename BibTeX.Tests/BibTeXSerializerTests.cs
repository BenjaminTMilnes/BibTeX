using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibTeX;

namespace BibTeX.Tests
{
    [TestClass]
    public class BibTeXSerializerTests
    {
        private BibTeXAttributeReader _attributeReader;
        private BibTeXSerializer _serializer;

        public BibTeXSerializerTests()
        {
            _attributeReader = new BibTeXAttributeReader();
            _serializer = new BibTeXSerializer();
        }

        [TestMethod]
        public void SerializeBibTeXFieldTest()
        {
            var book = new BibTeXBook();

            book.Author = "B. T. Milnes";

            var property = _attributeReader.GetBibTeXFieldByName(book, "author");
            var field = _attributeReader.GetBibTeXFieldWithValue(book, property);

            Assert.AreEqual("author = \"B. T. Milnes\"", _serializer.SerializeBibTeXField(field));
        }

        [TestMethod]
        public void SerializeBibTeXMonthTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January);
            var month = BibTeXMonth.December;

            Assert.AreEqual("December", serializer.SerializeBibTeXMonth(month));

            serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.Jan);
            month = BibTeXMonth.November;

            Assert.AreEqual("Nov", serializer.SerializeBibTeXMonth(month));

            serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.Numeric);
            month = BibTeXMonth.October;

            Assert.AreEqual("10", serializer.SerializeBibTeXMonth(month));
        }

        [TestMethod]
        public void SerializeBibTeXMonthFieldTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January);
            var book = new BibTeXBook();

            book.Month = BibTeXMonth.September;

            var property = _attributeReader.GetBibTeXFieldByName(book, "month");
            var field = _attributeReader.GetBibTeXFieldWithValue(book, property);

            Assert.AreEqual("month = \"September\"", serializer.SerializeBibTeXField(field));
        }

        [TestMethod]
        public void SerializeBibTeXEntryTest()
        {
            var serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks, BibTeXMonthStyle.January, BibTeXFormatStyle.Minimal);

            var miscellaneous = new BibTeXMiscellaneous();

            miscellaneous.CitationKey = "wxyz";
            miscellaneous.Author = "abcd";

            Assert.AreEqual("@misc{wxyz,author=\"abcd\"}\n", serializer.SerializeBibTeXEntry(miscellaneous));
        }
    }
}