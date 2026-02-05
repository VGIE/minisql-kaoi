using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DbManager;

namespace DbManager
{
    public class Condition
    {
        public string ColumnName { get; private set; }
        public string Operator { get; private set; }
        public string LiteralValue { get; private set; }

        public Condition(string column, string op, string literalValue)
        {
            //TODO DEADLINE 1A: Initialize member variables
            this.ColumnName = column;
            this.Operator = op;
            this.LiteralValue = literalValue;
        }


        public bool IsTrue(string value, ColumnDefinition.DataType type)
        {
            //TODO DEADLINE 1A: return true if the condition is true for this value
            //Depending on the type of the column, the comparison should be different:
            //"ab" < "cd
            //"9" > "10"
            //9 < 10
            //Convert first the strings to the appropriate type and then compare (depending on the operator of the condition)
            if (type == ColumnDefinition.DataType.String)
            {
                int comparison = string.Compare(value, this.LiteralValue, StringComparison.Ordinal);
                switch (this.Operator)
                {
                    case "=":
                        return comparison == 0;
                    case "<":
                        return comparison < 0;
                    case ">":
                        return comparison > 0;
                    case "<=":
                        return comparison <= 0;
                    case ">=":
                        return comparison >= 0;
                    default:
                        return false;
                }
            }
            else if (type == ColumnDefinition.DataType.Int)
            {
                int valueInt = int.Parse(value);
                int literalValueInt = int.Parse(this.LiteralValue);
                switch (this.Operator)
                {
                    case "=":
                        return valueInt == literalValueInt;
                    case "<":
                        return valueInt < literalValueInt;
                    case ">":
                        return valueInt > literalValueInt;
                    case "<=":
                        return valueInt <= literalValueInt;
                    case ">=":
                        return valueInt >= literalValueInt;
                    default:
                        return false;
                }
            }
            else
            {
                double valueDouble = double.Parse(value, CultureInfo.InvariantCulture);
                double literalValueDouble = double.Parse(this.LiteralValue, CultureInfo.InvariantCulture);
                switch (this.Operator)
                {
                    case "=":
                        return valueDouble == literalValueDouble;
                    case "<":
                        return valueDouble < literalValueDouble;
                    case ">":
                        return valueDouble > literalValueDouble;
                    case "<=":
                        return valueDouble <= literalValueDouble;
                    case ">=":
                        return valueDouble >= literalValueDouble;
                    default:
                        return false;
                }
            }            
        }
    }
}