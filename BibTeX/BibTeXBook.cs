using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("book")]
    public class BibTeXBook
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("publisher")]
        public string Publisher { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

    }
}
