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
            Assert.AreEqual("book", _serializer.GetBibTeXEntryName(_serializer.GetBibTeXEntryType(new BibTeXBook())));
        }
    }
}
