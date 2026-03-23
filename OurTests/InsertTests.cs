using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class InsertTests
    {
        [Fact]
        public void TestInsertSuccess()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            database.CreateTable("Test", col);
            database.Insert("Test", new List<string>() { "Igor", "42" });
            Insert insert = new Insert("Test", new List<string>() { "Igor", "42" });
            string result = insert.Execute(database);
            Assert.Equal(Constants.InsertSuccess, result);
        }
        [Fact]
        public void TestInsertTableDoesNotExist()
        {
            Database database = Database.CreateTestDatabase();
            Insert insert = new Insert("NonExistentTable", new List<string>() { "Igor", "42" });
            string result = insert.Execute(database);
            Assert.Equal(Constants.TableDoesNotExistError, result);
        }
        [Fact]
        public void TestInsertError()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            database.CreateTable("Test", col);
            Insert insert = new Insert("Test", new List<string>() {});
            string result = insert.Execute(database);
            Assert.Equal(Constants.Error, result);
        }
    }
}