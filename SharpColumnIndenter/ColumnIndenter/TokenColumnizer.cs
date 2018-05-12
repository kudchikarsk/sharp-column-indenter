using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using SharpColumnIndenter.Languages;
using SharpColumnIndenter.Languages.Base;

namespace SharpColumnIndenter.ColumnIndenter
{
    internal class TokenColumnizer
    {
        private string _actualText;
        private IToken[][] _tokensByLine;
        private IToken[] _commonTokens;
        private TokenRow[] _tokenRows;
        private IEqualityComparer<IToken> _comparator;

        private int [] _columnIndexByLine;


        public TokenColumnizer(IToken[][] tokenByLine, IToken[] commonTokens, string actualText, IEqualityComparer<IToken> comparator)
        {
            _tokensByLine = tokenByLine;
            _commonTokens = commonTokens;
            _actualText = actualText;
            _comparator = comparator;
            _tokenRows = new TokenRow[_tokensByLine.Count()];
            for (int i = 0; i < _tokenRows.Count(); i++)
                _tokenRows[i] = new TokenRow();

            _columnIndexByLine = new int[_tokensByLine.Count()];
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

            CopyTokensUptoCommontToken(new BaseToken(""));
        }

        private void CopyCurrentTokens()
        {
            for (int i = 0; i < _tokensByLine.Count(); i++)
            {
                var token = GetCurrentToken(i);
                if (token != null)
                {
                    _tokenRows[i].AppendToLastColumn(token);
                }
                
            }
        }

        private void AddColumn()
        {
            for (int i = 0; i < _tokensByLine.Count(); i++)
            {
                _tokenRows[i].AddColumn();
            }
        }

        private void CopyTokensUptoCommontToken(IToken commonToken)
        {
            for (int lineIndex = 0; lineIndex < _tokensByLine.Count(); lineIndex++)
            {
                var token = GetNextColumnToken(lineIndex);
                while (token != null && ( string.IsNullOrEmpty(commonToken.Text) || !_comparator.Equals(token,commonToken)))
                {
                    _tokenRows[lineIndex].AppendToLastColumn(token);
                    token = GetNextColumnToken(lineIndex);
                }
            }
        }

        public override string ToString()
        {
            var lineContent = new string[_tokensByLine.Count()];

            for (int i = 0; i < _tokenRows.First().Columns.Count(); i++)
            {                
                var linesColumnText = _tokenRows.Select(l => string.Join(" ", l.GetColumn(i).Tokens.Select(t => t.Text))).ToArray();
                var maxLength = linesColumnText.Select(t=>t.Length).Max();
                for (int j = 0; j < _tokensByLine.Count(); j++)
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

        private IToken GetNextColumnToken(int lineIndex)
        {
            _columnIndexByLine[lineIndex]++;
            return GetCurrentToken(lineIndex);
        }

        private IToken GetCurrentToken(int lineIndex)
        {
            try
            {
                return _tokensByLine[lineIndex][_columnIndexByLine[lineIndex]];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }
}