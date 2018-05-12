using SharpColumnIndenter.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpColumnIndenter.Languages.Base
{
    class BaseLanguage : ILanguage
    {
        BaseTokenComparator _comparer;

        public BaseLanguage()
        {
            _comparer = new BaseTokenComparator();
        }

        public IEqualityComparer<IToken> Comparer => _comparer;

        public IToken[] GetTokens(string text)
        {
            var words = Regex.Split(text, @"\s+").ToList();
            words.RemoveAll(w => string.IsNullOrWhiteSpace(w));
            var tokens = words.Select(w => new BaseToken(w));
            return tokens.ToArray();
        }
    }
}
