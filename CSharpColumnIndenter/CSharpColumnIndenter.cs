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
        public string Apply(string text)
        {
            var tokenByLine = GetTokenByLine(text);
            var commonTokens = GetCommonTokens(tokenByLine);
            var columnizedText = GetColumnizedString(tokenByLine,commonTokens,text);
            CheckCode(columnizedText, text);
            return columnizedText;
        }

        private string GetColumnizedString(SyntaxToken[][] tokenByLine, SyntaxToken[] commonTokens,string actualText)
        {
            var columnizer = new SyntaxColumnizer(tokenByLine, commonTokens,actualText,new SyntaxTokenComparer());
            columnizer.Execute();
            return columnizer.ToString();
        }

        private void CheckCode(string columnizedText, string text)
        {
            var modifiedCode = GetTokens(columnizedText).Select(t=>t.Text).ToList();
            var originalCode = GetTokens(text).Select(t=>t.Text).ToList();
            var isNotEqual = modifiedCode.Except(originalCode).ToList().Any() || originalCode.Except(modifiedCode).ToList().Any() || originalCode.Count!=modifiedCode.Count;
            if (isNotEqual) throw new Exception("Visual Studio Extension - CSharp Column Indenter is trying to change the selected code, its a bug please report here.");
        }

        private SyntaxToken[] GetCommonTokens(SyntaxToken[][] tokenByLine)
        {
            var lcs = new LCS<SyntaxToken>(new SyntaxTokenComparer(), tokenByLine);
            lcs.Execute();
            var result = lcs.Result.ToList();
            //remove EndOfLineToken
            result.RemoveAll(t => string.IsNullOrEmpty(t.Text));
            return result.ToArray();
        }

        private SyntaxToken [][] GetTokenByLine(string text)
        {
            var lines = text.Split('\n').ToList();
            lines.RemoveAll(s => string.IsNullOrWhiteSpace(s));
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
