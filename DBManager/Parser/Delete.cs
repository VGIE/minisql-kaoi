using System;
using System.Collections.Generic;
using System.Text;

namespace DbManager.Parser
{
    public class Delete : MiniSqlQuery
    {
        public string Table { get; private set; }
        public Condition Where { get; private set; }

        public Delete(string table, Condition where)
        {
            //TODO DEADLINE 2: Initialize member variables
            Table = table;
            Where = where;
        }

        public string Execute(Database database)
        {
            //TODO DEADLINE 3: Run the query and return the appropriate message
            //DeleteSuccess or the last error in the database           
            Table tabla = database.TableByName(Table);
            if (database==null)
                return Constants.Error;

            if (database.IsUserAdmin() == false)
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;

            if (tabla == null)
                return Constants.TableDoesNotExistError;

            if(database.DeleteWhere(Table, Where))
                return Constants.DeleteSuccess;
                
            return Constants.Error;          
        }
    }
}