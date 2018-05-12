using SharpColumnIndenter.Languages;

namespace SharpColumnIndenter.ColumnIndenter
{
    internal class BaseToken : Token
    {
        private string _text;

        public BaseToken(string text)
        {
            _text = text;
        }

        public override string Text => _text;
    }
}