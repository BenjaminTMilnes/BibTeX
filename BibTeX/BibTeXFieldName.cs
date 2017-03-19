using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BibTeXFieldName : Attribute
    {
        public string FieldName { get; private set; }

        public BibTeXFieldName(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
