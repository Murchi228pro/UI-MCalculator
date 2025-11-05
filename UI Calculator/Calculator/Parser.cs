using System.Collections.Generic;
using System;
using System.Runtime.ExceptionServices;
using System.Linq;
using System.Diagnostics;

namespace Calculator
{
    public class ExpressionParser
    {
        private List<Token> ExprTokens;

        private int _current = 0;
        private int _lenght;
        private Token _currentToken;
        public ExpressionParser(List<Token> exprTokens)
        {
            ExprTokens = exprTokens;
            _lenght = exprTokens.Count();
        }

        public double Parse()
        {
            if (ExprTokens.First().Type == TokenType.EOF)
            {
                return 0;
            }
            _currentToken = ExprTokens.First();
            return Additive();
        }


        private bool Match(TokenType tokenType)
        {
            if (ExprTokens[_current].Type == TokenType.EOF)
            {
                return false;
            }
            if (ExprTokens[_current].Type == tokenType)
            {
                _currentToken = ExprTokens[_current];
                _current++;
                return true;
            }
            return false;
        }

        private double GetPrimaryValue()
        {
            return double.Parse(_currentToken.Value);
        }

        private double Additive()
        {
            double result = Multiplicative();
            while (_currentToken.Type != TokenType.EOF)
            {
                if (Match(TokenType.PLUS))
                {
                    result += Multiplicative();
                    continue;
                }
                if (Match(TokenType.MINUS))
                {
                    result -= result - Multiplicative();
                    continue;
                }
                break;
            }
            return result;
        }

        private double Multiplicative()
        {
            double result = Unary();
            while (_currentToken.Type != TokenType.EOF)
            {
                if (Match(TokenType.MUL))
                {
                    result *= Unary();
                    continue;
                }
                if (Match(TokenType.DIV))
                {
                    result /= Unary();
                    continue;
                }
                break;
            }
            return result;
        }

        private double Unary()
        {
            if (Match(TokenType.MINUS))
            {
                return -Primary();
            }
            if (Match(TokenType.PLUS))
            {
                return Primary();
            }
            return Primary();
        }

        private double Primary()
        {
            double result;
            if (Match(TokenType.LPAR))
            {
                result = Additive();
                Match(TokenType.RPAR);
            }
            else
            {
                Match(TokenType.LITERAL);
                result = GetPrimaryValue();
            }
            return result;
        }
    }
}

