using DbManager;
using DbManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTests
{
    public class DeleteUserTestExecute
    {
        [Fact]
        public void IncorrectUserDoesNotExist()
        {
            Database db = new Database("Admin", "1234");

            Manager manager = db.SecurityManager;

            Profile adminProfile = new Profile { Name = "Admin" };
            User adminUser = new User("Admin", "1234");
            adminProfile.Users.Add(adminUser);
            manager.Profiles.Add(adminProfile);

            Profile profile = new Profile { Name = "Test" };
            manager.Profiles.Add(profile);

            DeleteUser query = new DeleteUser("Kevin");
            string result = query.Execute(db);

            Assert.Equal(Constants.UserDoesNotExistError, result);
        }

        [Fact]
        public void IncorrectWithoutProf()
        {
            Database db = new Database("Admin", "1234");

            Manager manager = db.SecurityManager;

            Profile adminProf = new Profile { Name = "Admin" };
            User adminUser = new User("Admin", "1234");
            adminProf.Users.Add(adminUser);
            manager.Profiles.Add(adminProf);

            DeleteUser query = new DeleteUser("Kevin");
            string result = query.Execute(db);

            Assert.Equal(Constants.UserDoesNotExistError, result);
        }

    }
}
