using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibTeX.Tests
{
    [TestClass]
    public class BibTeXAttributeReaderTests
    {
        private BibTeXAttributeReader _attributeReader;

        public BibTeXAttributeReaderTests()
        {
            _attributeReader = new BibTeXAttributeReader();
        }

        [TestMethod]
        public void GetBibTeXEntryNameTest()
        {
            Assert.AreEqual("book", _attributeReader.GetBibTeXEntryName(new BibTeXBook()));
        }

        [TestMethod]
        public void GetBibTeXFieldsTest()
        {
            Assert.AreEqual(11, _attributeReader.GetBibTeXFields(new BibTeXBook()).Count());
        }

        [TestMethod]
        public void GetBibTeXFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "author", "title", "publisher", "year", "volume", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name);
            var fieldNames = _attributeReader.GetBibTeXFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.IsTrue(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [TestMethod]
        public void GetBibTeXOptionalFieldsTest()
        {
            Assert.AreEqual(7, _attributeReader.GetBibTeXOptionalFields(new BibTeXBook()).Count());
        }

        [TestMethod]
        public void GetBibTeXOptionalFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "volume", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name);
            var fieldNames = _attributeReader.GetBibTeXOptionalFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.IsTrue(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [TestMethod]
        public void GetBibTeXRequiredFieldsTest()
        {
            Assert.AreEqual(4, _attributeReader.GetBibTeXRequiredFields(new BibTeXBook()).Count());
        }

        [TestMethod]
        public void GetBibTeXRequiredFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "author", "title", "publisher", "year" }).OrderBy((name) => name);
            var fieldNames = _attributeReader.GetBibTeXRequiredFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.IsTrue(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [TestMethod]
        public void GetBibTeXFieldByNameTest()
        {
            var book = new BibTeXBook();

            book.Author = "B. T. Milnes";

            var propertyInfo = _attributeReader.GetBibTeXFieldByName(book, "author");

            Assert.AreEqual(book.Author, propertyInfo.GetValue(book));
        }
    }
}
