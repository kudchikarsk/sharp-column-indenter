using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace CSharpColumnIndenter
{
    public class SyntaxTokenComparer : IEqualityComparer<SyntaxToken>
    {
        public bool Equals(SyntaxToken x, SyntaxToken y)
        {
            return x.Text.Equals(y.Text);
        }

        public int GetHashCode(SyntaxToken obj)
        {
            return obj.Text.GetHashCode();
        }
    }
}