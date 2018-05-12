using SharpColumnIndenter.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpColumnIndenter.ColumnIndenter
{
    public abstract class Token : IToken
    {
        abstract public string Text { get; }

        public override string ToString()
        {
            return Text;
        }
    }
}
