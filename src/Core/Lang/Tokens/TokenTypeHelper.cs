using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Tokens
{
    internal static class TokenTypeHelper
    {
        private static Dictionary<TokenType, string> patterns = new Dictionary<TokenType, string>()
        {
            { TokenType.T_COMMENT,                  @"((\/\/(.|\w)*)|((\/\*)[\w\W\s\S]*(\*\/)))" },

            { TokenType.T_IF,                       @"[Ii][Ff]" },
            { TokenType.T_ELSE,                     @"[Ee][Ll][Ss][Ee]" },
            // { TokenType.T_ELSEIF,                   @"[Ee][Ll][Ss][Ee][Ii][Ff]" },
            { TokenType.T_WHILE,                    @"[Ww][Hh][Ii][Ll][Ee]" },
            { TokenType.T_DO,                       @"[Dd][Oo]" },
            { TokenType.T_FOR,                      @"[Ff][Oo][Rr]" },
            { TokenType.T_FOREACH,                  @"[Ff][Oo][Rr][Ee][Aa][Cc][Hh]" },
            { TokenType.T_AS,                       @"[Aa][Ss]" },
            
            { TokenType.T_ECHO,                     @"[Ee][Cc][Hh][Oo]" },
            { TokenType.T_FUNCTION,                 @"[Ff][Uu][Nn][Cc][Tt][Ii][Oo][Nn]" },
            { TokenType.T_RETURN,                   @"[Rr][Ee][Tt][Uu][Rr][Nn]" },
            { TokenType.T_USE,                      @"[Uu][Ss][Ee]" },

            { TokenType.T_BOOL_CAST,                @"[(][Bb][Oo][Oo][Ll][)]" },
            { TokenType.T_INT_CAST,                 @"[(][Ii][Nn][Tt][)]" },
            { TokenType.T_DOUBLE_CAST,              @"[(]([Dd][Oo][Uu][Bb][Ll][Ee])|([Ff][Ll][Oo][Aa][Tt])|([Rr][Ee][Aa][Ll])[)]" },
            { TokenType.T_STRING_CAST,              @"[(][Ss][Tt][Rr][Ii][Nn][Gg][)]" },
            { TokenType.T_ARRAY_CAST,               @"[(][Aa][Rr][Rr][Aa][Yy][)]" },
            { TokenType.T_OBJECT_CAST,              @"[(][Oo][Bb][Jj][Ee][Cc][Tt][)]" },
            { TokenType.T_UNSET_CAST,               @"[(][Uu][Nn][Ss][Ee][Tt][)]" },
            { TokenType.T_CUSTOM_TYPE_CAST,         @"[(]([A-Za-z_][A-Za-z0-9_]*)[)]" },

            { TokenType.T_CURLY_BRACE_OPEN,         @"[{]" },
            { TokenType.T_CURLY_BRACE_CLOSE,        @"[}]" },
            { TokenType.T_BRACE_OPEN,               @"[(]" },
            { TokenType.T_BRACE_CLOSE,              @"[)]" },

            { TokenType.T_OBJECT_OPERATOR,          @"[-][>]" },
            { TokenType.T_NULLSAFE_OBJECT_OPERATOR, @"[?][-][>]" },

            { TokenType.T_INCREMENT,                @"[+][+]" },
            { TokenType.T_DECREMENT,                @"[-][-]" },

            { TokenType.T_ADD_ASSIGNMENT,           @"[+][=]" },
            { TokenType.T_ADD,                      @"[+]" },
            { TokenType.T_SUB_ASSIGNMENT,           @"[-][=]" },
            { TokenType.T_SUB,                      @"[-]" },
            { TokenType.T_POW_ASSIGNMENT,           @"[*][*][=]" },
            { TokenType.T_MUL_ASSIGNMENT,           @"[*][=]" },
            { TokenType.T_POW,                      @"[*][*]" },
            { TokenType.T_MUL,                      @"[*]" },
            { TokenType.T_DIV_ASSIGNMENT,           @"[/][=]" },
            { TokenType.T_DIV,                      @"[/]" },
            { TokenType.T_MOD_ASSIGNMENT,           @"[%][=]" },
            { TokenType.T_MOD,                      @"[%]" },
            { TokenType.T_CONCAT_ASSIGNMENT,        @"[.][=]" },
            { TokenType.T_CONCAT,                   @"[.]" },
            { TokenType.T_ASSIGNMENT,               @"[=]" },
            
            { TokenType.T_IS_EQUAL,                 @"[=][=]" },
            { TokenType.T_IS_NOT_EQUAL,             @"(([!][=])|([<][>]))" },
            { TokenType.T_IS_SMALLER,               @"[<]" }, 
            { TokenType.T_IS_SMALLER_OR_EQUAL,      @"[<][=]" },
            { TokenType.T_IS_GREATER,               @"[>]" },
            { TokenType.T_IS_GREATER_OR_EQUAL,      @"[>][=]" },
            { TokenType.T_SPACESHIP,                @"[<][=][>]" },
            
            { TokenType.T_SEMICOLON,                @"[;]" },

            { TokenType.T_DNUMBER,                  @"([0-9]+)[.]([0-9]+)" },
            { TokenType.T_LNUMBER,                  @"[0-9]+" },
            { TokenType.T_VARIABLE,                 @"([$]+([a-zA-Z_][a-zA-Z0-9_]*))" },
            { TokenType.T_STRING,                   @"('(?:(([^\\'])|(\\.)))*')" },
            { TokenType.T_STATIC_STRING,            @"[a-zA-Z_][a-zA-Z0-9_]*" },

            { TokenType.T_QUERY,                    @"[?]" },
            { TokenType.T_DOUBLE_COLON,             @"[:][:]" },
            { TokenType.T_COLON,                    @"[:]" },
            { TokenType.T_ELLIPSIS,                 @"[.][.][.]" },
            { TokenType.T_COMMA,                    @"[,]" },

            { TokenType.T_OPEN_TAG_WITH_ECHO,       @"<[?%]=" },
            { TokenType.T_OPEN_TAG,                 @"[<](([?]([pP][hH][pP])?)|[%])" },
            { TokenType.T_CLOSE_TAG,                @"[?%][>]" },
            { TokenType.T_WHITESPACE,               @"\s+" }
        };

        private static List<TokenType> queue = new List<TokenType>()
        {
            TokenType.T_COMMENT,
            
            TokenType.T_OPEN_TAG_WITH_ECHO,
            TokenType.T_OPEN_TAG,
            TokenType.T_CLOSE_TAG,
            
            TokenType.T_IS_EQUAL,
            TokenType.T_IS_NOT_EQUAL,
            TokenType.T_SPACESHIP,
            TokenType.T_IS_SMALLER_OR_EQUAL,
            TokenType.T_IS_GREATER_OR_EQUAL,
            TokenType.T_IS_SMALLER,
            TokenType.T_IS_GREATER,
            
            TokenType.T_IF,
            TokenType.T_ELSE,
            // TokenType.T_ELSEIF,
            TokenType.T_WHILE,
            TokenType.T_DO,
            TokenType.T_FOR,
            TokenType.T_FOREACH,
            TokenType.T_AS,

            TokenType.T_ECHO,
            TokenType.T_FUNCTION,
            TokenType.T_RETURN,
            TokenType.T_USE,

            TokenType.T_BOOL_CAST,
            TokenType.T_INT_CAST,
            TokenType.T_DOUBLE_CAST,
            TokenType.T_STRING_CAST,
            TokenType.T_ARRAY_CAST,
            TokenType.T_OBJECT_CAST,
            TokenType.T_UNSET_CAST,
            TokenType.T_CUSTOM_TYPE_CAST,

            TokenType.T_CURLY_BRACE_OPEN,
            TokenType.T_CURLY_BRACE_CLOSE,
            TokenType.T_BRACE_OPEN,
            TokenType.T_BRACE_CLOSE,

            TokenType.T_OBJECT_OPERATOR,
            TokenType.T_NULLSAFE_OBJECT_OPERATOR,

            TokenType.T_INCREMENT,
            TokenType.T_DECREMENT,

            TokenType.T_ADD_ASSIGNMENT,
            TokenType.T_ADD,
            TokenType.T_SUB_ASSIGNMENT,
            TokenType.T_SUB,
            TokenType.T_POW_ASSIGNMENT,
            TokenType.T_MUL_ASSIGNMENT,
            TokenType.T_POW,
            TokenType.T_MUL,
            TokenType.T_DIV_ASSIGNMENT,
            TokenType.T_DIV,
            TokenType.T_MOD_ASSIGNMENT,
            TokenType.T_MOD,
            TokenType.T_CONCAT_ASSIGNMENT,
            TokenType.T_CONCAT,
            TokenType.T_ASSIGNMENT,

            TokenType.T_SEMICOLON,

            TokenType.T_DNUMBER,
            TokenType.T_LNUMBER,
            TokenType.T_VARIABLE,
            TokenType.T_STRING,
            TokenType.T_STATIC_STRING,

            TokenType.T_QUERY,
            TokenType.T_DOUBLE_COLON,
            TokenType.T_COLON,
            TokenType.T_ELLIPSIS,
            TokenType.T_COMMA,

            TokenType.T_WHITESPACE
        };

        public static TokenType[] GetTokensQueue() => queue.ToArray();

        public static Regex GetPatternRegex(this TokenType type) => patterns.TryGetValue(type, out var pattern) ? new Regex($"^{pattern}") : null;
        public static string GetPatternString(this TokenType type) => patterns.TryGetValue(type, out var patternString) ? $"^{patternString}" : null;
    }
}
