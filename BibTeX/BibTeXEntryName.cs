using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BibTeXEntryName : Attribute
    {
        public string EntryName { get; private set; }

        public BibTeXEntryName(string entryName)
        {
            EntryName = entryName;
        }
    }
}
