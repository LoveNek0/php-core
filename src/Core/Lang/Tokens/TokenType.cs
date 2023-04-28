using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Tokens
{
    public enum TokenType
    {
        T_EOF,

        //  Arithmetic Operator
        T_ADD,                      //  +
        T_SUB,                      //  -
        T_MUL,                      //  *
        T_DIV,                      //  /
        T_MOD,                      //  %
        T_POW,                      //  **

        //  Assignment with Operator
        T_ADD_ASSIGNMENT,           //  +=
        T_SUB_ASSIGNMENT,           //  -=
        T_MUL_ASSIGNMENT,           //  *=
        T_DIV_ASSIGNMENT,           //  /=
        T_MOD_ASSIGNMENT,           //  %=
        T_POW_ASSIGNMENT,           //  **=
        T_CONCAT_ASSIGNMENT,        //  .=


        //  Bitwise Operator
        T_BIT_AND,                  //  &
        T_BIT_XOR,                  //  ^
        T_BIT_NOT,                  //  |
        T_BIT_SHIFT_LEFT,           //  <<
        T_BIT_SHIFT_RIGHT,          //  >>

        //  String operator
        T_CONCAT,                   //  .

        //  Assignment Operator
        T_ASSIGNMENT,                    //  =

        //  Null Coalescing Operator
        T_COALESCE,                 //  ??



        //  Assignment with Bitwise Operator
        T_BIT_AND_EQUAL,            //  &=
        T_BIT_XOR_EQUAL,            //  ^=
        T_BIT_NOT_EQUAL,            //  |=
        T_BIT_SHIFT_LEFT_EQUAL,     //  <<=
        T_BIT_SHIFT_RIGHT_EQUAL,    //  >>=

        //  Assignment with String Operator

        //  Assignment with Null Coalescing Operator
        T_COALESCE_EQUAL,           //  ??=

        //  Logical Operator
        T_LOGICAL_AND,              //  && and
        T_LOGICAL_OR,               //  || or
        T_LOGICAL_XOR,              //  xor

        //  Comparison Operator
        T_IS_EQUAL,                 //  ==
        T_IS_IDENTICAL,             //  ===
        T_IS_NOT_EQUAL,             //  != <>
        T_IS_NOT_IDENTICAL,         //  !==
        T_IS_GREATER,               // >
        T_IS_SMALLER,               // <
        T_IS_GREATER_OR_EQUAL,      //  >=
        T_IS_SMALLER_OR_EQUAL,      //  <=
        T_SPACESHIP,                //  <=>

        //  Incrementing/Decrementing Operator
        T_DECREMENT,                //  --
        T_INCREMENT,                //  ++

        //  Data
        T_LNUMBER,                  //  123 012 0x1ac
        T_DNUMBER,                  //  1.5
        T_STRING, //  "" ''
        T_STATIC_STRING,                   //  parent self T_CONSTANT_ENCAPSED_STRING

        //  Variable
        T_VARIABLE,                 //  $var_1
        T_DOLLAR_OPEN_CURLY_BRACES, //  ${

        //  Function
        T_FUNCTION,                 //  function
        T_STATIC,                   //  static
        T_RETURN,                   //  return
        T_YIELD,                    //  yield
        T_YIELD_FROM,               //  yield_from
        T_CALLABLE,                 //  callable
        T_ELLIPSIS,                 //  ...
        T_FN,                       //  fn

        //  Class
        T_ABSTRACT,                 //  abstract
        T_INTERFACE,                //  interface
        T_TRAIT,                    //  trait
        T_CLASS,                    //  class
        T_PUBLIC,                   //  public
        T_PRIVATE,                  //  private
        T_PROTECTED,                //  protected
        T_DOUBLE_COLON,             //  ::
        T_OBJECT_OPERATOR,          //  ->
        T_NULLSAFE_OBJECT_OPERATOR, //  ?->
        T_CLONE,                    //  clone
        T_EXTENDS,                  //  extends
        T_IMPLEMENTS,               //  implements
        T_FINAL,                    //  final
        T_INSTANCEOF,               //  instanceof
        T_INSTEADOF,                //  insteadof
        T_NEW,                      //  new
        T_QUERY,                    //  ?
                                    //  T_ATTRIBUTE,                                //  attribytes

        //  Array
        T_ARRAY,                    //  array()
        T_DOUBLE_ARROW,             //  =>

        //  Casting
        T_INT_CAST,                 //  (int) (integer)
        T_DOUBLE_CAST,              //  (real) (double) (float)
        T_STRING_CAST,              //  (string)
        T_BOOL_CAST,                //  (bool) (boolean)
        T_ARRAY_CAST,               //  (array)
        T_OBJECT_CAST,              //  (object)
        T_UNSET_CAST,               //  (unset)
        T_CUSTOM_TYPE_CAST,         //  (CustomType)

        //  Cycles
        T_DO,                       //  do
        T_WHILE,                    //  while
        T_ENDWHILE,                 //  endwhile
        T_FOR,                      //  for
        T_ENDFOR,                   //  endfor
        T_FOREACH,                  //  foreach
        T_ENDFOREACH,               //  endforeach
        T_AS,                       //  as
        T_CONTINUE,                 //  continue

        //  Switch
        T_SWITCH,                   //  switch
        T_ENDSWITCH,                //  endswitch
        T_CASE,                     //  case
        T_BREAK,                    //  break
        T_DEFAULT,                  //  default

        //  if ... else
        T_IF,                       //  if
        T_ELSE,                     //  else
        // T_ELSEIF,                   //  elseif
        T_ENDIF,                    //  endif

        //  try .. catch
        T_THROW,                    //  throw
        T_TRY,                      //  try
        T_CATCH,                    //  catch
        T_FINALLY,                  //  finally

        //  Magic Constants
        T_FILE,                     //  __FILE__
        T_FUNC_C,                   //  __FUNCTION__
        T_CLASS_C,                  //  __CLASS__
        T_DIR,                      //  __DIR__
        T_TRAIT_C,                  //  __TRAIT__
        T_LINE,                     //  __LINE__
        T_METHOD_C,                 //  __METHOD__
        T_NS_C,                     //  __NAMESPACE__

        //  Braces
        T_BRACE_OPEN,               //  (
        T_BRACE_CLOSE,              //  )
        T_CURLY_BRACE_OPEN,         //  {  
        T_CURLY_BRACE_CLOSE,        //  }
        T_SQUARE_BRACE_OPEN,        //  [
        T_SQUARE_BRACE_CLOSE,       //  ]

        //  Language Constructions
        T_CONST,                    //  const
        T_ECHO,                     //  echo
        T_MATCH,                    //  match
        T_GLOBAL,                   //  global
        T_DECLARE,                  //  declare
        T_ENDDECLARE,               //  enddeclare

        //  Namespaces
        T_NAMESPACE,                //  namespace
        T_NAMESPACE_NAME,           //  a\b\c
        T_USE,                      //  use
        T_NAMESPACE_CALL_NAME,      //  \a\b\c

        //  Goto points
        T_GOTO,                     //  goto
        T_COLON,                    //  :

        //  Including Operators
        T_INCLUDE,                  //  include
        T_INCLUDE_ONCE,             //  include_once
        T_REQUIRE,                  //  require
        T_REQUIRE_ONCE,             //  require_once

        //  Unused Tokens
        T_DOC_COMMENT,              // /** */
        T_COMMENT,                  // // # /* */
        T_WHITESPACE,               //  \t \r \n

        //  Basic Functions
        //  T_UNSET,                    //  unset()
        //  T_EMPTY,                    //  empty()
        //  T_EVAL,                     //  eval()
        //  T_EXIT,                     //  exit() die()
        //  T_HALT_COMPILER,            //  __halt_compiler()
        //  T_ISSET,                    //  isset()
        //  T_LIST,                     //  list()
        //  T_PRINT,                    //  print()

        //  PHP Code Limiters
        T_OPEN_TAG,                 //  <? <?php <%
        T_OPEN_TAG_WITH_ECHO,       //  <?= <%=
        T_CLOSE_TAG,                //  ?> %>
        T_INLINE_HTML,              //  any text outside php code tags

        //  Separators
        T_COMMA,                    //  ,
        T_SEMICOLON,                //  ;

        //  Heredoc
        T_START_HEREDOC,            //  <<<
        T_END_HEREDOC,              //  END

        //  Incorrect symbols
        //  T_BAD_CHARACTER, \n (0x0a) и \r (0x0d)
    }
}
