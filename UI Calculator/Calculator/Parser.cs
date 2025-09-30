using System.Collections.Generic;
using System;
using System.Runtime.ExceptionServices;
using System.Linq;

namespace Calculator
{
    public class Parser
    {
        private int _position;
        private Token _current;
        private int _size;
        private List<Token> _tokens;

        public int Value;

        public Parser(List<Token> tokens)
        {
            this._tokens = tokens;
            this._size = _tokens.Count;

            this._current = _tokens[0];
            Value = Expr();
        }

        private int Expr()
        {
            if (Match(TokenType.EOF))
            {
                return 0;
            }
            return Add();
        }

        private int Add()
        {
            int first = Mul();
            if (Match(TokenType.PLUS))
            {
                return (first + Mul());
            }

            if (Match(TokenType.MINUS))
            {
                return (first - Mul());
            }
            return first;
        }

        private int Mul()
        {
            int first = Unary();

            if (Match(TokenType.MUL))
            {
                return (first * Unary());
            }

            if (Match(TokenType.DIV))
            {
                return (first * Unary());
            }

            return first;
        }

        private int Unary()
        {
            if (Match(TokenType.MINUS))
            {
                return -Primary();
            }

            Match(TokenType.PLUS);
            return Primary();
        }


        private int Primary()
        {
            if (Match(TokenType.LPAR))
            {
                int number = Expr();
                Match(TokenType.RPAR);
                if (Match(TokenType.LITERAL))
                {
                    return number * Primary();
                }
                return number;
            }
            else
            {
                Match(TokenType.LITERAL);
   
                return int.Parse(_current.Value);
            }

        }

        private bool Match(TokenType tokenType)
        {

            _current = _tokens[_position];
            Console.WriteLine(_current.Value);
            if (_current.Type == tokenType)
            {
                _position++;
                return true;
            }
            return false;
        }
    }
}

