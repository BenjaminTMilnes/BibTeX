using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX.Tests
{
    public class BibTeXAttributeReaderTests
    {
        private BibTeXAttributeReader _attributeReader;

        public BibTeXAttributeReaderTests()
        {
            _attributeReader = new BibTeXAttributeReader();
        }

        [Fact]
        public void GetBibTeXEntryNameTest()
        {
            Assert.Equal("book", _attributeReader.GetBibTeXEntryName(new BibTeXBook()));
        }

        [Fact]
        public void GetBibTeXFieldsTest()
        {
            Assert.Equal(13, _attributeReader.GetBibTeXFields(new BibTeXBook()).Count());
        }

        [Fact]
        public void GetBibTeXFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "author", "editor", "title", "publisher", "year", "volume", "number", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name);
            var fieldNames = _attributeReader.GetBibTeXFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.True(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [Fact]
        public void GetBibTeXOptionalFieldsTest()
        {
            Assert.Equal(8, _attributeReader.GetBibTeXOptionalFields(new BibTeXBook()).Count());
        }

        [Fact]
        public void GetBibTeXOptionalFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "volume", "number", "series", "address", "edition", "month", "note", "key" }).OrderBy((name) => name);
            var fieldNames = _attributeReader.GetBibTeXOptionalFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.True(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [Fact]
        public void GetBibTeXRequiredFieldsTest()
        {
            Assert.Equal(3, _attributeReader.GetBibTeXRequiredFields(new BibTeXBook()).Count());
        }

        [Fact]
        public void GetBibTeXRequiredFieldNamesTest()
        {
            var expectedFieldNames = (new string[] { "title", "publisher", "year" }).OrderBy((name) => name);
            var fieldNames = _attributeReader.GetBibTeXRequiredFieldNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.True(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [Fact]
        public void GetBibTeXRequiredFieldGroupNamesTest()
        {
            var expectedGroupNames = (new string[] { "author/editor" }).OrderBy((name) => name);
            var groupNames = _attributeReader.GetBibTeXRequiredFieldGroupNames(new BibTeXBook()).OrderBy((name) => name);

            Assert.True(expectedGroupNames.SequenceEqual(groupNames));
        }

        [Fact]
        public void GetFieldsInBibTeXRequiredFieldGroupTest()
        {
            var expectedFieldNames = (new string[] { "author", "editor" }).OrderBy((name) => name);
            var fields = _attributeReader.GetFieldsInBibTeXRequiredFieldGroup(new BibTeXBook(), "author/editor");
            var fieldNames = _attributeReader.GetBibTeXFieldNames(fields).OrderBy((name) => name);

            Assert.True(expectedFieldNames.SequenceEqual(fieldNames));
        }

        [Fact]
        public void GetBibTeXFieldByNameTest()
        {
            var book = new BibTeXBook();

            book.Author = "B. T. Milnes";

            var propertyInfo = _attributeReader.GetBibTeXFieldByName(book, "author");

            Assert.Equal(book.Author, propertyInfo.GetValue(book));
        }
    }
}
