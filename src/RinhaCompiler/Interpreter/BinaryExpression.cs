﻿using System.Text.Json.Serialization;

namespace RinhaCompiler.Interpreter;

public sealed class BinaryExpression : Expression
{
    [JsonPropertyName("lhs")]
    public Expression LeftHandSide { get; set; }

    [JsonPropertyName("rhs")]
    public Expression RightHandSide { get; set; }

    [JsonPropertyName("op")]
    public string Operation { get; set; }

    private BinaryOperator GetOperation() => (BinaryOperator)Enum.Parse(typeof(BinaryOperator), Operation, true);

    public override object Run()
    {
        var leftSide = RunUntil.Find<ValueExpression>(LeftHandSide);
        var rightSide = RunUntil.Find<ValueExpression>(RightHandSide);

        switch (GetOperation())
        {
            case BinaryOperator.Add:
                return AddOperation(leftSide, rightSide);
            case BinaryOperator.Sub:
                return SubOperation(leftSide, rightSide);
            case BinaryOperator.Mul:
                return MultiOperation(leftSide, rightSide);
            case BinaryOperator.Div:
                return DivOperation(leftSide, rightSide);
            case BinaryOperator.Rem:
                return RemOperation(leftSide, rightSide);
            case BinaryOperator.Eq:
                return EqualOperation(leftSide, rightSide);
            case BinaryOperator.Neq:
                return NotEqualOperation(leftSide, rightSide);
            case BinaryOperator.Lt:
                return LessThanOperation(leftSide, rightSide);
            case BinaryOperator.Gt:
                return GreaterThanOperation(leftSide, rightSide);
            case BinaryOperator.Lte:
                return LessOrEqualThanOperation(leftSide, rightSide);
            case BinaryOperator.Gte:
                return GreaterOrEqualThanOperation(leftSide, rightSide);
            case BinaryOperator.And:
                return AndOperation(leftSide, rightSide);
            case BinaryOperator.Or:
                return OrOperation(leftSide, rightSide);
            default:
                throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
        }

    }

    private Expression AddOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value + rightSideIntValue.Value, Location = leftSide.Location };
            }
            else if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<string> { Value = leftSideIntValue.Value + rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        else if (leftSide is ValueExpression<string> leftSideStrValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<string> { Value = leftSideStrValue.Value + rightSideIntValue.Value, Location = leftSide.Location };
            }
            else if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<string> { Value = leftSideStrValue.Value + rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> SubOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value - rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> MultiOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value * rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> DivOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value / rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> RemOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value % rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> EqualOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value == rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        else if (leftSide is ValueExpression<string> leftSideStrValue)
        {
            if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<bool> { Value = leftSideStrValue.Value == rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> NotEqualOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value != rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        else if (leftSide is ValueExpression<string> leftSideStrValue)
        {
            if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<bool> { Value = leftSideStrValue.Value != rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> LessThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value < rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> LessOrEqualThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value <= rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> GreaterThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value < rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> GreaterOrEqualThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value <= rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> AndOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<bool> leftSideIntValue)
        {
            if (rightSide is ValueExpression<bool> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value && rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> OrOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<bool> leftSideIntValue)
        {
            if (rightSide is ValueExpression<bool> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value && rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on {leftSide.Location.GetLog()}");
    }
}