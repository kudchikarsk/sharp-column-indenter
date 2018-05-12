using SharpColumnIndenter.Languages;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpColumnIndenter.ColumnIndenter
{
    public class TokenRow
    {
        private List<TokenColumn> _columns;

        public List<TokenColumn> Columns => _columns.ToList();

        public TokenRow()
        {
            _columns = new List<TokenColumn>();
        }

        public void AppendToLastColumn(IToken syntaxToken)
        {
            _columns.Last().Add(syntaxToken);
        }

        public void AddColumn()
        {
            _columns.Add(new TokenColumn());
        }

        public TokenColumn GetColumn(int i)
        {
            return _columns.ElementAt(i);
        }
    }
}