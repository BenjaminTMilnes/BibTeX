using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX.Tests
{
    public class BibTeXValidatorTests
    {
        private BibTeXValidator _validator;

        public BibTeXValidatorTests()
        {
            _validator = new BibTeXValidator();
        }

        [Fact]
        public void ValidateBibTeXRequiredFieldFailTest()
        {
            var book = new BibTeXBook("a", "", "a", "2000");

            Assert.Throws<RequiredFieldException>(() => _validator.ValidateBibTeXEntry(book));
        }

        [Fact]
        public void ValidateBibTeXRequiredFieldGroupFailTest()
        {
            var book = new BibTeXBook("", "a", "a", "2000");

            Assert.Throws<RequiredFieldGroupException>(() => _validator.ValidateBibTeXEntry(book));
        }

        [Fact]
        public void ValidateBibTeXEntryPassTest()
        {
            var book = new BibTeXBook("a", "a", "a", "2000");

            Assert.True(_validator.ValidateBibTeXEntry(book));
        }
    }
}
