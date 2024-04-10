namespace PHP.Core.Lang.Tokens
{
    public static class TokenTypeExtension
    {
        public static string GetPattern(this TokenType tokenType)
        {
            var type = typeof(TokenType);
            var memInfo = type.GetMember(tokenType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TokenTypePattern), false);
            var pattern = ((TokenTypePattern)attributes[0]).Pattern;
            return pattern;
        }
    }
}