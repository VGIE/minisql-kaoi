using DbManager;
using DbManager.Parser;
using DbManager.Security;
using Xunit;

namespace OurTests
{
    public class ManagerTest
    {
        [Fact]
        public void IsUserAdminTest()
        {
            string adminName = "admin";
            Manager manager = new Manager(adminName);
            
            Profile adminProfile = new Profile { Name = Profile.AdminProfileName };

            User adminUser = new User {Username = "admin"};
            adminProfile.Users.Add(adminUser);
            manager.Profiles.Add(adminProfile);

            Assert.True(manager.IsUserAdmin());
        }

        [Fact]
        public void IsUserNotAdminTest()
        {
            string adminName = "admin";
            Manager manager = new Manager(adminName);
            
            Profile userProfile = new Profile { Name = "UserProfile" };

            User normalUser = new User {Username = "user"};
            userProfile.Users.Add(normalUser);
            manager.Profiles.Add(userProfile);

            Assert.False(manager.IsUserAdmin());
        }

        [Fact]
        public void IsUserAdminNullTest()
        {
            string adminName = "admin";
            Manager manager = new Manager(adminName);
            
            Assert.False(manager.IsUserAdmin());
        }

        [Fact]
        public void IsGrantedPrivilegeTest()
        {
            string adminName = "admin";
            Manager manager = new Manager(adminName);
            
            Profile adminProfile = new Profile { Name = Profile.AdminProfileName };

            User adminUser = new User {Username = "admin"};
            adminProfile.Users.Add(adminUser);
            manager.Profiles.Add(adminProfile);

            Assert.True(manager.IsGrantedPrivilege("admin", "table1", Privilege.Select));
        }

        [Fact]
        public void GrantedPrivilegeTest()
        {
            User adminUser = new User { Username = "admin" };
            Profile adminProfile = new Profile { Name = Profile.AdminProfileName };
            adminProfile.Users.Add(adminUser);
            
            User normalUser = new User { Username = "user" };
            Profile userProfile = new Profile { Name = "UserProfile" };
            userProfile.Users.Add(normalUser);

            Manager manager = new Manager("admin");
            manager.Profiles.Add(adminProfile);
            manager.Profiles.Add(userProfile);

            string tableName = "table1";
            manager.GrantPrivilege("UserProfile",tableName, Privilege.Select);
            Assert.True(manager.IsGrantedPrivilege("user", tableName, Privilege.Select));

        }
            


    }
}