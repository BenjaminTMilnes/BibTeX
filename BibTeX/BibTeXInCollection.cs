using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("incollection")]
    public class BibTeXInCollection : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("booktitle")]
        public string BookTitle { get; set; }

        [BibTeXFieldName("publisher")]
        public string Publisher { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

        [BibTeXFieldName("editor")]
        [BibTeXOptionalField]
        public string Editor { get; set; }

        [BibTeXFieldName("volume")]
        [BibTeXOptionalField]
        public string Volume { get; set; }

        [BibTeXFieldName("series")]
        [BibTeXOptionalField]
        public string Series { get; set; }

        [BibTeXFieldName("type")]
        [BibTeXOptionalField]
        public string Type { get; set; }

        [BibTeXFieldName("chapter")]
        [BibTeXOptionalField]
        public string Chapter { get; set; }

        [BibTeXFieldName("pages")]
        [BibTeXOptionalField]
        public string Pages { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("edition")]
        [BibTeXOptionalField]
        public string Edition { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        public BibTeXInCollection() { }

        public BibTeXInCollection(string author, string title, string bookTitle, string publisher, string year)
        {
            Author = author;
            Title = title;
            BookTitle = bookTitle;
            Publisher = publisher;
            Year = year;
        }
    }
}
