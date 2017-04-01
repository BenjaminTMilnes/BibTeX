﻿using System;
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
        private BibTeXSerializer _serializer;

        public BibTeXSerializerTests()
        {
            _serializer = new BibTeXSerializer(BibTeXBeginEndFieldValueCharacterType.QuotationMarks);
        }

        [TestMethod]
        public void GetBibTeXEntryNameTest()
        {
            Assert.AreEqual("book", _serializer.GetBibTeXEntryName(new BibTeXBook()));
        }

        [TestMethod]
        public void GetBibTeXFieldsTest()
        {
            Assert.AreEqual(11, _serializer.GetBibTeXFields(new BibTeXBook()).Count());
        }

        [TestMethod]
        public void GetBibTeXFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "author", "title", "publisher", "year", "volume", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name);
            var fieldNames = _serializer.GetBibTeXFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.IsTrue(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [TestMethod]
        public void GetBibTeXOptionalFieldsTest()
        {
            Assert.AreEqual(7, _serializer.GetBibTeXOptionalFields(new BibTeXBook()).Count());
        }

        [TestMethod]
        public void GetBibTeXOptionalFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "volume", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name);
            var fieldNames = _serializer.GetBibTeXOptionalFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.IsTrue(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [TestMethod]
        public void GetBibTeXRequiredFieldsTest()
        {
            Assert.AreEqual(4, _serializer.GetBibTeXRequiredFields(new BibTeXBook()).Count());
        }

        [TestMethod]
        public void GetBibTeXRequiredFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "author", "title", "publisher", "year" }).OrderBy((name) => name);
            var fieldNames = _serializer.GetBibTeXRequiredFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.IsTrue(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [TestMethod]
        public void GetBibTeXFieldByNameTest()
        {
            var book = new BibTeXBook();

            book.Author = "B. T. Milnes";

            var propertyInfo = _serializer.GetBibTeXFieldByName(book, "author");

            Assert.AreEqual(book.Author, propertyInfo.GetValue(book));
        }

        [TestMethod]
        public void SerializeBibTeXFieldTest()
        {
            var book = new BibTeXBook();

            book.Author = "B. T. Milnes";

            var property = _serializer.GetBibTeXFieldByName(book, "author");
            var field = _serializer.GetBibTeXFieldWithValue(book, property);

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
    }
}