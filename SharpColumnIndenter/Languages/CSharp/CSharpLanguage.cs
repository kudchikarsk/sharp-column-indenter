using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpColumnIndenter.Languages.CSharp
{
    class CSharpLanguage:ILanguage
    {
        private CSharpTokenComparator _comparator;
        public CSharpLanguage()
        {
            _comparator = new CSharpTokenComparator();
        }

        public IEqualityComparer<IToken> Comparer => _comparator;

        public IToken[] GetTokens(string text)
        {
            var syntaxStatement = SyntaxFactory.ParseStatement(text);
            return syntaxStatement.DescendantTokens().Select(t => new CSharpToken(t)).ToArray();
        }
    }
}
