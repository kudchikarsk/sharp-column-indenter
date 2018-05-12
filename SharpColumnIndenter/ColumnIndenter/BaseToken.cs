using SharpColumnIndenter.Languages;

namespace SharpColumnIndenter.ColumnIndenter
{
    internal class BaseToken : IToken
    {
        private string _text;

        public BaseToken(string text)
        {
            _text = text;
        }

        public string Text => _text;
    }
}