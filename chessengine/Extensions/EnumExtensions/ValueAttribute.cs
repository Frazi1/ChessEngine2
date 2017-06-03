using System;

namespace chessengine.Extensions.EnumExtensions {
    public class ValueAttribute : Attribute {
        public int Value { get; private set; }

        public ValueAttribute(int value) {
            Value = value;
        }
    }
}