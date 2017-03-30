using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("techreport")]
    public class BibTeXTechReport : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("institution")]
        public string Institution { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

        [BibTeXFieldName("type")]
        [BibTeXOptionalField]
        public string Type { get; set; }

        [BibTeXFieldName("number")]
        [BibTeXOptionalField]
        public string Number { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        public BibTeXTechReport() { }

        public BibTeXTechReport(string author, string title, string institution, string year)
        {
            Author = author;
            Title = title;
            Institution = institution;
            Year = year;
        }
    }
}
