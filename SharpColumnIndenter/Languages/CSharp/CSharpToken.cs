using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpColumnIndenter.Languages.CSharp
{
    class CSharpToken : IToken
    {
        private SyntaxToken _token;

        public CSharpToken(SyntaxToken token)
        {
            _token = token;
        }

        public string Text => _token.Text;
        public int RawKind => _token.RawKind;
    }
}
