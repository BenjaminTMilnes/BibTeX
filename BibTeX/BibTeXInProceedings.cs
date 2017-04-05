using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("inproceedings")]
    public class BibTeXInProceedings : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("booktitle")]
        public string BookTitle { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

        [BibTeXFieldName("editor")]
        [BibTeXOptionalField]
        public string Editor { get; set; }

        [BibTeXFieldName("volume")]
        [BibTeXOptionalField]
        public string Volume { get; set; }

        [BibTeXFieldName("number")]
        [BibTeXOptionalField]
        public string Number { get; set; }

        [BibTeXFieldName("series")]
        [BibTeXOptionalField]
        public string Series { get; set; }

        [BibTeXFieldName("pages")]
        [BibTeXOptionalField]
        public string Pages { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        [BibTeXFieldName("organization")]
        [BibTeXOptionalField]
        public string Organization { get; set; }

        [BibTeXFieldName("publisher")]
        [BibTeXOptionalField]
        public string Publisher { get; set; }

        public BibTeXInProceedings() { }

        public BibTeXInProceedings(string author, string title, string bookTitle, string year)
        {
            Author = author;
            Title = title;
            BookTitle = bookTitle;
            Year = year;
        }
    }
}
