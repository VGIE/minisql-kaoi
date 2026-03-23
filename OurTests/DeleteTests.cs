using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class DeleteTests
    {
        [Fact]
        public void TestDeleteSuccess()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            database.CreateTable("Test", col);
            database.Insert("Test", new List<string>() { "Igor", "42" });
            Delete delete = new Delete("Test", new Condition("Name","=", "Igor"));
            string result = delete.Execute(database);
            Assert.Equal(Constants.DeleteSuccess, result);
        }
        [Fact]
        public void TestDeleteTableDoesNotExist()
        {
            Database database = Database.CreateTestDatabase();
            Delete delete = new Delete("NonExistentTable", new Condition("Name", "=", "Igor"));
            string result = delete.Execute(database);
            Assert.Equal(Constants.TableDoesNotExistError, result);
        }
        [Fact]
        public void TestDeleteError()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            database.CreateTable("Test", col);
            Delete delete = new Delete("Test", new Condition("NonExistentColumn","=", "NonExistentValue"));
            string result = delete.Execute(database);
            Assert.Equal(Constants.Error, result);
        }
    }
}