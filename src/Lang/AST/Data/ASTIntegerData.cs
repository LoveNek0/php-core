using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Data
{
    public class ASTIntegerData : ASTData
    {
        internal ASTIntegerData(TokenItem token) : base(token)
        {
        }
    }
}