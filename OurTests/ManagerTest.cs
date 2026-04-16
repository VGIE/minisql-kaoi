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
            Manager adminManager = new Manager("admin");
            Manager userManager = new Manager("user");

            Assert.True(adminManager.IsUserAdmin());
            Assert.False(userManager.IsUserAdmin());
        }

        [Fact]
        public void ProfileByUserTest()
        {
            Manager manager = new Manager("admin");
            Profile profile = new Profile();
            profile.Name = "testProfile";
            profile.Users.Add(new User("igor", "1234"));
            manager.Profiles.Add(profile);

            Assert.Equal(profile, manager.ProfileByUser("igor"));
            Assert.Null(manager.ProfileByUser("desconocido"));
        }
        /*
        [Fact]
        public void UserByNameTest()
        {
            User u1 = new User("user1", "1234");
            User u2 = new User("user2", "1234");
            User u3 = new User("user3", "1234");

            User a1 = new User("admin1", "1235");
            User a2 = new User("admin2", "1235");


            Profile p1 = new Profile();
            p1.Users.Add(a1);
            p1.Users.Add(a2);

            Profile p2 = new Profile();
            p2.Users.Add(u1);
            p2.Users.Add(u2);
            p2.Users.Add(u2);
            
            Manager m = new Manager("admin");
            m.AddProfile(p1);
            m.AddProfile(p2);
            User foundUser = m.UserByName("admin1");
        Assert.NotNull(foundUser);
        Assert.Equal("admin1", foundUser.Username);

        }

        [Fact]
        public void ProfileByNameTest()
        {
            User u1 = new User("user1", "1234");
            User u2 = new User("user2", "1234");
            User u3 = new User("user3", "1234");

            User a1 = new User("admin1", "1235");
            User a2 = new User("admin2", "1235");


            Profile p1 = new Profile();
            p1.Users.Add(a1);
            p1.Users.Add(a2);

            Profile p2 = new Profile();
            p2.Users.Add(u1);
            p2.Users.Add(u2);
            p2.Users.Add(u2);
            
            Manager m = new Manager("admin");
            m.AddProfile(p1);
            m.AddProfile(p2);
            Profile foundUser = m.ProfileByName("admin1");
        Assert.NotNull(foundUser);
        Assert.Equal("admin", foundUser.Name);

        }

        */

    }
}