using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SharpColumnIndenter.Languages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SharpColumnIndenter.ColumnIndenter
{
    class Indenter
    {
        private ILanguage _language;        

        public Indenter(ILanguage language)
        {
            _language = language;
        }
        public string Apply(string text)
        {
            var tokenByLine = GetTokenByLine(text);
            if (tokenByLine.Count() < 2) throw new Exception("Failed to find a pattern for column indention! if you think this is a bug please report here https://github.com/kudchikarsk/csharp-column-indenter/issues");
            var commonTokens = GetCommonTokens(tokenByLine);
            if (!commonTokens.Any()) throw new Exception("Failed to find a pattern for column indention! if you think this is a bug please report here https://github.com/kudchikarsk/csharp-column-indenter/issues");
            var columnizedText = GetColumnizedString(tokenByLine,commonTokens,text);
            CheckCode(columnizedText, text);
            return columnizedText;
        }

        private string GetColumnizedString(IToken[][] tokenByLine, IToken[] commonTokens,string actualText)
        {
            var columnizer = new TokenColumnizer(tokenByLine, commonTokens,actualText,_language.Comparer);
            columnizer.Execute();
            return columnizer.ToString();
        }

        private void CheckCode(string columnizedText, string text)
        {
            var modifiedCode = _language.GetTokens(columnizedText).Select(t=>t.Text).ToList();
            var originalCode = _language.GetTokens(text).Select(t=>t.Text).ToList();
            var isNotEqual = modifiedCode.Except(originalCode).ToList().Any() || originalCode.Except(modifiedCode).ToList().Any() || originalCode.Count!=modifiedCode.Count;
            if (isNotEqual) throw new Exception("Visual Studio Extension - CSharp Column Indenter is trying to change the selected code, its a bug please report here https://github.com/kudchikarsk/csharp-column-indenter/issues.");
        }

        private IToken [] GetCommonTokens(IToken [][] tokenByLine)
        {
            var lcs = new LCS<IToken>(_language.Comparer, tokenByLine);
            lcs.Execute();
            var result = lcs.Result.ToList();
            //remove EndOfLineToken
            result.RemoveAll(t => string.IsNullOrEmpty(t.Text));
            return result.ToArray();
        }

        private IToken [][] GetTokenByLine(string text)
        {
            var lines = Regex.Split(text,@"(\r\n|\n)").ToList();
            lines.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            var tokensByLine = new IToken[lines.Count][];
            for (int i = 0; i < lines.Count; i++)
            {                
                tokensByLine[i] = _language.GetTokens(lines[i]);
            }

            return tokensByLine;
        }

        
    }
}
