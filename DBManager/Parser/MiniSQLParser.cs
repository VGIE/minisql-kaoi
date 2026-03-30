using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace DbManager
{
    public class MiniSQLParser
    {
        public static MiniSqlQuery Parse(string miniSQLQuery)
        {
            //TODO DEADLINE 2
            const string selectPattern = @"^SELECT\s+(?<colums>[a-zA-Z0-9]+(?:,[a-zA-Z0-9]+)*)\s+FROM\s+(?<table>[a-zA-Z0-9]+)(?:\s+WHERE\s+(?<colName>[a-zA-Z0-9]+)(?<operator>=|>|<)(?<Value>'[^']*'|[0-9]+(?:\.[0-9]+)?))?$";

            const string insertPattern = @"^INSERT\s+INTO\s+(?<table>[A-Za-z][A-Za-z0-9]*)\s+VALUES\s*\((?<values>.+)\)\s*;?\s*$";

            const string dropTablePattern = @"^DROP\s+TABLE\s+(?<table>[A-Za-z][A-Za-z0-9]*)\s*;?\s*$";

            //Note: The parsing of CREATE TABLE should accept empty columns "()"
            //And then, an execution error should be given if a CreateTable without columns is executed
            const string createTablePattern = @"^CREATE\s+TABLE\s+(?<table>[A-Za-z][A-Za-z0-9]*)\s*\(\s*(?<column_defs>(?:[A-Za-z][A-Za-z0-9]*\s+(?:INT|DOUBLE|TEXT)\s*(?:,\s*[A-Za-z][A-Za-z0-9]*\s+(?:INT|DOUBLE|TEXT)\s*)*)?)\s*\)\s*;?\s*$";

            const string updateTablePattern = @"^UPDATE\s+(?<table>\w+)\s+SET\s+(?<set_columns>(\w+)(=)(\'-?\d+(\.\d+)?\'|'[^']+')(,(\w+)(=)(\'-?\d+(\.\d+)?\'|'[^']+'))*)*\s+WHERE\s+(?<columnName>\w+)(?<operator>=|<|>)(?<value>\'-?\d+(?<decimals>\.\d+)?\'|'[^']+')$";

            const string deletePattern = @"^DELETE\s+FROM\s+(?<table>\w+)\s+WHERE\s+(?<columnName>\w+)(?<operator>=|<|>)(?<literalValue>\'-?\d+(?<values>\.\d+)?\'|'[^']+')$";
            //espacio despues y antes de la coma, no deberia aceptarlos

            //TODO DEADLINE 4
            const string createSecurityProfilePattern = null;

            const string dropSecurityProfilePattern = null;

            const string grantPattern = null;

            const string revokePattern = null;

            const string addUserPattern = null;

            const string deleteUserPattern = null;


            //TODO DEADLINE 2
            //Parse query using the regular expressions above one by one. If there is a match, create an instance of the query with the parsed parameters
            //For example, if the query is a "SELECT ...", there should be a match with selectPattern. We would create and return an instance of Select
            //initialized with the table name, the columns, and (possibly) an instance of Condition.
            //If there is no match, it means there is a syntax error. We will return null.
            if (Regex.IsMatch(miniSQLQuery, selectPattern))
            {
                Match match = Regex.Match(miniSQLQuery, selectPattern);
                string tableName = match.Groups["table"].Value;
                string columnsname = match.Groups["colums"].Value;
                List<string> columnslist = CommaSeparatedNames(columnsname);

                if (match.Groups["colName"].Success &&
                   match.Groups["operator"].Success &&
                   match.Groups["Value"].Success)
                {
                    string condcloumn = match.Groups["colName"].Value;
                    string op = match.Groups["operator"].Value;
                    string values = match.Groups["Value"].Value;
                    if (Regex.IsMatch(values, @"^'.*'$"))
                    {
                        values = values.Trim('\'');
                    }
                    Condition cond = new Condition(condcloumn, op, values);

                    return new Select(tableName, columnslist, cond);
                }
                
                return new Select(tableName, columnslist, null);

            }
            else if (Regex.IsMatch(miniSQLQuery, insertPattern))
            {
                Match match = Regex.Match(miniSQLQuery, insertPattern);
                string tableName = match.Groups["table"].Value;
                string values = match.Groups["values"].Value;
                List<string> listvalues = CommaSeparatedNames(values);
                if (listvalues.Count == 1 && listvalues[0].Split('\'').Length-1 > 2)
                {
                    return null;
                }
                foreach (string valus in listvalues)
                {
                    char l1 = valus[0];
                    char l2 = valus[valus.Length-1];
                    if (l1 !='\'' || l2 != '\'' )
                    {
                        return null;
                    }
                }
                for(int i = 0; i < listvalues.Count; i++)
                {
                    if (Regex.IsMatch(listvalues[i], @"^'.*'$"))
                    {
                        listvalues[i] = listvalues[i].Trim('\'');
                    }
                }

                return new Insert(tableName, listvalues);
            }
            else if (Regex.IsMatch(miniSQLQuery, dropTablePattern))
            {
                Match match = Regex.Match(miniSQLQuery, dropTablePattern);
                string tableName = match.Groups["table"].Value;
                return new DropTable(tableName);
            }
            else if (Regex.IsMatch(miniSQLQuery, createTablePattern))
            {
                Match match = Regex.Match(miniSQLQuery, createTablePattern);
                string tableName = match.Groups["table"].Value;
                string columns = match.Groups["column_defs"].Value;

                List<ColumnDefinition> cd = new List<ColumnDefinition>();
                ColumnDefinition.DataType t = ColumnDefinition.DataType.String;
                string name = null;
                if (columns != null || columns!="")
                {
                    List<string> separetec = CommaSeparatedNames(columns);
                    foreach (string valus in separetec)
                    {
                        if (valus.StartsWith(" ") || valus.EndsWith(" "))
                        {
                            return null;
                        }
                    }
                        for (int i = 0; i < separetec.Count; i++)
                    {
                        string[] type = separetec[i].Split(" ");
                        if(type[1] == "TEXT")
                        {
                            t = ColumnDefinition.DataType.String;
                            name = type[0];
                        }
                        else if (type[1] == "INT")
                        {
                            t = ColumnDefinition.DataType.Int;
                            name = type[0];
                        }
                        else if (type[1] == "DOUBLE")
                        {
                            t = ColumnDefinition.DataType.Double;
                            name = type[0];
                        }
                        cd.Add(new ColumnDefinition(t, name));
                    }
                    
                }


                return new CreateTable(tableName,cd);
            }
            else if (Regex.IsMatch(miniSQLQuery, updateTablePattern))
            {
                Match match = Regex.Match(miniSQLQuery, updateTablePattern);
                string tableName = match.Groups["table"].Value;
                string updateColumns = match.Groups["set_columns"].Value;
                List<string> toUpdateColumns = CommaSeparatedNames(updateColumns);
                string cdcolumn = match.Groups["columnName"].Value;
                string op = match.Groups["operator"].Value;
                string value = match.Groups["value"].Value;
                List<SetValue> setValue = new List<SetValue>();
                 for (int i = 0; i < toUpdateColumns.Count; i++)
                {
                    string[] partes = toUpdateColumns[i].Split("=");
                    string column = partes[0];
                    string value2 = partes[1];
                    if (Regex.IsMatch(value2, @"^'.*'$"))
                    {
                        value2 = value2.Trim('\'');
                    }
                    setValue.Add(new SetValue(column, value2));
                }
                 if (Regex.IsMatch(value, @"^'.*'$"))
                    {
                        value = value.Trim('\'');
                    }
                return new Update(tableName, setValue, new Condition(cdcolumn, op, value));

            }
            else if (Regex.IsMatch(miniSQLQuery, deletePattern))
            {
                Match match = Regex.Match(miniSQLQuery, deletePattern);
                string tableName = match.Groups["table"].Value;
                string column = match.Groups["columnName"].Value;
                string op = match.Groups["operator"].Value;
                string literalValue = match.Groups["literalValue"].Value;
                if (Regex.IsMatch(literalValue, @"^'.*'$"))
                    {
                        literalValue = literalValue.Trim('\'');
                    }

                return new Delete(tableName, new Condition(column, op, literalValue));

            }
            //TODO DEADLINE 4
            //Do the same for the security queries (CREATE SECURITY PROFILE, ...)

            return null;

        }

        static List<string> CommaSeparatedNames(string text)
        {
            string[] textParts = text.Split(",", System.StringSplitOptions.RemoveEmptyEntries);
            List<string> commaSeparator = new List<string>();
            for (int i = 0; i < textParts.Length; i++)
            {
                commaSeparator.Add(textParts[i]);
            }
            return commaSeparator;
        }

    }
}
