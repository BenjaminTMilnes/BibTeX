using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public abstract class BibTeXEntry : IBibTeXEntry
    {
        [BibTeXFieldName("note")]
        [BibTeXOptionalField]
        public string Note { get; set; }

        [BibTeXFieldName("key")]
        [BibTeXOptionalField]
        public string Key { get; set; }
    }
}
