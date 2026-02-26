using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DbManager
{
    public class Row
    {
        private List<ColumnDefinition> ColumnDefinitions = new List<ColumnDefinition>();
        public List<string> Values { get; set; }

        public Row(List<ColumnDefinition> columnDefinitions, List<string> values)
        {
            //TODO DEADLINE 1.A: Initialize member variables
            this.Values = values;
            this.ColumnDefinitions = columnDefinitions;
            while (Values.Count < ColumnDefinitions.Count)
            {
                Values.Add(null);
            }
            
        }

        public void SetValue(string columnName, string value)
        {
            //TODO DEADLINE 1.A: Given a column name and value, change the value in that column
            for (int i = 0; i < ColumnDefinitions.Count; i++)
            {
                if (ColumnDefinitions[i].Name == columnName)
                {
                    Values[i]=value;
                }
            }
        }

        public string GetValue(string columnName)
        {
            //TODO DEADLINE 1.A: Given a column name, return the value in that column
            for(int i = 0; i <ColumnDefinitions.Count; i++)
            {
                if(ColumnDefinitions[i].Name == columnName)
                {
                    if (i < Values.Count)
                    {
                        return Values[i];
                    }
                }
                //resultado = Values.Contains(columnName).ToString();
            }

            return null;
            
        }

        public bool IsTrue(Condition condition)
        {
            //TODO DEADLINE 1.A: Given a condition (column name, operator and literal value, return whether it is true or not
            //for this row. Check Condition.IsTrue method

            string value = GetValue(condition.ColumnName);
            for (int i = 0; i < ColumnDefinitions.Count; i++)
            {
                if (ColumnDefinitions[i].Name == condition.ColumnName)
                {
                    return condition.IsTrue(value, ColumnDefinitions[i].Type);

                }
            }
            return false;
            
        }

        private const string Delimiter = ":";
        private const string DelimiterEncoded = "[SEPARATOR]";

        private static string Encode(string value)
        {
            //TODO DEADLINE 1.C: Encode the delimiter in value
            //si el nombre tiene espacio lo guardamos etc, codificamos con la inversa
            //con las llaves space o lo que hayhamos usado y lo que da (no entiendo)

            
            return null;
            
        }

        private static string Decode(string value)
        {
            //TODO DEADLINE 1.C: Decode the value doing the opposite of Encode()
            
            return null;
            
        }

        public string AsText()
        {
            //TODO DEADLINE 1.C: Return the row as string with all values separated by the delimiter
            
            return null;
            
        }

        public static Row Parse(List<ColumnDefinition> columns, string value)
        {
            //TODO DEADLINE 1.C: Parse a rowReturn the row as string with all values separated by the delimiter
            
            return null;
            
        }
    }
}
