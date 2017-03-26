using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("conference")]
    public class BibTeXConference : BibTeXInProceedings
    {
        public BibTeXConference() { }

        public BibTeXConference(string author, string title, string bookTitle, string year) : base(author, title, bookTitle, year) { }
    }
}
