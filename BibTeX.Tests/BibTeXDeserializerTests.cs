using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BibTeX.Tests
{
    public class BibTeXDeserializerTests
    {
        [Theory]
        [InlineData("a", "a")]
        [InlineData("aa", "aa")]
        [InlineData("aaa", "aaa")]
        [InlineData("abc", "abc")]
        [InlineData("A", "A")]
        [InlineData("AA", "AA")]
        [InlineData("AAA", "AAA")]
        [InlineData("ABC", "ABC")]
        [InlineData("title", "title")]
        [InlineData("author", "author")]
        [InlineData("year", "year")]
        [InlineData("month", "month")]
        [InlineData("journal", "journal")]
        [InlineData("volume", "volume")]
        [InlineData("number", "number")]
        [InlineData("publisher", "publisher")]
        [InlineData("URL", "URL")]
        [InlineData("ISSN", "ISSN")]
        [InlineData("DOI", "DOI")]
        [InlineData("a1", "a")]
        [InlineData("aa1", "aa")]
        [InlineData("aaa1", "aaa")]
        [InlineData("title1", "title")]
        [InlineData("title_", "title")]
        [InlineData("title-", "title")]
        [InlineData("title=", "title")]
        public void GetFieldNameTest1(string input, string fieldName)
        {
            var bibtexDeserializer = new BibTeXDeserializer();

            var output = bibtexDeserializer.GetFieldName(input, new Marker());

            Assert.Equal(fieldName, output);
        }

        [Theory]
        [InlineData("{a}", "a")]
        [InlineData("{aa}", "aa")]
        [InlineData("{aaa}", "aaa")]
        [InlineData("{abc}", "abc")]
        [InlineData("{2025}", "2025")]
        [InlineData("{January}", "January")]
        [InlineData("{1-10}", "1-10")]
        [InlineData("{http://www.benjamintmilnes.com}", "http://www.benjamintmilnes.com")]
        public void GetFieldValueTest1(string input, string fieldValue)
        {
            var bibtexDeserializer = new BibTeXDeserializer();

            var output = bibtexDeserializer.GetFieldValue(input, new Marker());

            Assert.Equal(fieldValue, output);
        }

        [Theory]
        [InlineData("a={b}", "a", "b")]
        [InlineData("abc={def}", "abc", "def")]
        [InlineData("title={abc}", "title", "abc")]
        [InlineData("author={B. T. Milnes}", "author", "B. T. Milnes")]
        [InlineData("year={2025}", "year", "2025")]
        [InlineData("month={January}", "month", "January")]
        [InlineData("journal={abc}", "journal", "abc")]
        [InlineData("volume={123}", "volume", "123")]
        [InlineData("number={123}", "number", "123")]
        [InlineData("publisher={abc}", "publisher", "abc")]
        [InlineData("URL={http://www.benjamintmilnes.com}", "URL", "http://www.benjamintmilnes.com")]
        public void GetFieldTest1(string input, string fieldName, string fieldValue)
        {
            var bibtexDeserializer = new BibTeXDeserializer();

            var output = bibtexDeserializer.GetField(input, new Marker());

            Assert.Equal(fieldName, output.Item1);
            Assert.Equal(fieldValue, output.Item2);
        }
    }
}
