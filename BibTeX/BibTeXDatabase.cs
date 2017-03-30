using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public class BibTeXDatabase
    {
        public IList<IBibTeXEntry> Entries { get; set; }

        public BibTeXDatabase()
        {
            Entries = new List<IBibTeXEntry>();
        }

        #region GetEntriesByType

        public IEnumerable<IBibTeXEntry> GetBooks()
        {
            return Entries.Where((entry) => entry is BibTeXBook);
        }

        #endregion
    }
}
