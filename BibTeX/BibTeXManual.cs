using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("manual")]
    public class BibTeXManual : BibTeXEntry
    {
        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("author")]
        [BibTeXOptionalField]
        public string Author { get; set; }

        [BibTeXFieldName("organization")]
        [BibTeXOptionalField]
        public string Organization { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("edition")]
        [BibTeXOptionalField]
        public string Edition { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        [BibTeXFieldName("year")]
        [BibTeXOptionalField]
        public string Year { get; set; }

        public BibTeXManual() { }

        public BibTeXManual(string title)
        {
            Title = title;
        }
    }
}
