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
        EndOfFile,
        
        DocComment,                 // /** */
        Comment,                    // // # /* */
        Whitespace,                 //  \t \r \n
        
        PHPOpenTag,                 //  <? <?php <%
        PHPOpenTagWithEcho,         //  <?= <%=
        PHPCloseTag,                //  ?> %>
        
        Comma,                      //  ,
        Semicolon,                  //  ;
        
        BraceOpen,                  //  (
        BraceClose,                 //  )
        CurlyBraceOpen,             //  {  
        CurlyBraceClose,            //  }
        SquareBraceOpen,            //  [
        SquareBraceClose,           //  ]
        
        Add,                        //  +
        Sub,                        //  -
        Mul,                        //  *
        Div,                        //  /
        Mod,                        //  %
        Pow,                        //  **
        Concat,                     //  .

        Assignment,                 //  =
        AssignmentAdd,              //  +=
        AssignmentSub,              //  -=
        AssignmentMul,              //  *=
        AssignmentDiv,              //  /=
        AssignmentMod,              //  %=
        AssignmentPow,              //  **=
        AssignmentConcat,           //  .=
        
        BitAnd,                     //  &
        BitXor,                     //  ^
        BitNot,                     //  |
        BitShiftLeft,               //  <<
        BitShiftRight,              //  >>

        AssignmentBitAnd,           //  &=
        AssignmentBitXor,           //  ^=
        AssignmentBitNot,           //  |=
        AssignmentBitShiftLeft,     //  <<=
        AssignmentBitShiftRight,    //  >>=
        
        Coalesce,                   //  ??
        AssignmentCoalesce,         //  ??=

        LogicalAnd,                 //  && and
        LogicalOr,                  //  || or
        LogicalXor,                 //  xor

        IsEqual,                    //  ==
        IsIdentical,                //  ===
        IsNotEqual,                 //  != <>
        IsNotIdentical,             //  !==
        IsGreater,                  // >
        IsSmaller,                  // <
        IsGreaterOrEqual,           //  >=
        IsSmallerOrEqual,           //  <=
        IsSpaceship,                //  <=>

        Decrement,                  //  --
        Increment,                  //  ++

        Integer,                    //  123 012 0x1ac
        Float,                      //  1.5
        String,                     //  "" ''

        Variable,                 //  $var_1
/*
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

        //  Basic Functions
        //  T_UNSET,                    //  unset()
        //  T_EMPTY,                    //  empty()
        //  T_EVAL,                     //  eval()
        //  T_EXIT,                     //  exit() die()
        //  T_HALT_COMPILER,            //  __halt_compiler()
        //  T_ISSET,                    //  isset()
        //  T_LIST,                     //  list()
        //  T_PRINT,                    //  print()

        */
        //  Incorrect symbols
        //  T_BAD_CHARACTER, \n (0x0a) и \r (0x0d)
    }
}
