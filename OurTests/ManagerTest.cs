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

        [Fact]
        public void AddProfileTest()
        {
            Manager manager = new Manager("Admin");
            Profile admin = new Profile { Name = Profile.AdminProfileName };
            User useradmin = new User { Username = "Admin" };
            admin.Users.Add(useradmin);
            manager.Profiles.Add(admin);
            
            Profile newProfile = new Profile { Name = "NewProfile" };
            manager.AddProfile(newProfile);
            Assert.Contains(newProfile, manager.Profiles);
        }

        [Fact]
        public void AddProfileTest()
        {
            Manager manager = new Manager("Admin");
            Profile admin = new Profile { Name = Profile.AdminProfileName };
            User useradmin = new User { Username = "Admin" };
            admin.Users.Add(useradmin);
            manager.Profiles.Add(admin);
            
            Profile newProfile = new Profile { Name = "NewProfile" };
            manager.AddProfile(newProfile);
            Assert.Contains(newProfile, manager.Profiles);
        }
        
        [Fact]
        public void UserByNameTest()
        {
            User a1 = new User();
            User a2 = new User();
            a1.Username = "Admin";
            a2.Username = "Admin";


            Profile p1 = new Profile{ Name = "UserProfile"};
            p1.Name = Profile.AdminProfileName;
            p1.Users.Add(a1);
            p1.Users.Add(a2);
        
            Manager m = new Manager("admin");
            m.Profiles.Add(p1);
            User foundUser = m.UserByName("Admin");
        Assert.NotNull(foundUser);
        Assert.Equal("Admin", foundUser.Username);

        }

        [Fact]
        public void ProfileByNameTest()
        {
            User u1 = new User("user1", "1234");
            User u2 = new User("user2", "1234");
            User u3 = new User("user3", "1234");

            User a1 = new User("admin1", "1235");
            User a2 = new User("admin2", "1235");


            Profile p1 = new Profile{ Name = "UserProfile"};
            p1.Users.Add(a1);
            p1.Users.Add(a2);

            Profile p2 = new Profile{ Name = "UserProfile"};
            p2.Users.Add(u1);
            p2.Users.Add(u2);
            p2.Users.Add(u2);
            
            Manager m = new Manager("Admin");
            m.Profiles.Add(p1);
            m.Profiles.Add(p2);
            Profile foundUser = m.ProfileByName("UserProfile");
        Assert.NotNull(foundUser);
        Assert.Equal("UserProfile", foundUser.Name);

        }

        
        [Fact]
        public void testRemoveProfileValidAdmin()
        {
            Manager manager = new Manager("Admin");
            Profile profile = new Profile { Name = Profile.AdminProfileName };
            User u = new User("Admin", "1234");
            profile.Users.Add(u);
            manager.Profiles.Add(profile);

            bool result = manager.RemoveProfile(Profile.AdminProfileName);

            Assert.True(result);
            Assert.Empty(manager.Profiles);
            Assert.Null(manager.ProfileByName("TestProfile"));
        }

    }

}