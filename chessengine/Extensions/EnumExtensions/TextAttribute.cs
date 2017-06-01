using System;

namespace chessengine.Extensions.EnumExtensions {
    public class TextAttribute : Attribute {
        public string Text { get; private set; }

        public TextAttribute(string text) {
            Text = text;
        }
    }
}