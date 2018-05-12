using System;
using System.Collections.Generic;
using System.Linq;
using SharpColumnIndenter.Languages;
using Microsoft.CodeAnalysis;

namespace SharpColumnIndenter.ColumnIndenter
{
    public class TokenColumn
    {
        private List<IToken> _tokens;

        public TokenColumn()
        {
            _tokens = new List<IToken>();
        }

        public List<IToken> Tokens => _tokens.ToList();

        public void Add(IToken syntaxToken)
        {
            _tokens.Add(syntaxToken);
        }
    }
}