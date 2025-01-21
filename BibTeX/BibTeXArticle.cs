using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("article")]
    public class BibTeXArticle : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("journal")]
        public string Journal { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

        [BibTeXFieldName("volume")]
        public string Volume { get; set; }

        [BibTeXFieldName("number")]
        [BibTeXOptionalField]
        public string Number { get; set; }

        [BibTeXFieldName("pages")]
        [BibTeXOptionalField]
        public string Pages { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        [BibTeXFieldName("url")]
        [BibTeXOptionalField]
        public string URL { get; set; }

        [BibTeXFieldName("doi")]
        [BibTeXOptionalField]
        public string DOI { get; set; }

        public BibTeXArticle() { }

        public BibTeXArticle(string author, string title, string journal, string year, string volume)
        {
            Author = author;
            Title = title;
            Journal = journal;
            Year = year;
            Volume = volume;
        }
    }
}
