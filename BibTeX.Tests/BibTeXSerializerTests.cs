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
        private BibTeXSerializer _serializer;

        public BibTeXSerializerTests()
        {
            _serializer = new BibTeXSerializer();
        }

        [TestMethod]
        public void GetBibTeXEntryNameTest()
        {
            Assert.AreEqual("book", _serializer.GetBibTeXEntryName( new BibTeXBook()));
        }

        [TestMethod]
        public void GetBibTeXFieldsTest()
        {

            Assert.AreEqual(11, _serializer.GetBibTeXFields(new BibTeXBook()).Count());

        }

        [TestMethod]
        public void GetBibTeXFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "author", "title", "publisher", "year", "volume", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name).ToArray();
            var fieldNames = _serializer.GetBibTeXFieldNames(new BibTeXBook()).OrderBy((name) => name).ToArray();

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

            var propertyInfo = _serializer.GetBibTeXFieldByName(book, "author");

            Assert.AreEqual("author = \"B. T. Milnes\"", _serializer.SerializeBibTeXField(book, propertyInfo));
        }
    }
}