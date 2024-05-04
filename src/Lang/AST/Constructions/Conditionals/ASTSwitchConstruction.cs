using System.Collections.Generic;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Conditionals
{
    public class ASTSwitchConstruction : ASTNode
    {
        public readonly ASTNode Expression;
        public readonly int Default;
        public readonly KeyValuePair<ASTNode, int>[] Conditions;
        public readonly ASTNode[][] Bodies;

        internal ASTSwitchConstruction(TokenItem token, ASTNode expression, int @default, KeyValuePair<ASTNode, int>[] conditions, ASTNode[][] bodies) : base(token)
        {
            Expression = expression;
            Default = @default;
            Conditions = conditions;
            Bodies = bodies;
        }

        public override string ToString()
        {
            string result = $"[switch({Expression}){{\n";
            for (int i = 0; i < Bodies.Length; i++)
            {
                if (i == Default)
                    result += "default:\n";
                result += string.Join("\n", Conditions.Where(pair => pair.Value == i).Select(pair => $"case {pair.Key}:"));
                result += $"{{\n{string.Join("\n", string.Join("\n", Bodies[i].Select(body => body)))}\n}}\n";
            }
            return result + "}}]";
        }
    }
}