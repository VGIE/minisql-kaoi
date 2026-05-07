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
            
            else if (database.IsUserAdmin() == false)
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            Profile p = database.SecurityManager.ProfileByName(ProfileName);
            if (p == null)
            {
                return Constants.SecurityProfileDoesNotExistError;
            }
            User newUser = new User(Username,Password);
            p.Users.Add(newUser);
            return Constants.AddUserSuccess;
            
        }

    }
}
