using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
    public class Grant : MiniSqlQuery
    {
        public string PrivilegeName { get; set; }
        public string TableName { get; set; }
        public string ProfileName { get; set; }

        public Grant(string privilegeName, string tableName, string profileName)
        {
            //TODO DEADLINE 4: Initialize member variables
            this.PrivilegeName = privilegeName;
            this.TableName = tableName;
            this.ProfileName = profileName;
            
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, PrivilegeDoesNotExistError, GrantPrivilegeSuccess, ProfileAlreadyHasPrivilege
            if (database == null)
            {
                return Constants.Error;
            }
            if (!database.IsUserAdmin())
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            Profile profile = database.SecurityManager.ProfileByName(ProfileName);
            if (profile == null)
            {
                return Constants.SecurityProfileDoesNotExistError;
            }
            Privilege priv;
            try
            {
                priv = PrivilegeUtils.FromPrivilegeName(PrivilegeName);
            }
            catch
            {
                return Constants.PrivilegeDoesNotExistError;
            }

            if (profile.IsGrantedPrivilege(TableName, priv))
            {
                return Constants.ProfileAlreadyHasPrivilege;
            }
            database.SecurityManager.GrantPrivilege(ProfileName, TableName, priv);
            return Constants.GrantPrivilegeSuccess;
        }

    }
}
