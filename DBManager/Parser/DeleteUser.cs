using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
    public class DeleteUser : MiniSqlQuery
    {
        public string Username { get; private set; }

        public DeleteUser(string username)
        {
            //TODO DEADLINE 4: Initialize member variables
            this.Username = username;
            
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, UserDoesNotExistError, DeleteUserSuccess
            if (database == null)
            {
                return Constants.Error;
            }
            if (database.SecurityManager.IsUserAdmin() == false)
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            User u = database.SecurityManager.UserByName(Username);
            if (u == null)
            {
                return Constants.UserDoesNotExistError;
            }
            else
            {
                Profile prof = database.SecurityManager.ProfileByUser(Username);

                if (prof != null && prof.Users.Remove(u))
                {
                    return Constants.DeleteUserSuccess;
                }
                else
                {
                    return Constants.UserDoesNotExistError;
                }
            }
        }

    }
}
