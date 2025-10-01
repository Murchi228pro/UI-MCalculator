using System.Collections.Generic;
using System;
using System.Runtime.ExceptionServices;
using System.Linq;
using System.Diagnostics;

namespace Calculator
{
    public class Parser
    {
        private int _position;
        private Token _current;
        private int _size;
        private List<Token> _tokens;

        public double Value { get; }

        public Parser(List<Token> tokens)
        {
            this._tokens = tokens;
            this._size = _tokens.Count;

            this._current = _tokens[0];
            Value = Expr();
        }

        private double Expr()
        {
            if (Match(TokenType.EOF))
            {
                return 0;
            }
            return Add();
        }

        private double Add()
        {
            double first = Mul();
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

        private double Mul()
        {
            double first = Unary();

            if (Match(TokenType.MUL))
            {
                return (first * Unary());
            }

            if (Match(TokenType.DIV))
            {
                return (first / Unary());
            }

            return first;
        }

        private double Unary()
        {
            if (Match(TokenType.MINUS))
            {
                return -Primary();
            }

            Match(TokenType.PLUS);
            return Primary();
        }


        private double Primary()
        {
            if (Match(TokenType.LPAR))
            {
                double number = Expr();
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
                Debug.WriteLine(_current.Value);
                return double.Parse(_current.Value);
            }

        }

        private bool Match(TokenType tokenType)
        {

            _current = _tokens[_position];
            if (_current.Type == tokenType)
            {
                _position++;
                return true;
            }
            return false;
        }
    }
}

