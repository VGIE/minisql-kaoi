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

    }
}