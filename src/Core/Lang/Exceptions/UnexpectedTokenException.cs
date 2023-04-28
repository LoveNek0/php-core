using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.Exceptions;

public class UnexpectedTokenException : SyntaxException
{
    public readonly TokenType[] Expected;

    public UnexpectedTokenException(TokenItem token, params TokenType[] expected) : base(
        $"Unexpected token '{token.Type}'" + (expected.Length > 0 ? $", expected {String.Join(", ", expected)}" : ""), token) => Expected = expected;
}