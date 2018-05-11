using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpColumnIndenter
{
    public class SyntaxTokenColumnManager
    {
        private List<SyntaxTokenColumn> _columns;

        public List<SyntaxTokenColumn> Columns => _columns.ToList();

        public SyntaxTokenColumnManager()
        {
            _columns = new List<SyntaxTokenColumn>();
        }

        public void AppendToLastColumn(SyntaxToken syntaxToken)
        {
            _columns.Last().Add(syntaxToken);
        }

        public void AddColumn()
        {
            _columns.Add(new SyntaxTokenColumn());
        }

        public SyntaxTokenColumn GetColumn(int i)
        {
            return _columns.ElementAt(i);
        }
    }
}