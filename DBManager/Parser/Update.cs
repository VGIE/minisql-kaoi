using DbManager.Parser;
using System.Collections.Generic;

namespace DbManager
{
    public class Update: MiniSqlQuery
    {
        public string Table { get; private set; }
        public List<SetValue> Columns { get; private set; }
        public Condition Where { get; private set; }

        public Update(string table, List<SetValue> columnNames, Condition where)
        {
            //TODO DEADLINE 2: Initialize member variables
            Table = table ;
            Columns =columnNames ;
            Where = where;
        }

        public string Execute(Database database)
        {
            //TODO DEADLINE 3: Run the query and return the appropriate message
            //UpdateSuccess or the last error in the database
            
            if(database == null)
            {
                return Constants.Error;
            }
            else
            {
                Table t = database.TableByName(Table);
                if(t == null)
                {
                    return Constants.TableDoesNotExistError;
                }
                else
                {
                    ColumnDefinition colunm = t.ColumnByName(Where.ColumnName);
                    if(colunm == null || Where.ColumnName == null)
                    {
                        return Constants.ColumnDoesNotExistError;
                    }

                    foreach(SetValue st in Columns)
                    {
                        if(st.ColumnName == null || t.ColumnByName(st.ColumnName) == null)
                        {
                            return Constants.ColumnDoesNotExistError;
                        }
                    }
                    bool done =t.Update(Columns, Where);
                    if(done)
                    {
                       return Constants.UpdateSuccess; 
                    }
                    return Constants.SyntaxError;
                }
            }

        }

       
    }
}