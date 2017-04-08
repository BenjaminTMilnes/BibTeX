using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibTeX.Tests
{
    [TestClass]
    public class BibTeXValidatorTests
    {
        private BibTeXValidator _validator;

        public BibTeXValidatorTests()
        {
            _validator = new BibTeXValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldException))]
        public void ValidateBibTeXEntryFailTest()
        {
            var book = new BibTeXBook("a", "", "a", "2000");

            _validator.ValidateBibTeXEntry(book);
        }

        [TestMethod]
        public void ValidateBibTeXEntryPassTest()
        {
            var book = new BibTeXBook("a", "a", "a", "2000");

            Assert.IsTrue(_validator.ValidateBibTeXEntry(book));
        }
    }
}
