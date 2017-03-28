using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public interface IBibTeXEntry
    {
        string CitationKey { get; set; }
    }
}
