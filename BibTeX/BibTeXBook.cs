using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("book")]
    public class BibTeXBook : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        [BibTeXRequiredFieldGroup("author/editor")]
        public string Author { get; set; }

        [BibTeXFieldName("editor")]
        [BibTeXRequiredFieldGroup("author/editor")]
        public string Editor { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("publisher")]
        public string Publisher { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

        [BibTeXFieldName("volume")]
        [BibTeXOptionalField]
        public string Volume { get; set; }

        [BibTeXFieldName("number")]
        [BibTeXOptionalField]
        public string Number { get; set; }

        [BibTeXFieldName("series")]
        [BibTeXOptionalField]
        public string Series { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("edition")]
        [BibTeXOptionalField]
        public string Edition { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }
        
        public BibTeXBook() { }

        public BibTeXBook(string author, string title, string publisher, string year)
        {
            Author = author;
            Title = title;
            Publisher = publisher;
            Year = year;
        }
    }
}
