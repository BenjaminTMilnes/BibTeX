using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("booklet")]
    public class BibTeXBooklet : BibTeXEntry
    {
        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("author")]
        [BibTeXOptionalField]
        public string Author { get; set; }

        [BibTeXFieldName("howpublished")]
        [BibTeXOptionalField]
        public string HowPublished { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        [BibTeXFieldName("year")]
        [BibTeXOptionalField]
        public string Year { get; set; }
        
        public BibTeXBooklet() { }

        public BibTeXBooklet(string title)
        {
            Title = title;
        }
    }
}
