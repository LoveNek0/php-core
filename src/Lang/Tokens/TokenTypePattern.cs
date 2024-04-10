using System;

namespace PHP.Core.Lang.Tokens
{
    internal class TokenTypePattern : Attribute
    {
        public readonly string Pattern;

        public TokenTypePattern(string pattern = "") => Pattern = pattern;
    }
}