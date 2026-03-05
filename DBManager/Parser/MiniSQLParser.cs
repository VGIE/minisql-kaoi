using DbManager.Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DbManager
{
    public class MiniSQLParser
    {
        public static MiniSqlQuery Parse(string miniSQLQuery)
        {
            //TODO DEADLINE 2
            const string selectPattern = @"^SELECT\s+(?P<columns>[A-Za-z][A-Za-z0-9]*(?:\s*,\s*[A-Za-z][A-Za-z0-9]*)*|\*)\s+FROM\s+(?P<table>[A-Za-z][A-Za-z0-9]*)(?:\s+WHERE\s+(?P<condition>.+))?\s*;?\s*$";
            
            const string insertPattern = @"^INSERT\s+INTO\s+(?P<table>[A-Za-z][A-Za-z0-9]*)\s+VALUES\s*\((?P<values>.+)\)\s*;?\s*$";
            
            const string dropTablePattern = @"^DROP\s+TABLE\s+(?P<table>[A-Za-z][A-Za-z0-9]*)\s*;?\s*$";
            
            //Note: The parsing of CREATE TABLE should accept empty columns "()"
            //And then, an execution error should be given if a CreateTable without columns is executed
            const string createTablePattern = @"^CREATE\s+TABLE\s+(?P<table>[A-Za-z][A-Za-z0-9]*)\s*\((?P<column_defs>[A-Za-z][A-Za-z0-9]*\s+(?:INT|DOUBLE|TEXT)(?:\s*,\s*[A-Za-z][A-Za-z0-9]*\s+(?:INT|DOUBLE|TEXT))*)\)\s*;?\s*$";
            
            const string updateTablePattern = @"^UPDATE\s+(?P<table>[A-Za-z][A-Za-z0-9]*)\s+SET\s+(?P<set_columns>[A-Za-z][A-Za-z0-9]*\s*=\s*[^,]+(?:\s*,\s*[A-Za-z][A-Za-z0-9]*\s*=\s*[^,]+)*)\s+WHERE\s+(?P<condition>.+)\s*;?\s*$";
            
            const string deletePattern = @"^DELETE\s+FROM\s+(?P<table>[A-Za-z][A-Za-z0-9]*)\s+WHERE\s+(?P<condition>.+)\s*;?\s*$";
            

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
            
            //TODO DEADLINE 4
            //Do the same for the security queries (CREATE SECURITY PROFILE, ...)
            
            return null;
           
        }

        static List<string> CommaSeparatedNames(string text)
        {
            string[] textParts = text.Split(",", System.StringSplitOptions.RemoveEmptyEntries);
            List<string> commaSeparator = new List<string>();
            for(int i=0; i < textParts.Length; i++)
            {
                commaSeparator.Add(textParts[i]);
            }
            return commaSeparator;
        }
        
    }
}
