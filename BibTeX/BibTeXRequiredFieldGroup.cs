using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BibTeXRequiredFieldGroup : Attribute
    {
        public string Name { get; }

        public BibTeXRequiredFieldGroup(string name)
        {
            Name = name;
        }
    }
}
