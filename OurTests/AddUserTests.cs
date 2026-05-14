using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DbManager.Security;
using DbManager;

namespace OurTests
{
    public class AddUserTest
    {
        [Fact]
        public void AddUserSuccess()
        {
            Database db = new Database(Database.AdminUsername, Database.AdminPassword);

            Profile profile = new Profile();
            profile.Name = "userProfile";
            db.SecurityManager.Profiles.Add(profile);

            AddUser query = new AddUser("user", "1234", "userProfile");
            string result = query.Execute(db);

            Assert.Equal(Constants.AddUserSuccess, result);
            Assert.NotNull(db.SecurityManager.UserByName("user"));
        }

        [Fact]
        public void AddUserDuplicate()
        {
            Database db = new Database(Database.AdminUsername, Database.AdminPassword);
            
            Profile p = new Profile();
            p.Name = "p1";
            p.Users.Add(new User("user", "1234"));
            db.SecurityManager.Profiles.Add(p);

            AddUser query = new AddUser("user", "5678", "p1");
            Assert.Equal(Constants.Error, query.Execute(db));
        }

        [Fact]
        public void AddUserNoProfile()
        {
            Database db = new Database(Database.AdminUsername, Database.AdminPassword);

            AddUser query = new AddUser("user", "1234", "none");
            string result = query.Execute(db);

            Assert.Equal(Constants.SecurityProfileDoesNotExistError, result);
        }

        [Fact]
        public void AddUserEmptyPassword()
        {
            Database db = new Database(Database.AdminUsername, Database.AdminPassword);
            
            Profile p = new Profile();
            p.Name = "p1";
            db.SecurityManager.Profiles.Add(p);

            AddUser query = new AddUser("user", "", "p1");
            string result = query.Execute(db);

            Assert.Equal(Constants.Error, result);
        }
    }
}
