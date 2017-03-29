using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("misc")]
    public class BibTeXMiscellaneous : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        [BibTeXOptionalField]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        [BibTeXOptionalField]
        public string Title { get; set; }

        [BibTeXFieldName("howpublished")]
        [BibTeXOptionalField]
        public string HowPublished { get; set; }

        [BibTeXFieldName("year")]
        [BibTeXOptionalField]
        public string Year { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        public BibTeXMiscellaneous() { }
    }
}
