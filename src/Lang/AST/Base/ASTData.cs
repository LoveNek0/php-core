using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Base
{
    public class ASTData : ASTNode
    {
        internal ASTData(TokenItem token) : base(token)
        {
        }

        public override string ToString() => Token.Data.ToString();
    }
}
