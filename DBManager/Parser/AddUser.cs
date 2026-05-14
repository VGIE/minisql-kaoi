using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
    public class AddUser : MiniSqlQuery
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ProfileName { get; private set; }


        public AddUser(string username, string password, string profileName)
        {
            //TODO DEADLINE 4: Initialize member variables
            this.Username = username;
            this.Password = password;
            this.ProfileName = profileName;
            
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, AddUserSuccess
            if(database == null)
            {
                return Constants.Error;
            }

            if (!database.IsUserAdmin())
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }

            Profile p = database.SecurityManager.ProfileByName(ProfileName);
            if (p == null)
            {
                return Constants.SecurityProfileDoesNotExistError;
            }

            if (database.SecurityManager.UserByName(Username) != null)
            {
                return Constants.Error;
            }

            if (string.IsNullOrEmpty(Password))
            {
                return Constants.Error;
            }

            User newUser = new User(Username, Password);
            p.Users.Add(newUser);

            return Constants.AddUserSuccess;
        }

    }
}
