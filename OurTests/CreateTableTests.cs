using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class CreateTableTests
    {
        [Fact]
        public void TestCreateTableSuccess()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            CreateTable createTable = new CreateTable("Test", col);
            string result = createTable.Execute(database);
            Assert.Equal(Constants.CreateTableSuccess, result);
        }

        [Fact]
        public void TestCreateTableAlreadyExists()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };
            CreateTable createTable = new CreateTable("Test", col);
            createTable.Execute(database);
            string result = createTable.Execute(database);
            Assert.Equal(Constants.TableAlreadyExistsError, result);
        }

        [Fact]
        public void TestCreateTableWithoutColumns()
        {
            Database database = Database.CreateTestDatabase();
            List<ColumnDefinition> col = new List<ColumnDefinition>();
            CreateTable createTable = new CreateTable("Test", col);
            string result = createTable.Execute(database);
            Assert.Equal(Constants.DatabaseCreatedWithoutColumnsError, result);
        }

        [Fact]
        public void TestCreateTableNullDatabase()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };
            CreateTable createTable = new CreateTable("Test", col);
            string result = createTable.Execute(null);
            Assert.Equal(Constants.Error, result);
        }
    }
}