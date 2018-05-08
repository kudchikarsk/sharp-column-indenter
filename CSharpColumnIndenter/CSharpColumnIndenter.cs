using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(text);

            return text.ToUpper();
        }
    }
}
