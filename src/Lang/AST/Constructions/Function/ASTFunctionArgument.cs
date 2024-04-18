using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{

    public class ASTFunctionArgument
    {
        public TokenItem Type;
        public TokenItem Sign;
        public TokenItem Variable;
        public TokenItem ByDefault;

        public ASTFunctionArgument(TokenItem type, TokenItem sign, TokenItem variable, TokenItem byDefault)
        {
            Type = type;
            Sign = sign;
            Variable = variable;
            ByDefault = byDefault;
        }

        public override string ToString() =>
            $"{(Type != null ? Type.Data + " " : "")}{(Sign != null ? Sign.Data : "")}{Variable.Data}{(ByDefault != null ? " = " + ByDefault.Data : "")}";
    }
}
