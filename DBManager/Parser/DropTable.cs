using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbManager
{
    public class DropTable: MiniSqlQuery
    {
        public string Table { get; private set; }

        public DropTable(string table)
        {
            //TODO DEADLINE 2: Initialize member variables
            Table = table;
        }

        public string Execute(Database database)
        {
            //TODO DEADLINE 3: Run the query and return the appropriate message
            //DropTableSuccess or the last error in the database
            Table tabla = database.TableByName(Table);

            if (database == null)
                return Constants.Error;

            if (tabla == null)
                return Constants.TableDoesNotExistError;

            if (database.DropTable(Table))
                return Constants.DropTableSuccess;


            return Constants.Error;
            
        }
    }
}
