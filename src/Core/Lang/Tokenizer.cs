using PHP.Core.Lang.Exceptions;
using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang
{
    public class Tokenizer
    {
        public readonly string Code;

        private List<TokenItem> tokens = new List<TokenItem>();
        private int index = 0;

        public Tokenizer(string code)
        {
            this.Code = code;
        }
        
        private TokenPosition GetPosition()
        {
            int[] result = new int[2];
            result[0] = 0;
            result[1] = 0;
            for (int i = 0; i < Code.Length && i < index; i++)
                if (Code[i] == '\n')
                {
                    result[0]++;
                    result[1] = 0;
                }
                else
                    result[1]++;
            return new TokenPosition(index, result[0], result[1]);
        }

        private TokenItem NextToken()
        {
            string part = Code.Substring(index);
            foreach (TokenType type in TokenTypeHelper.GetTokensQueue())
            {
                var match = type.GetPatternRegex().Match(part);
                if (match != null && match.Success)
                {
                    TokenItem item = new TokenItem(type, match.Value, GetPosition());
                    index += match.Length;
                    return item;
                }
            }
            throw new LexicalException($"Unexpected symbol \"{Code[index]}\"", GetPosition());
        }

        public TokenItem[] Tokenize()
        {
            tokens.Clear();
            index = 0;
            while(index < Code.Length)
            {
               tokens.Add(NextToken());
            }
            tokens.Add(new TokenItem(TokenType.T_EOF, "", GetPosition()));
            return tokens.ToArray();
        }
    }
}
