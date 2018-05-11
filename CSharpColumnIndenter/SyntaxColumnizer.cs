using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace CSharpColumnIndenter
{
    internal class SyntaxColumnizer
    {
        private string _actualText;
        private SyntaxToken[][] _tokenByLine;
        private SyntaxToken[] _commonTokens;
        private SyntaxTokenColumnManager[] _syntaxTokenColumnsByLine;
        private IEqualityComparer<SyntaxToken> _comparator;

        private int [] _columnIndexByLine;


        public SyntaxColumnizer(SyntaxToken[][] tokenByLine, SyntaxToken[] commonTokens, string actualText, IEqualityComparer<SyntaxToken> comparator)
        {
            _tokenByLine = tokenByLine;
            _commonTokens = commonTokens;
            _actualText = actualText;
            _comparator = comparator;
            _syntaxTokenColumnsByLine = new SyntaxTokenColumnManager[_tokenByLine.Count()];
            for (int i = 0; i < _syntaxTokenColumnsByLine.Count(); i++)
                _syntaxTokenColumnsByLine[i] = new SyntaxTokenColumnManager();

            _columnIndexByLine = new int[_tokenByLine.Count()];
            for (int i = 0; i < _columnIndexByLine.Count(); i++)
                _columnIndexByLine[i] = -1;
        }

        public void Execute()
        {
            AddColumn();

            foreach (var commonToken in _commonTokens)
            {
                CopyTokensUptoCommontToken(commonToken);
                AddColumn();
                CopyCurrentTokens();
                AddColumn();
            }

            CopyTokensUptoCommontToken(new SyntaxToken());
        }

        private void CopyCurrentTokens()
        {
            for (int i = 0; i < _tokenByLine.Count(); i++)
            {
                var token = GetCurrentToken(i);
                if (token != null)
                {
                    _syntaxTokenColumnsByLine[i].AppendToLastColumn((SyntaxToken)token);
                }
                
            }
        }

        private void AddColumn()
        {
            for (int i = 0; i < _tokenByLine.Count(); i++)
            {
                _syntaxTokenColumnsByLine[i].AddColumn();
            }
        }

        private void CopyTokensUptoCommontToken(SyntaxToken commonToken)
        {
            for (int lineIndex = 0; lineIndex < _tokenByLine.Count(); lineIndex++)
            {
                var token = GetNextColumnToken(lineIndex);
                while (token != null && ( string.IsNullOrEmpty(commonToken.Text) || !_comparator.Equals(token.Value,commonToken)))
                {
                    _syntaxTokenColumnsByLine[lineIndex].AppendToLastColumn((SyntaxToken)token);
                    token = GetNextColumnToken(lineIndex);
                }
            }
        }

        public override string ToString()
        {
            var lineContent = new string[_tokenByLine.Count()];

            for (int i = 0; i < _syntaxTokenColumnsByLine.First().Columns.Count(); i++)
            {                
                var linesColumnText = _syntaxTokenColumnsByLine.Select(l => string.Join(" ", l.GetColumn(i).Tokens.Select(t => t.Text))).ToArray();
                var maxLength = linesColumnText.Select(t=>t.Length).Max();
                for (int j = 0; j < _tokenByLine.Count(); j++)
                {
                    lineContent[j] += Pad(linesColumnText[j], maxLength);
                }
            }

            var lines = Regex.Split(_actualText.Trim(), @"(\r\n|\r|\n)").ToList(); ;
            lines.RemoveAll(l => string.IsNullOrWhiteSpace(l));
            var indention = Regex.Match(lines[1], @"^(\s*)\w").Value;
            if (!string.IsNullOrEmpty(indention))
                indention = indention.Remove(indention.Count() - 1, 1);

            var spaceBeforeLine = Regex.Match(_actualText, @"^(\s*)\w").Value;
            if (!string.IsNullOrEmpty(spaceBeforeLine))
                spaceBeforeLine = spaceBeforeLine.Remove(spaceBeforeLine.Count() - 1, 1);

            var spaceAfterLine = Regex.Match(_actualText, @"\w(\s*)$").Value;
            if (!string.IsNullOrEmpty(spaceAfterLine))
                spaceAfterLine = spaceAfterLine.Remove(0, 1);

            return $"{spaceBeforeLine}{string.Join("\r\n"+indention,lineContent)}{spaceAfterLine}\n";
        }

        private string Pad(string text, int maxLength)
        {
            return text + string.Join("", Enumerable.Repeat(" ", maxLength + 1 - text.Length));
        }

        private SyntaxToken? GetNextColumnToken(int lineIndex)
        {
            _columnIndexByLine[lineIndex]++;
            return GetCurrentToken(lineIndex);
        }

        private SyntaxToken? GetCurrentToken(int lineIndex)
        {
            try
            {
                return _tokenByLine[lineIndex][_columnIndexByLine[lineIndex]];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }
}