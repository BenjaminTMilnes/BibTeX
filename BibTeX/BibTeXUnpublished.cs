using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("unpublished")]
    public class BibTeXUnpublished : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("year")]
        [BibTeXOptionalField]
        public string Year { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        public BibTeXUnpublished() { }

        public BibTeXUnpublished(string author, string title)
        {
            Author = author;
            Title = title;
        }
    }
}
