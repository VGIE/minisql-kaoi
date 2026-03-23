using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class DropTableTests
    {
        [Fact]
        public void TestDropTableTrue()
        {
            Database database = Database.CreateTestDatabase();
            DropTable dropTable = new DropTable(Table.TestTableName);
            string result = dropTable.Execute(database);
            Assert.Equal(Constants.DropTableSuccess, result);

        }

        [Fact]
        public void TestDropTableDoesNotExistError()
        {
            Database database = Database.CreateTestDatabase();
            DropTable dropTable = new DropTable("Test1");
            string result = dropTable.Execute(database);
            Assert.Equal(Constants.TableDoesNotExistError, result);
        }
    }
}