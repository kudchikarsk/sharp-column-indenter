using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpColumnIndenter
{
    class CSharpColumnIndenter
    {
        public CSharpColumnIndenter()
        {

        }

        public string Apply(string text)
        {
            var tokenByLine = GetTokenByLine(text);
            var commonTokens = GetCommonTokens(tokenByLine);
            var columnizedTokens = GetColumnizedTokens(tokenByLine, commonTokens);
            var columnizedText = GetString(columnizedTokens,text);
            CheckCode(columnizedText, text);
            return columnizedText;
        }

        private void CheckCode(string columnizedText, string text)
        {
            var modifyCode = GetTokens(columnizedText).Select(t=>t.Text).ToList();
            var originalCode = GetTokens(text).Select(t=>t.Text).ToList();
            var isNotEqual = modifyCode.Except(originalCode).ToList().Any() || originalCode.Except(modifyCode).ToList().Any() || originalCode.Count!=modifyCode.Count;
            if (isNotEqual) throw new Exception("CSharp column indenter is trying to change code this bug please report here.");
        }

        private string GetString(SyntaxToken[][][] columnizedTokens,string actualText)
        {
            var lines = Regex.Split(actualText.Trim(), @"(\r\n|\r|\n)").ToList(); ;
            lines.RemoveAll(l => string.IsNullOrWhiteSpace(l));
            var indention = Regex.Match(lines[1], @"^(\s*)\w").Value;
            if (!string.IsNullOrEmpty(indention))
                indention =indention.Remove(indention.Count()-1,1);

            var spaceBeforeLine = Regex.Match(actualText, @"^(\s*)\w").Value;
            if (!string.IsNullOrEmpty(spaceBeforeLine))
                spaceBeforeLine = spaceBeforeLine.Remove(spaceBeforeLine.Count() - 1, 1);

            var spaceAfterLine = Regex.Match(actualText, @"$\w(\s*)").Value;
            if(!string.IsNullOrEmpty(spaceAfterLine))
                spaceAfterLine = spaceAfterLine.Remove(0, 1);

            var columnsByLine = columnizedTokens.Select(l => l.Select(c => string.Join(" ", c.Select(t=>t.Text))).ToArray()).ToArray();
            var columnsCount = columnsByLine.First().Count();
            var columnsMaxLength = new int[columnsCount];
            for (int i = 0; i < columnsCount; i++)
            {
                var maxColumnSize = 0;
                for (int j = 0; j < columnsByLine.Count(); j++)
                {
                    maxColumnSize = Math.Max(maxColumnSize, columnsByLine[j][i].Length);
                }
                columnsMaxLength[i] = maxColumnSize;
            }
            for (int i = 0; i < columnsMaxLength.Count(); i++)
            {
                var columnMaxLength = columnsMaxLength[i];
                for (int j = 0; j < columnsByLine.Count(); j++)
                {
                    var charList = columnsByLine[j][i].ToList();
                    if (charList.Count < columnMaxLength)
                    {
                        charList.AddRange(Enumerable.Repeat(' ', columnMaxLength - (charList.Count - 1)));
                        columnsByLine[j][i] = new string(charList.ToArray());
                    }
                }
            }

            return $"{spaceBeforeLine}{string.Join("\r\n"+ indention, columnsByLine.Select(l => string.Join("\t", l)))}{spaceAfterLine}\r\n";
        }

        private SyntaxToken[][][] GetColumnizedTokens(SyntaxToken[][] tokenByLine, SyntaxToken[] commonTokens)
        {
            var columnizedTokens = new SyntaxToken[tokenByLine.Count()][][];
            for (int i = 0; i < columnizedTokens.Count(); i++)
            {
                columnizedTokens [i]= new SyntaxToken[commonTokens.Count()][];
            }
            
            var columnTokensByLine = new List<SyntaxToken>[tokenByLine.Count()];
            for (int i = 0; i < columnTokensByLine.Count(); i++)
            {
                columnTokensByLine[i] = new List<SyntaxToken>();
            }
            var commonTokenIndexByLine = new int[tokenByLine.Count()];

            for (int columnIndex = 0; columnIndex < tokenByLine.Max(l=>l.Count()); columnIndex++)
            {                    
                for (int lineIndex = 0; lineIndex < tokenByLine.Count(); lineIndex++)
                {
                    var token = tokenByLine[lineIndex].ElementAtOrDefault(columnIndex);
                    if (string.IsNullOrEmpty(token.Text)) continue;
                    if (commonTokens.Any() && token.RawKind == commonTokens[commonTokenIndexByLine[lineIndex]].RawKind)
                    {
                        if (columnTokensByLine[lineIndex].Any())
                        {
                            columnizedTokens[lineIndex][commonTokenIndexByLine[lineIndex]] = columnTokensByLine[lineIndex].ToArray();
                            columnTokensByLine[lineIndex].Clear();                            
                        }

                        columnTokensByLine[lineIndex].Add(token);
                        columnizedTokens[lineIndex][commonTokenIndexByLine[lineIndex]] = columnTokensByLine[lineIndex].ToArray();
                        commonTokenIndexByLine[lineIndex]++;
                        columnTokensByLine[lineIndex].Clear();
                        continue;
                    }
                    else
                    {
                        columnTokensByLine[lineIndex].Add(token);
                    }
                }
            }

            return columnizedTokens;
        }

        private SyntaxToken[] GetCommonTokens(SyntaxToken[][] tokenByLine)
        {
            var lcs = new LCS<SyntaxToken>(new SyntaxTokenRawKindComparer(), tokenByLine);
            lcs.Execute();
            var result = lcs.Result.ToList();
            result.RemoveAll(t => string.IsNullOrEmpty(t.Text));
            return result.ToArray();
        }

        private SyntaxToken [][] GetTokenByLine(string text)
        {
            var lines = text.Split('\n').ToList();
            lines.RemoveAll(s => string.IsNullOrEmpty(s));
            var tokensByLine = new SyntaxToken[lines.Count][];
            for (int i = 0; i < lines.Count; i++)
            {                
                tokensByLine[i] = GetTokens(lines[i]);
            }

            return tokensByLine;
        }

        private SyntaxToken[] GetTokens(string text)
        {
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(text);
            return syntaxTree.GetRoot().DescendantTokens().ToArray();
        }
    }
}
