using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace CSharpColumnIndenter
{
    public class SyntaxTokenComparer : IEqualityComparer<SyntaxToken>
    {
        public bool Equals(SyntaxToken x, SyntaxToken y)
        {
            return x.RawKind.Equals(y.RawKind);
        }

        public int GetHashCode(SyntaxToken obj)
        {
            return obj.RawKind.GetHashCode();
        }
    }
}