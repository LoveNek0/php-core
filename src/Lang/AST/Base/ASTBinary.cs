using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Base
{
    public abstract class ASTBinary : ASTNode
    {
        public readonly ASTNode Left;
        public readonly ASTNode Right;

        internal ASTBinary(TokenItem token, ASTNode left, ASTNode right) : base(token)
        {
            Left = left;
            Right = right;
        }

        public override string ToString() => $"({Left} {Token.Data} {Right})";
    }
}