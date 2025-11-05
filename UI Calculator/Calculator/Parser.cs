using System.Collections.Generic;
using System;
using System.Runtime.ExceptionServices;
using System.Linq;
using System.Diagnostics;

namespace Calculator
{
    public class ExpressionParser
    {
        private List<Token> _exprTokens;

        private int _current = 0;
        private int _lenght;
        private Token _currentToken;
        public ExpressionParser(List<Token> exprTokens)
        {
            _exprTokens = exprTokens;
            _lenght = exprTokens.Count();
        }

        public double Parse()
        {
            if (_exprTokens.First().Type == TokenType.EOF)
            {
                return 0;
            }
            _currentToken = _exprTokens.First();
            return Additive();
        }


        private bool Match(TokenType tokenType)
        {
            if (_exprTokens[_current].Type == TokenType.EOF)
            {
                return false;
            }
            if (_exprTokens[_current].Type == tokenType)
            {
                _currentToken = _exprTokens[_current];
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
                Debug.WriteLine(result);
                if (Match(TokenType.PLUS))
                {
                    result += Multiplicative();
                    continue;
                }
                if (Match(TokenType.MINUS))
                {
                    result -= Multiplicative();
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
            double result;
            if (Match(TokenType.MINUS))
            {
                result = -Primary();
            }
            else if (Match(TokenType.PLUS))
            {
                result = Primary();
                
            }
            else
            {
                result = Primary();
            }
            return result;
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

