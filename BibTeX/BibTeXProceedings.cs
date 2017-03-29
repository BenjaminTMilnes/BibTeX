using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [BibTeXEntryName("proceedings")]
    public class BibTeXProceedings : BibTeXEntry
    {
        [BibTeXFieldName("title")]
        public string Title { get; set; }

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

        public BibTeXProceedings() { }

        public BibTeXProceedings(string title, string year)
        {
            Title = title;
            Year = year;
        }
    }
}
