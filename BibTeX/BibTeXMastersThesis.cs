using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("mastersthesis")]
    public class BibTeXMastersThesis : BibTeXEntry
    {
        [BibTeXFieldName("author")]
        public string Author { get; set; }

        [BibTeXFieldName("title")]
        public string Title { get; set; }

        [BibTeXFieldName("school")]
        public string School { get; set; }

        [BibTeXFieldName("year")]
        public string Year { get; set; }

        [BibTeXFieldName("type")]
        [BibTeXOptionalField]
        public string Type { get; set; }

        [BibTeXFieldName("address")]
        [BibTeXOptionalField]
        public string Address { get; set; }

        [BibTeXFieldName("month")]
        [BibTeXOptionalField]
        public BibTeXMonth Month { get; set; }

        public BibTeXMastersThesis() { }

        public BibTeXMastersThesis(string author, string title, string school, string year)
        {
            Author = author;
            Title = title;
            School = school;
            Year = year;
        }
    }
}
