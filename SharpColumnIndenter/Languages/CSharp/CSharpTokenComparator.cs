using System.Collections.Generic;

namespace SharpColumnIndenter.Languages.CSharp
{
    public class CSharpTokenComparator : IEqualityComparer<IToken>
    {
        public bool Equals(IToken x, IToken y)
        {
            var xx = (CSharpToken)x;
            var yy = (CSharpToken)y;

            return xx.RawKind.Equals(yy.RawKind);
        }

        public int GetHashCode(IToken obj)
        {
            return ((CSharpToken)obj).RawKind.GetHashCode();
        }
    }
}