using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTArrowFunctionConstruction : ASTNode
    {
        public readonly bool ResultAsPointer;
        public readonly ASTFunctionArgument[] Arguments;
        public readonly TokenItem ReturnType;
        public readonly ASTNode Body;
        internal ASTArrowFunctionConstruction(TokenItem token, bool resultAsPointer, ASTFunctionArgument[] arguments, TokenItem returnType, ASTNode body) : base(token)
        {
            ResultAsPointer = resultAsPointer;
            Arguments = arguments;
            ReturnType = returnType;
            Body = body;
        }

        public override string ToString() => $"fn{(ResultAsPointer?"&":"")}({string.Join(", ", Arguments.Select(a => a.ToString()))}){(ReturnType != null ? $": {ReturnType.Data}" : "")} => {Body}";
    }
}