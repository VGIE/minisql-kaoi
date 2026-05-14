using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
    public class Revoke : MiniSqlQuery
    {
        public string PrivilegeName { get; set; }
        public string TableName { get; set; }
        public string ProfileName { get; set; }

        public Revoke(string privilegeName, string tableName, string profileName)
        {
            //TODO DEADLINE 4: Initialize member variables
            this.PrivilegeName = privilegeName;
            this.TableName = tableName;
            this.ProfileName = profileName;
            
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, RevokePrivilegeSuccess, 
            
            if(database == null)
            {
                return Constants.Error;
            }

            if(database.IsUserAdmin() == false)
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }

            Profile profile = database.SecurityManager.ProfileByName(ProfileName);
            if (profile == null)
            {
                return Constants.SecurityProfileDoesNotExistError;
            }

            Privilege privilege;
            try
            {
                privilege = PrivilegeUtils.FromPrivilegeName(PrivilegeName);
            }
            catch (Exception)
            {
                return Constants.PrivilegeDoesNotExistError;
            }
            if (!profile.IsGrantedPrivilege(TableName, privilege))
            {
                return Constants.PrivilegeDoesNotExistError;
            }
            database.SecurityManager.RevokePrivilege(ProfileName, TableName, privilege);

            return Constants.RevokePrivilegeSuccess;

        
            
        }

    }
}
