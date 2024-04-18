using System;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{

    public class ASTFunction
    {
        public readonly ASTNode Name;
        public readonly ASTFunctionArgument[] Arguments;
        public readonly TokenItem ReturnType;
        public readonly ASTNode[] Lines;
        
        
        //  TODO: конструктор и парсинг функций
        public ASTFunction()
        {
        }

        public override string ToString() =>
            $"function {Name} ({String.Join(", ", Arguments.GetEnumerator())}){{\n{String.Join("\n", Lines.GetEnumerator())}\n}}";
    }
}