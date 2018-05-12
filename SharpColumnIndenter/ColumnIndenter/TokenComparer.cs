using System.Collections.Generic;
using SharpColumnIndenter.Languages;
using Microsoft.CodeAnalysis;

namespace SharpColumnIndenter.ColumnIndenter
{
    public class TokenComparer : IEqualityComparer<IToken>
    {
        public bool Equals(IToken x, IToken y)
        {
            return x.Text.Equals(y.Text);
        }

        public int GetHashCode(IToken obj)
        {
            return obj.Text.GetHashCode();
        }
    }
}