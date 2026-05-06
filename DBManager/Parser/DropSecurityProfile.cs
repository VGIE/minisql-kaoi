using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
    public class DropSecurityProfile : MiniSqlQuery
    {
        public string ProfileName { get; set; }

        public DropSecurityProfile(string profileName)
        {
            //TODO DEADLINE 4: Initialize member variables
            this.ProfileName = profileName;
            
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, DropSecurityProfileSuccess
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
            bool result = database.SecurityManager.RemoveProfile(ProfileName);
            if (result == true)
            {
               return Constants.DropSecurityProfileSuccess; 
            }
            return null;
            
        }

    }
}
