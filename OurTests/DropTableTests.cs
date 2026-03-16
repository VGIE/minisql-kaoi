using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class DropTableTests
    {
        [Fact]
        public void TestDropTableSuccess()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            DropTable dropTable = new DropTable("Test");
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
        [Fact]
        public void TestDropTableWithoutColumns()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>();
            DropTable dropTable = new DropTable("Test");
            string result = dropTable.Execute(database);
            Assert.Equal(Constants.DatabaseCreatedWithoutColumnsError, result);
        }
    }
}