using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public class UnknownBibTeXEntryException : Exception { }

    public class BibTeXSerializer
    {
        public Type GetBibTeXEntryType(IBibTeXEntry entry)
        {
            if (entry is BibTeXBook) return typeof(BibTeXBook);

            throw new UnknownBibTeXEntryException();
        }

        public string GetBibTeXEntryName(Type type)
        {
            return ((BibTeXEntryName)Attribute.GetCustomAttribute(type, typeof(BibTeXEntryName))).EntryName;
        }
    }
}
