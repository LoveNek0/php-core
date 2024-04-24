using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTArrowFunction : ASTNode
    {
        public readonly bool ResultAsPointer;
        public readonly ASTFunctionArgument[] Arguments;
        public readonly ASTNode Body;
        internal ASTArrowFunction(TokenItem token, bool resultAsPointer, ASTFunctionArgument[] arguments, ASTNode body) : base(token)
        {
            ResultAsPointer = resultAsPointer;
            Arguments = arguments;
            Body = body;
        }

        public override string ToString() => $"fn{(ResultAsPointer?"&":"")}({string.Join(", ", Arguments.Select(a => a.ToString()))}) => {Body}";
    }
}