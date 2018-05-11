using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace CSharpColumnIndenter
{
    public class SyntaxTokenColumn
    {
        private List<SyntaxToken> _tokens;

        public SyntaxTokenColumn()
        {
            _tokens = new List<SyntaxToken>();
        }

        public List<SyntaxToken> Tokens => _tokens.ToList();

        public void Add(SyntaxToken syntaxToken)
        {
            _tokens.Add(syntaxToken);
        }
    }
}