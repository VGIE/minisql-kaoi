using DbManager;
using DbManager.Parser;
using DbManager.Security;
using Xunit;

namespace OurTests
{
    public class ProfileTest
    {
        [Fact]
        public void GrantPrivilegeTest()
        {
            Profile profile = new Profile();
            profile.GrantPrivilege("Users", Privilege.Insert);

            bool result = profile.IsGrantedPrivilege("Users", Privilege.Insert);
            Assert.True(result);
            Assert.True(profile.IsGrantedPrivilege("Users", Privilege.Insert));
        }
        
        [Fact]
        public void RevokePrivilegeTest()
        {
            Profile profile = new Profile();
            profile.GrantPrivilege("Users", Privilege.Insert);
            bool revokeResult = profile.RevokePrivilege("Users", Privilege.Insert);
            Assert.True(revokeResult);
            Assert.False(profile.IsGrantedPrivilege("Users", Privilege.Insert));
        }

        [Fact]
        public void RevokeNonExistingPrivilegeTest()
        {
            Profile profile = new Profile();
            bool revokeResult = profile.RevokePrivilege("Users", Privilege.Insert);
            Assert.False(revokeResult);
        }

        [Fact]
        public void IsGrantedPrivilegeNonExistingTableTest()
        {
            Profile profile = new Profile();
            bool result = profile.IsGrantedPrivilege("NonExistingTable", Privilege.Select);
            Assert.False(result);
        }

        [Fact]
        public void IsGrantedPrivilegeNullTableTest()
        {
            Profile profile = new Profile();
            bool result = profile.IsGrantedPrivilege(null, Privilege.Select);
            Assert.False(result);
        }

        [Fact]
        public void GrantPrivilegeAddDuplicatePrivilegeTest()
        {
            Profile profile = new Profile();
            profile.GrantPrivilege("Users", Privilege.Select);
            int initialCount = profile.PrivilegesOn["Users"].Count;

            bool result = profile.GrantPrivilege("Users", Privilege.Select);
            Assert.False(result);
            Assert.Equal(initialCount, profile.PrivilegesOn["Users"].Count);
        }

        [Fact]
        public void GrantPrivilegeWithNullTableTest()
        {
            Profile profile = new Profile();
            bool result = profile.GrantPrivilege(null, Privilege.Delete);
            Assert.False(result);
        }

        [Fact]
        public void RevokePrivilegeWithNullTableTest()
        {
            Profile profile = new Profile();
            bool result = profile.RevokePrivilege(null, Privilege.Delete);
            Assert.False(result);
        }

        [Fact]
        public void GrantPrivilegeAddOtherPrivilegeTest()
        {
            Profile profile = new Profile();
            profile.GrantPrivilege("Users", Privilege.Select);

            bool result = profile.GrantPrivilege("Users", Privilege.Update);
            Assert.True(result);
            Assert.True(profile.IsGrantedPrivilege("Users", Privilege.Select));
            Assert.True(profile.IsGrantedPrivilege("Users", Privilege.Update));
        }

        [Fact]
        public void IsGrantedPrivilegeNonExistPrivilegeTest()
        {
            Profile profile = new Profile();
            profile.GrantPrivilege("Users", Privilege.Select);

            bool result = profile.IsGrantedPrivilege("Users", Privilege.Delete);
            Assert.False(result);
        }



    }
}