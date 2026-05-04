using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;

namespace DbManager
{
 
    public class CreateSecurityProfile : MiniSqlQuery
    {
        public string ProfileName { get; set; }

        public CreateSecurityProfile(string profileName)
        {
            //TODO DEADLINE 4: Initialize member variables
            this.ProfileName = profileName;
            
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, CreateSecurityProfileSuccess
            if(database == null)
            {
                return Constants.Error;
            }
            if(!database.IsUserAdmin())
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            else
            {
                database.SecurityManager.Profiles.Add(new Security.Profile{Name = this.ProfileName});
                return Constants.CreateSecurityProfileSuccess;
            }
            
        }

    }
}
