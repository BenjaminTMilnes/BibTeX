using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("inbook")]
    public class BibTeXInBook : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        [BibTeXRequiredFieldGroup("author/editor")]
        public string Author { get; set; }

        [BibTeXFieldName("editor")]
        [BibTeXRequiredFieldGroup("author/editor")]
        public string Editor { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("chapter")]
        [BibTeXRequiredFieldGroup("chapter/pages")]
        public string Chapter { get; set; }

        [BibTeXFieldName("pages")]
        [BibTeXRequiredFieldGroup("chapter/pages")]
        public string Pages { get; set; }

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

        [BibTeXFieldName("type")]
        [BibTeXOptionalField]
        public string Type { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("edition")]
        [BibTeXOptionalField]
        public string Edition { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        public BibTeXInBook() { }

        public BibTeXInBook(string author, string title, string chapter, string publisher, string year)
        {
            Author = author;
            Title = title;
            Chapter = chapter;
            Publisher = publisher;
            Year = year;
        }
    }
}
