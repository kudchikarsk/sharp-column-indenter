using SharpColumnIndenter.Languages;
using System.Collections;
using System.Collections.Generic;

namespace SharpColumnIndenter.Languages.Base
{
    public class BaseTokenComparator : IEqualityComparer<IToken>
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