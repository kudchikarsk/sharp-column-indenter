using System.Collections.Generic;

namespace SharpColumnIndenter.Languages
{
    public interface ILanguage
    {
        IEqualityComparer<IToken> Comparer { get; }
        IToken[] GetTokens(string text);        
    }
}