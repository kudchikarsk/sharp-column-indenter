using Microsoft.CodeAnalysis;
using SharpColumnIndenter.ColumnIndenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpColumnIndenter.Languages.CSharp
{
    class CSharpToken : Token
    {
        private SyntaxToken _token;

        public CSharpToken(SyntaxToken token)
        {
            _token = token;
        }

        public override string Text => _token.Text;

        public int RawKind => _token.RawKind;
    }
}
