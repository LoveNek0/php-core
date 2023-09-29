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
    public class Lexer
    {
        private static Dictionary<TokenType, string> _patterns = new Dictionary<TokenType, string>()
        {
            { TokenType.DocComment,                         @"((\/\*\*)[\w\W\s\S]*(\*\/))" },
            { TokenType.Comment,                            @"((\/\/(.|\w)*)|((\/\*)[\w\W\s\S]*(\*\/)))" },
            { TokenType.Whitespace,                         @"\s+" },
            
            { TokenType.PHPOpenTag,                         @"[<](([?]([pP][hH][pP])?)|[%])" },
            { TokenType.PHPOpenTagWithEcho,                 @"<[?%]=" },
            { TokenType.PHPCloseTag,                        @"[?%][>]" },
            
            { TokenType.Comma,                              @"[,]" },
            { TokenType.Semicolon,                          @"[;]" },
            
            { TokenType.BraceOpen,                          @"[(]" },
            { TokenType.BraceClose,                         @"[)]" },
            { TokenType.CurlyBraceOpen,                     @"[{]" },
            { TokenType.CurlyBraceClose,                    @"[}]" },
            { TokenType.SquareBraceOpen,                    @"[\[]" },
            { TokenType.SquareBraceClose,                   @"[\]]" },
            
            { TokenType.Add,                                @"[+]" },
            { TokenType.Sub,                                @"[-]" },
            { TokenType.Mul,                                @"[*]" },
            { TokenType.Div,                                @"[\/]" },
            { TokenType.Mod,                                @"[%]" },
            { TokenType.Pow,                                @"[*][*]" },
            { TokenType.Concat,                             @"[.]" },
            
            { TokenType.Decrement,                          @"[-][-]" },
            { TokenType.Increment,                          @"[+][+]" },
            
            { TokenType.BitAnd,                             @"[&]" },
            { TokenType.BitXor,                             @"[\^]" },
            { TokenType.BitNot,                             @"[|]" },
            { TokenType.BitShiftLeft,                       @"[<][<]" },
            { TokenType.BitShiftRight,                      @"[>][>]" },
            
            { TokenType.Coalesce,                           @"[?][?]" },
            
            { TokenType.Assignment,                         @"[=]" },
            { TokenType.AssignmentAdd,                      @"[+][=]" },
            { TokenType.AssignmentSub,                      @"[-][=]" },
            { TokenType.AssignmentMul,                      @"[*][=]" },
            { TokenType.AssignmentDiv,                      @"[\/][=]" },
            { TokenType.AssignmentMod,                      @"[%][=]" },
            { TokenType.AssignmentPow,                      @"[*][*][=]" },
            { TokenType.AssignmentConcat,                   @"[.][=]" },
            
            { TokenType.AssignmentBitAnd,                   @"[&][=]" },
            { TokenType.AssignmentBitXor,                   @"[\^][=]" },
            { TokenType.AssignmentBitNot,                   @"[|][=]" },
            { TokenType.AssignmentBitShiftLeft,             @"[<][<][=]" },
            { TokenType.AssignmentBitShiftRight,            @"[>][>][=]" },
            
            { TokenType.AssignmentCoalesce,                 @"[?][?][=]" },
            
            { TokenType.LogicalAnd,                         @"([&][&])|([Aa][Nn][Dd])" },
            { TokenType.LogicalOr,                          @"([|][|])|([Oo][Rr])" },
            { TokenType.LogicalXor,                         @"[Xx][Oo][Rr]" },
            
            { TokenType.IsEqual,                            @"[=][=]" },
            { TokenType.IsIdentical,                        @"[=][=][=]" },
            { TokenType.IsNotEqual,                         @"([!][=])|([<][>])" },
            { TokenType.IsNotIdentical,                     @"[!][=][=]" },
            { TokenType.IsGreater,                          @"[>]" },
            { TokenType.IsSmaller,                          @"[<]" },
            { TokenType.IsGreaterOrEqual,                   @"[>][=]" },
            { TokenType.IsSmallerOrEqual,                   @"[<][=]" },
            { TokenType.IsSpaceship,                        @"[<][=][>]" },
            
            { TokenType.Integer,                            @"[0-9]+" },
            { TokenType.Float,                              @"([0-9]+)[.]([0-9]+)" },
            { TokenType.String,                             @"('(?:(([^\\'])|(\\.)))*')" },
            { TokenType.Variable,                           @"([$]+([a-zA-Z_][a-zA-Z0-9_]*))" }
        };

        private static TokenType[] _ignore = new[]
        {
            TokenType.DocComment,
            TokenType.Comment,
            TokenType.PHPOpenTagWithEcho,
            TokenType.PHPOpenTag,
            TokenType.PHPCloseTag,
            TokenType.Whitespace
        };
        
        private static TokenType[] _queue = new[]
        {
            TokenType.DocComment,                 // /** */
            TokenType.Comment,                    // // # /* */
            TokenType.PHPOpenTagWithEcho,         //  <?= <%=
            TokenType.PHPOpenTag,                 //  <? <?php <%
            TokenType.PHPCloseTag,                //  ?> %>
            TokenType.Comma,                      //  ,
            TokenType.Semicolon,                  //  ;
            TokenType.BraceOpen,                  //  (
            TokenType.BraceClose,                 //  )
            TokenType.CurlyBraceOpen,             //  {  
            TokenType.CurlyBraceClose,            //  }
            TokenType.SquareBraceOpen,            //  [
            TokenType.SquareBraceClose,           //  ]
            TokenType.Increment,                  //  ++
            TokenType.AssignmentAdd,              //  +=
            TokenType.Add,                        //  +
            TokenType.AssignmentSub,              //  -=
            TokenType.Decrement,                  //  --
            TokenType.Sub,                        //  -
            TokenType.AssignmentMul,              //  *=
            TokenType.Mul,                        //  *
            TokenType.AssignmentDiv,              //  /=
            TokenType.Div,                        //  /
            TokenType.AssignmentMod,              //  %=
            TokenType.Mod,                        //  %
            TokenType.AssignmentPow,              //  **=
            TokenType.Pow,                        //  **
            TokenType.AssignmentConcat,           //  .=
            TokenType.Concat,                     //  .
            TokenType.IsIdentical,                //  ===
            TokenType.IsEqual,                    //  ==
            TokenType.Assignment,                 //  =
            TokenType.AssignmentBitAnd,           //  &=
            TokenType.LogicalAnd,                 //  && and
            TokenType.BitAnd,                     //  &
            TokenType.AssignmentBitXor,           //  ^=
            TokenType.BitXor,                     //  ^
            TokenType.AssignmentBitNot,           //  |=
            TokenType.LogicalOr,                  //  || or
            TokenType.BitNot,                     //  |
            TokenType.AssignmentBitShiftLeft,     //  <<=
            TokenType.BitShiftLeft,               //  <<
            TokenType.IsSpaceship,                //  <=>
            TokenType.IsSmallerOrEqual,           //  <=
            TokenType.IsNotIdentical,             //  !==
            TokenType.IsNotEqual,                 //  != <>
            TokenType.IsSmaller,                  // <
            TokenType.AssignmentBitShiftRight,    //  >>=
            TokenType.BitShiftRight,              //  >>
            TokenType.IsGreaterOrEqual,           //  >=
            TokenType.IsGreater,                  // >
            TokenType.LogicalXor,                 //  xor
            TokenType.AssignmentCoalesce,         //  ??=
            TokenType.Coalesce,                   //  ??
            TokenType.Float,                      //  1.5
            TokenType.Integer,                    //  123 012 0x1ac
            TokenType.String,                     //  "" ''
            TokenType.Variable,                 //  $var_1
            TokenType.Whitespace,                 //  \t \r \n
        };
        
        public readonly string Code;

        private int _position;
        private int _line;
        private int _column;

        public Lexer(string code)
        {
            this.Code = code;
            Reset();
        }

        public void Reset()
        {
            _position = 0;
            _line = 0;
            _column = 0;
        }

        public bool HasNext() => _position <= Code.Length;

        public TokenItem NextToken()
        {
            if (_position == Code.Length)
            {
                _position++;
                return new TokenItem(TokenType.EndOfFile, "", _line, _column, _position - 1);
            }

            string part = Code.Substring(_position);
            foreach (TokenType type in _queue)
            {
                Match match = Regex.Match(part, $"^{_patterns[type]}");
                if (match.Success)
                {
                    TokenItem item = new TokenItem(type, match.Value, _line, _column, _position);
                    foreach (char symbol in match.Value)
                        if (symbol == '\n')
                        {
                            _line++;
                            _column = 0;
                        }
                        else
                            _column++;
                    _position += match.Length;
                    return item;
                }
            }
            throw new LexicalException($"Unexpected symbol \"{Code[_position]}\"", _line, _column);
        }

        public static TokenItem[] Tokenize(string code, bool ignore = true)
        {
            List<TokenItem> tokens = new List<TokenItem>();
            Lexer lexer = new Lexer(code);
            while (lexer.HasNext())
            {
                TokenItem item = lexer.NextToken();
                if (!ignore || !_ignore.Contains(item.Type))
                    tokens.Add(item);
            }
            return tokens.ToArray();
        }
    }
}
